using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using SharpDX.D3DCompiler;

namespace MGFXC.Effect;

internal class ConstantBufferData
{
	public string Name { get; private set; }

	public int Size { get; private set; }

	public List<int> ParameterIndex { get; private set; }

	public List<int> ParameterOffset { get; private set; }

	public List<EffectObject.d3dx_parameter> Parameters { get; private set; }

	public ConstantBufferData(string name)
	{
		Name = name;
		ParameterIndex = new List<int>();
		ParameterOffset = new List<int>();
		Parameters = new List<EffectObject.d3dx_parameter>();
		Size = 0;
	}

	public bool SameAs(ConstantBufferData other)
	{
		if (Name != other.Name)
		{
			return false;
		}
		if (Size != other.Size || Parameters.Count != other.Parameters.Count)
		{
			return false;
		}
		for (int i = 0; i < Parameters.Count; i++)
		{
			EffectObject.d3dx_parameter p1 = Parameters[i];
			EffectObject.d3dx_parameter p2 = other.Parameters[i];
			if (p1.name != p2.name || p1.rows != p2.rows || p1.columns != p2.columns || p1.class_ != p2.class_ || p1.type != p2.type || p1.bufferOffset != p2.bufferOffset)
			{
				return false;
			}
		}
		return true;
	}

	public ConstantBufferData(string name, MojoShader.MOJOSHADER_symbolRegisterSet set, MojoShader.MOJOSHADER_symbol[] symbols)
	{
		Name = name ?? string.Empty;
		ParameterIndex = new List<int>();
		ParameterOffset = new List<int>();
		Parameters = new List<EffectObject.d3dx_parameter>();
		int minRegister = 32767;
		int maxRegister = 0;
		int registerSize = ((set == MojoShader.MOJOSHADER_symbolRegisterSet.MOJOSHADER_SYMREGSET_BOOL) ? 1 : 4) * 4;
		for (int i = 0; i < symbols.Length; i++)
		{
			MojoShader.MOJOSHADER_symbol symbol = symbols[i];
			if (symbol.register_set == set)
			{
				EffectObject.d3dx_parameter parm = GetParameterFromSymbol(symbol);
				int offset = (parm.bufferOffset = (int)symbol.register_index * registerSize);
				Parameters.Add(parm);
				ParameterOffset.Add(offset);
				minRegister = Math.Min(minRegister, (int)symbol.register_index);
				maxRegister = Math.Max(maxRegister, (int)(symbol.register_index + symbol.register_count));
			}
		}
		Size = Math.Max(maxRegister - minRegister, 0) * registerSize;
	}

	private static EffectObject.d3dx_parameter GetParameterFromSymbol(MojoShader.MOJOSHADER_symbol symbol)
	{
		EffectObject.d3dx_parameter param = new EffectObject.d3dx_parameter();
		param.rows = symbol.info.rows;
		param.columns = symbol.info.columns;
		param.name = symbol.name ?? string.Empty;
		param.semantic = string.Empty;
		int registerSize = ((symbol.register_set == MojoShader.MOJOSHADER_symbolRegisterSet.MOJOSHADER_SYMREGSET_BOOL) ? 1 : 4) * 4;
		int offset = (param.bufferOffset = (int)symbol.register_index * registerSize);
		switch (symbol.info.parameter_class)
		{
		case MojoShader.MOJOSHADER_symbolClass.MOJOSHADER_SYMCLASS_SCALAR:
			param.class_ = EffectObject.D3DXPARAMETER_CLASS.SCALAR;
			break;
		case MojoShader.MOJOSHADER_symbolClass.MOJOSHADER_SYMCLASS_VECTOR:
			param.class_ = EffectObject.D3DXPARAMETER_CLASS.VECTOR;
			break;
		case MojoShader.MOJOSHADER_symbolClass.MOJOSHADER_SYMCLASS_MATRIX_COLUMNS:
			param.class_ = EffectObject.D3DXPARAMETER_CLASS.MATRIX_COLUMNS;
			break;
		default:
			throw new Exception("Unsupported parameter class!");
		}
		switch (symbol.info.parameter_type)
		{
		case MojoShader.MOJOSHADER_symbolType.MOJOSHADER_SYMTYPE_BOOL:
			param.type = EffectObject.D3DXPARAMETER_TYPE.BOOL;
			break;
		case MojoShader.MOJOSHADER_symbolType.MOJOSHADER_SYMTYPE_FLOAT:
			param.type = EffectObject.D3DXPARAMETER_TYPE.FLOAT;
			break;
		case MojoShader.MOJOSHADER_symbolType.MOJOSHADER_SYMTYPE_INT:
			param.type = EffectObject.D3DXPARAMETER_TYPE.INT;
			break;
		default:
			throw new Exception("Unsupported parameter type!");
		}
		param.data = new byte[param.rows * param.columns * 4];
		param.member_count = symbol.info.member_count;
		param.element_count = ((symbol.info.elements > 1) ? symbol.info.elements : 0u);
		if (param.member_count != 0)
		{
			param.member_handles = new EffectObject.d3dx_parameter[param.member_count];
			MojoShader.MOJOSHADER_symbol[] members = MarshalHelper.UnmarshalArray<MojoShader.MOJOSHADER_symbol>(symbol.info.members, (int)symbol.info.member_count);
			for (int j = 0; j < param.member_count; j++)
			{
				EffectObject.d3dx_parameter mparam2 = GetParameterFromSymbol(members[j]);
				param.member_handles[j] = mparam2;
			}
		}
		else
		{
			param.member_handles = new EffectObject.d3dx_parameter[param.element_count];
			for (int i = 0; i < param.element_count; i++)
			{
				EffectObject.d3dx_parameter mparam = new EffectObject.d3dx_parameter();
				mparam.name = string.Empty;
				mparam.semantic = string.Empty;
				mparam.type = param.type;
				mparam.class_ = param.class_;
				mparam.rows = param.rows;
				mparam.columns = param.columns;
				mparam.data = new byte[param.columns * param.rows * 4];
				param.member_handles[i] = mparam;
			}
		}
		return param;
	}

	public ConstantBufferData(ConstantBuffer cb)
	{
		Name = string.Empty;
		Size = cb.Description.Size;
		ParameterIndex = new List<int>();
		List<EffectObject.d3dx_parameter> parameters = new List<EffectObject.d3dx_parameter>();
		for (int i = 0; i < cb.Description.VariableCount; i++)
		{
			ShaderReflectionVariable vdesc = cb.GetVariable(i);
			EffectObject.d3dx_parameter param = GetParameterFromType(vdesc.GetVariableType());
			param.name = vdesc.Description.Name;
			param.semantic = string.Empty;
			param.bufferOffset = vdesc.Description.StartOffset;
			uint size = param.columns * param.rows * 4;
			byte[] data = new byte[size];
			if (vdesc.Description.DefaultValue != IntPtr.Zero)
			{
				Marshal.Copy(vdesc.Description.DefaultValue, data, 0, (int)size);
			}
			param.data = data;
			parameters.Add(param);
		}
		Parameters = parameters.OrderBy((EffectObject.d3dx_parameter e) => e.bufferOffset).ToList();
		ParameterOffset = new List<int>();
		foreach (EffectObject.d3dx_parameter param2 in Parameters)
		{
			ParameterOffset.Add(param2.bufferOffset);
		}
	}

	private static EffectObject.d3dx_parameter GetParameterFromType(ShaderReflectionType type)
	{
		EffectObject.d3dx_parameter param = new EffectObject.d3dx_parameter();
		param.rows = (uint)type.Description.RowCount;
		param.columns = (uint)type.Description.ColumnCount;
		param.name = type.Description.Name ?? string.Empty;
		param.semantic = string.Empty;
		param.bufferOffset = type.Description.Offset;
		switch (type.Description.Class)
		{
		case ShaderVariableClass.Scalar:
			param.class_ = EffectObject.D3DXPARAMETER_CLASS.SCALAR;
			break;
		case ShaderVariableClass.Vector:
			param.class_ = EffectObject.D3DXPARAMETER_CLASS.VECTOR;
			break;
		case ShaderVariableClass.MatrixColumns:
			param.class_ = EffectObject.D3DXPARAMETER_CLASS.MATRIX_COLUMNS;
			break;
		default:
			throw new Exception("Unsupported parameter class!");
		}
		switch (type.Description.Type)
		{
		case ShaderVariableType.Bool:
			param.type = EffectObject.D3DXPARAMETER_TYPE.BOOL;
			break;
		case ShaderVariableType.Float:
			param.type = EffectObject.D3DXPARAMETER_TYPE.FLOAT;
			break;
		case ShaderVariableType.Int:
			param.type = EffectObject.D3DXPARAMETER_TYPE.INT;
			break;
		default:
			throw new Exception("Unsupported parameter type!");
		}
		param.member_count = (uint)type.Description.MemberCount;
		param.element_count = (uint)type.Description.ElementCount;
		if (param.member_count != 0)
		{
			param.member_handles = new EffectObject.d3dx_parameter[param.member_count];
			for (int j = 0; j < param.member_count; j++)
			{
				EffectObject.d3dx_parameter mparam2 = GetParameterFromType(type.GetMemberType(j));
				mparam2.name = type.GetMemberTypeName(j) ?? string.Empty;
				param.member_handles[j] = mparam2;
			}
		}
		else
		{
			param.member_handles = new EffectObject.d3dx_parameter[param.element_count];
			for (int i = 0; i < param.element_count; i++)
			{
				EffectObject.d3dx_parameter mparam = new EffectObject.d3dx_parameter();
				mparam.name = string.Empty;
				mparam.semantic = string.Empty;
				mparam.type = param.type;
				mparam.class_ = param.class_;
				mparam.rows = param.rows;
				mparam.columns = param.columns;
				mparam.data = new byte[param.columns * param.rows * 4];
				param.member_handles[i] = mparam;
			}
		}
		return param;
	}

	public void Write(BinaryWriter writer, Options options)
	{
		writer.Write(Name);
		writer.Write((ushort)Size);
		writer.Write((byte)ParameterIndex.Count);
		for (int i = 0; i < ParameterIndex.Count; i++)
		{
			writer.Write((byte)ParameterIndex[i]);
			writer.Write((ushort)ParameterOffset[i]);
		}
	}
}
