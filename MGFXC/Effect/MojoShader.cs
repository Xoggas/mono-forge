using System;
using System.Runtime.InteropServices;

namespace MGFXC.Effect;

internal class MojoShader
{
	public class NativeConstants
	{
		public const string _INCL_MOJOSHADER_H_ = "";

		public const string _INCL_MOJOSHADER_VERSION_H_ = "";

		public const int MOJOSHADER_VERSION = 1111;

		public const string MOJOSHADER_CHANGESET = "hg-1111:91a6af79b5e4";

		public const int MOJOSHADER_POSITION_NONE = -3;

		public const int MOJOSHADER_POSITION_BEFORE = -2;

		public const int MOJOSHADER_POSITION_AFTER = -1;

		public const string MOJOSHADER_PROFILE_D3D = "d3d";

		public const string MOJOSHADER_PROFILE_BYTECODE = "bytecode";

		public const string MOJOSHADER_PROFILE_GLSL = "glsl";

		public const string MOJOSHADER_PROFILE_GLSL120 = "glsl120";

		public const string MOJOSHADER_PROFILE_ARB1 = "arb1";

		public const string MOJOSHADER_PROFILE_NV2 = "nv2";

		public const string MOJOSHADER_PROFILE_NV3 = "nv3";

		public const string MOJOSHADER_PROFILE_NV4 = "nv4";

		public const string MOJOSHADER_SRC_PROFILE_HLSL_VS_1_1 = "hlsl_vs_1_1";

		public const string MOJOSHADER_SRC_PROFILE_HLSL_VS_2_0 = "hlsl_vs_2_0";

		public const string MOJOSHADER_SRC_PROFILE_HLSL_VS_3_0 = "hlsl_vs_3_0";

		public const string MOJOSHADER_SRC_PROFILE_HLSL_PS_1_1 = "hlsl_ps_1_1";

		public const string MOJOSHADER_SRC_PROFILE_HLSL_PS_1_2 = "hlsl_ps_1_2";

		public const string MOJOSHADER_SRC_PROFILE_HLSL_PS_1_3 = "hlsl_ps_1_3";

		public const string MOJOSHADER_SRC_PROFILE_HLSL_PS_1_4 = "hlsl_ps_1_4";

		public const string MOJOSHADER_SRC_PROFILE_HLSL_PS_2_0 = "hlsl_ps_2_0";

		public const string MOJOSHADER_SRC_PROFILE_HLSL_PS_3_0 = "hlsl_ps_3_0";

		public const int MOJOSHADER_AST_DATATYPE_CONST = int.MinValue;
	}

	public struct MOJOSHADER_uniform
	{
		public MOJOSHADER_uniformType type;

		public int index;

		public int array_count;

		public int constant;

		[MarshalAs(UnmanagedType.LPStr)]
		public string name;
	}

	public struct MOJOSHADER_constant
	{
		public MOJOSHADER_uniformType type;

		public int index;

		public Anonymous_5371dd6a_e42a_47c1_91d1_a2af9a8283be value;
	}

	public struct MOJOSHADER_sampler
	{
		public MOJOSHADER_samplerType type;

		public int index;

		[MarshalAs(UnmanagedType.LPStr)]
		public string name;

		public int texbem;
	}

	public struct MOJOSHADER_samplerMap
	{
		public int index;

		public MOJOSHADER_samplerType type;
	}

	public struct MOJOSHADER_attribute
	{
		public MOJOSHADER_usage usage;

		public int index;

		[MarshalAs(UnmanagedType.LPStr)]
		public string name;
	}

	public struct MOJOSHADER_swizzle
	{
		public MOJOSHADER_usage usage;

		public uint index;

		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
		public string swizzles;
	}

	public struct MOJOSHADER_symbolTypeInfo
	{
		public MOJOSHADER_symbolClass parameter_class;

		public MOJOSHADER_symbolType parameter_type;

		public uint rows;

		public uint columns;

		public uint elements;

		public uint member_count;

		public IntPtr members;
	}

	public struct MOJOSHADER_symbolStructMember
	{
		[MarshalAs(UnmanagedType.LPStr)]
		public string name;

		public MOJOSHADER_symbolTypeInfo info;
	}

	public struct MOJOSHADER_symbol
	{
		[MarshalAs(UnmanagedType.LPStr)]
		public string name;

		public MOJOSHADER_symbolRegisterSet register_set;

		public uint register_index;

		public uint register_count;

		public MOJOSHADER_symbolTypeInfo info;
	}

	public struct MOJOSHADER_error
	{
		[MarshalAs(UnmanagedType.LPStr)]
		public string error;

		[MarshalAs(UnmanagedType.LPStr)]
		public string filename;

		public int error_position;
	}

	public enum MOJOSHADER_preshaderOpcode
	{
		MOJOSHADER_PRESHADEROP_NOP = 0,
		MOJOSHADER_PRESHADEROP_MOV = 1,
		MOJOSHADER_PRESHADEROP_NEG = 2,
		MOJOSHADER_PRESHADEROP_RCP = 3,
		MOJOSHADER_PRESHADEROP_FRC = 4,
		MOJOSHADER_PRESHADEROP_EXP = 5,
		MOJOSHADER_PRESHADEROP_LOG = 6,
		MOJOSHADER_PRESHADEROP_RSQ = 7,
		MOJOSHADER_PRESHADEROP_SIN = 8,
		MOJOSHADER_PRESHADEROP_COS = 9,
		MOJOSHADER_PRESHADEROP_ASIN = 10,
		MOJOSHADER_PRESHADEROP_ACOS = 11,
		MOJOSHADER_PRESHADEROP_ATAN = 12,
		MOJOSHADER_PRESHADEROP_MIN = 13,
		MOJOSHADER_PRESHADEROP_MAX = 14,
		MOJOSHADER_PRESHADEROP_LT = 15,
		MOJOSHADER_PRESHADEROP_GE = 16,
		MOJOSHADER_PRESHADEROP_ADD = 17,
		MOJOSHADER_PRESHADEROP_MUL = 18,
		MOJOSHADER_PRESHADEROP_ATAN2 = 19,
		MOJOSHADER_PRESHADEROP_DIV = 20,
		MOJOSHADER_PRESHADEROP_CMP = 21,
		MOJOSHADER_PRESHADEROP_MOVC = 22,
		MOJOSHADER_PRESHADEROP_DOT = 23,
		MOJOSHADER_PRESHADEROP_NOISE = 24,
		MOJOSHADER_PRESHADEROP_SCALAR_OPS = 25,
		MOJOSHADER_PRESHADEROP_MIN_SCALAR = 25,
		MOJOSHADER_PRESHADEROP_MAX_SCALAR = 26,
		MOJOSHADER_PRESHADEROP_LT_SCALAR = 27,
		MOJOSHADER_PRESHADEROP_GE_SCALAR = 28,
		MOJOSHADER_PRESHADEROP_ADD_SCALAR = 29,
		MOJOSHADER_PRESHADEROP_MUL_SCALAR = 30,
		MOJOSHADER_PRESHADEROP_ATAN2_SCALAR = 31,
		MOJOSHADER_PRESHADEROP_DIV_SCALAR = 32,
		MOJOSHADER_PRESHADEROP_DOT_SCALAR = 33,
		MOJOSHADER_PRESHADEROP_NOISE_SCALAR = 34
	}

	public enum MOJOSHADER_preshaderOperandType
	{
		MOJOSHADER_PRESHADEROPERAND_LITERAL = 1,
		MOJOSHADER_PRESHADEROPERAND_INPUT = 2,
		MOJOSHADER_PRESHADEROPERAND_OUTPUT = 4,
		MOJOSHADER_PRESHADEROPERAND_TEMP = 7,
		MOJOSHADER_PRESHADEROPERAND_UNKN = 255
	}

	public struct MOJOSHADER_preshaderOperand
	{
		public MOJOSHADER_preshaderOperandType type;

		public uint index;

		public int indexingType;

		public uint indexingIndex;
	}

	public struct MOJOSHADER_preshaderInstruction
	{
		public MOJOSHADER_preshaderOpcode opcode;

		public uint element_count;

		public uint operand_count;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.Struct)]
		public MOJOSHADER_preshaderOperand[] operands;
	}

	public struct MOJOSHADER_preshader
	{
		public uint literal_count;

		public IntPtr literals;

		public uint temp_count;

		public uint symbol_count;

		public IntPtr symbols;

		public uint instruction_count;

		public IntPtr instructions;
	}

	public struct MOJOSHADER_parseData
	{
		public int error_count;

		public IntPtr errors;

		[MarshalAs(UnmanagedType.LPStr)]
		public string profile;

		[MarshalAs(UnmanagedType.LPStr)]
		public string output;

		public int output_len;

		public int instruction_count;

		public MOJOSHADER_shaderType shader_type;

		public int major_ver;

		public int minor_ver;

		public int uniform_count;

		public IntPtr uniforms;

		public int constant_count;

		public IntPtr constants;

		public int sampler_count;

		public IntPtr samplers;

		public int attribute_count;

		public IntPtr attributes;

		public int output_count;

		public IntPtr outputs;

		public int swizzle_count;

		public IntPtr swizzles;

		public int symbol_count;

		public IntPtr symbols;

		public IntPtr preshader;

		public IntPtr malloc;

		public IntPtr free;

		public IntPtr malloc_data;
	}

	public struct MOJOSHADER_effectParam
	{
		[MarshalAs(UnmanagedType.LPStr)]
		public string name;

		[MarshalAs(UnmanagedType.LPStr)]
		public string semantic;
	}

	public struct MOJOSHADER_effectState
	{
		public uint type;
	}

	public struct MOJOSHADER_effectPass
	{
		[MarshalAs(UnmanagedType.LPStr)]
		public string name;

		public uint state_count;

		public IntPtr states;
	}

	public struct MOJOSHADER_effectTechnique
	{
		[MarshalAs(UnmanagedType.LPStr)]
		public string name;

		public uint pass_count;

		public IntPtr passes;
	}

	public struct MOJOSHADER_effectTexture
	{
		public uint param;

		[MarshalAs(UnmanagedType.LPStr)]
		public string name;
	}

	public struct MOJOSHADER_effectShader
	{
		public uint technique;

		public uint pass;

		public IntPtr shader;
	}

	public struct MOJOSHADER_effect
	{
		public int error_count;

		public IntPtr errors;

		[MarshalAs(UnmanagedType.LPStr)]
		public string profile;

		public int param_count;

		public IntPtr @params;

		public int technique_count;

		public IntPtr techniques;

		public int texture_count;

		public IntPtr textures;

		public int shader_count;

		public IntPtr shaders;

		public IntPtr malloc;

		public IntPtr free;

		public IntPtr malloc_data;
	}

	public struct MOJOSHADER_preprocessorDefine
	{
		[MarshalAs(UnmanagedType.LPStr)]
		public string identifier;

		[MarshalAs(UnmanagedType.LPStr)]
		public string definition;
	}

	public struct MOJOSHADER_preprocessData
	{
		public int error_count;

		public IntPtr errors;

		[MarshalAs(UnmanagedType.LPStr)]
		public string output;

		public int output_len;

		public IntPtr malloc;

		public IntPtr free;

		public IntPtr malloc_data;
	}

	public delegate int MOJOSHADER_includeOpen(MOJOSHADER_includeType inctype, [In][MarshalAs(UnmanagedType.LPStr)] string fname, [In][MarshalAs(UnmanagedType.LPStr)] string parent, ref IntPtr outdata, ref uint outbytes, IntPtr m, IntPtr f, IntPtr d);

	public delegate void MOJOSHADER_includeClose([In][MarshalAs(UnmanagedType.LPStr)] string data, IntPtr m, IntPtr f, IntPtr d);

	public enum MOJOSHADER_astDataTypeType
	{
		MOJOSHADER_AST_DATATYPE_NONE,
		MOJOSHADER_AST_DATATYPE_BOOL,
		MOJOSHADER_AST_DATATYPE_INT,
		MOJOSHADER_AST_DATATYPE_UINT,
		MOJOSHADER_AST_DATATYPE_FLOAT,
		MOJOSHADER_AST_DATATYPE_FLOAT_SNORM,
		MOJOSHADER_AST_DATATYPE_FLOAT_UNORM,
		MOJOSHADER_AST_DATATYPE_HALF,
		MOJOSHADER_AST_DATATYPE_DOUBLE,
		MOJOSHADER_AST_DATATYPE_STRING,
		MOJOSHADER_AST_DATATYPE_SAMPLER_1D,
		MOJOSHADER_AST_DATATYPE_SAMPLER_2D,
		MOJOSHADER_AST_DATATYPE_SAMPLER_3D,
		MOJOSHADER_AST_DATATYPE_SAMPLER_CUBE,
		MOJOSHADER_AST_DATATYPE_SAMPLER_STATE,
		MOJOSHADER_AST_DATATYPE_SAMPLER_COMPARISON_STATE,
		MOJOSHADER_AST_DATATYPE_STRUCT,
		MOJOSHADER_AST_DATATYPE_ARRAY,
		MOJOSHADER_AST_DATATYPE_VECTOR,
		MOJOSHADER_AST_DATATYPE_MATRIX,
		MOJOSHADER_AST_DATATYPE_BUFFER,
		MOJOSHADER_AST_DATATYPE_FUNCTION,
		MOJOSHADER_AST_DATATYPE_USER
	}

	public struct MOJOSHADER_astDataTypeStructMember
	{
		public IntPtr datatype;

		[MarshalAs(UnmanagedType.LPStr)]
		public string identifier;
	}

	public struct MOJOSHADER_astDataTypeStruct
	{
		public MOJOSHADER_astDataTypeType type;

		public IntPtr members;

		public int member_count;
	}

	public struct MOJOSHADER_astDataTypeArray
	{
		public MOJOSHADER_astDataTypeType type;

		public IntPtr @base;

		public int elements;
	}

	public struct MOJOSHADER_astDataTypeMatrix
	{
		public MOJOSHADER_astDataTypeType type;

		public IntPtr @base;

		public int rows;

		public int columns;
	}

	public struct MOJOSHADER_astDataTypeBuffer
	{
		public MOJOSHADER_astDataTypeType type;

		public IntPtr @base;
	}

	public struct MOJOSHADER_astDataTypeFunction
	{
		public MOJOSHADER_astDataTypeType type;

		public IntPtr retval;

		public IntPtr @params;

		public int num_params;

		public int intrinsic;
	}

	public struct MOJOSHADER_astDataTypeUser
	{
		public MOJOSHADER_astDataTypeType type;

		public IntPtr details;

		public IntPtr name;
	}

	[StructLayout(LayoutKind.Explicit)]
	public struct MOJOSHADER_astDataType
	{
		[FieldOffset(0)]
		public MOJOSHADER_astDataTypeType type;

		[FieldOffset(0)]
		public MOJOSHADER_astDataTypeArray array;

		[FieldOffset(0)]
		public MOJOSHADER_astDataTypeStruct structure;

		[FieldOffset(0)]
		public MOJOSHADER_astDataTypeArray vector;

		[FieldOffset(0)]
		public MOJOSHADER_astDataTypeMatrix matrix;

		[FieldOffset(0)]
		public MOJOSHADER_astDataTypeBuffer buffer;

		[FieldOffset(0)]
		public MOJOSHADER_astDataTypeUser user;

		[FieldOffset(0)]
		public MOJOSHADER_astDataTypeFunction function;
	}

	public enum MOJOSHADER_astNodeType
	{
		MOJOSHADER_AST_OP_START_RANGE,
		MOJOSHADER_AST_OP_START_RANGE_UNARY,
		MOJOSHADER_AST_OP_PREINCREMENT,
		MOJOSHADER_AST_OP_PREDECREMENT,
		MOJOSHADER_AST_OP_NEGATE,
		MOJOSHADER_AST_OP_COMPLEMENT,
		MOJOSHADER_AST_OP_NOT,
		MOJOSHADER_AST_OP_POSTINCREMENT,
		MOJOSHADER_AST_OP_POSTDECREMENT,
		MOJOSHADER_AST_OP_CAST,
		MOJOSHADER_AST_OP_END_RANGE_UNARY,
		MOJOSHADER_AST_OP_START_RANGE_BINARY,
		MOJOSHADER_AST_OP_COMMA,
		MOJOSHADER_AST_OP_MULTIPLY,
		MOJOSHADER_AST_OP_DIVIDE,
		MOJOSHADER_AST_OP_MODULO,
		MOJOSHADER_AST_OP_ADD,
		MOJOSHADER_AST_OP_SUBTRACT,
		MOJOSHADER_AST_OP_LSHIFT,
		MOJOSHADER_AST_OP_RSHIFT,
		MOJOSHADER_AST_OP_LESSTHAN,
		MOJOSHADER_AST_OP_GREATERTHAN,
		MOJOSHADER_AST_OP_LESSTHANOREQUAL,
		MOJOSHADER_AST_OP_GREATERTHANOREQUAL,
		MOJOSHADER_AST_OP_EQUAL,
		MOJOSHADER_AST_OP_NOTEQUAL,
		MOJOSHADER_AST_OP_BINARYAND,
		MOJOSHADER_AST_OP_BINARYXOR,
		MOJOSHADER_AST_OP_BINARYOR,
		MOJOSHADER_AST_OP_LOGICALAND,
		MOJOSHADER_AST_OP_LOGICALOR,
		MOJOSHADER_AST_OP_ASSIGN,
		MOJOSHADER_AST_OP_MULASSIGN,
		MOJOSHADER_AST_OP_DIVASSIGN,
		MOJOSHADER_AST_OP_MODASSIGN,
		MOJOSHADER_AST_OP_ADDASSIGN,
		MOJOSHADER_AST_OP_SUBASSIGN,
		MOJOSHADER_AST_OP_LSHIFTASSIGN,
		MOJOSHADER_AST_OP_RSHIFTASSIGN,
		MOJOSHADER_AST_OP_ANDASSIGN,
		MOJOSHADER_AST_OP_XORASSIGN,
		MOJOSHADER_AST_OP_ORASSIGN,
		MOJOSHADER_AST_OP_DEREF_ARRAY,
		MOJOSHADER_AST_OP_END_RANGE_BINARY,
		MOJOSHADER_AST_OP_START_RANGE_TERNARY,
		MOJOSHADER_AST_OP_CONDITIONAL,
		MOJOSHADER_AST_OP_END_RANGE_TERNARY,
		MOJOSHADER_AST_OP_START_RANGE_DATA,
		MOJOSHADER_AST_OP_IDENTIFIER,
		MOJOSHADER_AST_OP_INT_LITERAL,
		MOJOSHADER_AST_OP_FLOAT_LITERAL,
		MOJOSHADER_AST_OP_STRING_LITERAL,
		MOJOSHADER_AST_OP_BOOLEAN_LITERAL,
		MOJOSHADER_AST_OP_END_RANGE_DATA,
		MOJOSHADER_AST_OP_START_RANGE_MISC,
		MOJOSHADER_AST_OP_DEREF_STRUCT,
		MOJOSHADER_AST_OP_CALLFUNC,
		MOJOSHADER_AST_OP_CONSTRUCTOR,
		MOJOSHADER_AST_OP_END_RANGE_MISC,
		MOJOSHADER_AST_OP_END_RANGE,
		MOJOSHADER_AST_COMPUNIT_START_RANGE,
		MOJOSHADER_AST_COMPUNIT_FUNCTION,
		MOJOSHADER_AST_COMPUNIT_TYPEDEF,
		MOJOSHADER_AST_COMPUNIT_STRUCT,
		MOJOSHADER_AST_COMPUNIT_VARIABLE,
		MOJOSHADER_AST_COMPUNIT_END_RANGE,
		MOJOSHADER_AST_STATEMENT_START_RANGE,
		MOJOSHADER_AST_STATEMENT_EMPTY,
		MOJOSHADER_AST_STATEMENT_BREAK,
		MOJOSHADER_AST_STATEMENT_CONTINUE,
		MOJOSHADER_AST_STATEMENT_DISCARD,
		MOJOSHADER_AST_STATEMENT_BLOCK,
		MOJOSHADER_AST_STATEMENT_EXPRESSION,
		MOJOSHADER_AST_STATEMENT_IF,
		MOJOSHADER_AST_STATEMENT_SWITCH,
		MOJOSHADER_AST_STATEMENT_FOR,
		MOJOSHADER_AST_STATEMENT_DO,
		MOJOSHADER_AST_STATEMENT_WHILE,
		MOJOSHADER_AST_STATEMENT_RETURN,
		MOJOSHADER_AST_STATEMENT_TYPEDEF,
		MOJOSHADER_AST_STATEMENT_STRUCT,
		MOJOSHADER_AST_STATEMENT_VARDECL,
		MOJOSHADER_AST_STATEMENT_END_RANGE,
		MOJOSHADER_AST_MISC_START_RANGE,
		MOJOSHADER_AST_FUNCTION_PARAMS,
		MOJOSHADER_AST_FUNCTION_SIGNATURE,
		MOJOSHADER_AST_SCALAR_OR_ARRAY,
		MOJOSHADER_AST_TYPEDEF,
		MOJOSHADER_AST_PACK_OFFSET,
		MOJOSHADER_AST_VARIABLE_LOWLEVEL,
		MOJOSHADER_AST_ANNOTATION,
		MOJOSHADER_AST_VARIABLE_DECLARATION,
		MOJOSHADER_AST_STRUCT_DECLARATION,
		MOJOSHADER_AST_STRUCT_MEMBER,
		MOJOSHADER_AST_SWITCH_CASE,
		MOJOSHADER_AST_ARGUMENTS,
		MOJOSHADER_AST_MISC_END_RANGE,
		MOJOSHADER_AST_END_RANGE
	}

	public struct MOJOSHADER_astNodeInfo
	{
		public MOJOSHADER_astNodeType type;

		public IntPtr filename;

		public uint line;
	}

	public enum MOJOSHADER_astVariableAttributes
	{
		MOJOSHADER_AST_VARATTR_EXTERN = 1,
		MOJOSHADER_AST_VARATTR_NOINTERPOLATION = 2,
		MOJOSHADER_AST_VARATTR_SHARED = 4,
		MOJOSHADER_AST_VARATTR_STATIC = 8,
		MOJOSHADER_AST_VARATTR_UNIFORM = 0x10,
		MOJOSHADER_AST_VARATTR_VOLATILE = 0x20,
		MOJOSHADER_AST_VARATTR_CONST = 0x40,
		MOJOSHADER_AST_VARATTR_ROWMAJOR = 0x80,
		MOJOSHADER_AST_VARATTR_COLUMNMAJOR = 0x100
	}

	public enum MOJOSHADER_astIfAttributes
	{
		MOJOSHADER_AST_IFATTR_NONE,
		MOJOSHADER_AST_IFATTR_BRANCH,
		MOJOSHADER_AST_IFATTR_FLATTEN,
		MOJOSHADER_AST_IFATTR_IFALL,
		MOJOSHADER_AST_IFATTR_IFANY,
		MOJOSHADER_AST_IFATTR_PREDICATE,
		MOJOSHADER_AST_IFATTR_PREDICATEBLOCK
	}

	public enum MOJOSHADER_astSwitchAttributes
	{
		MOJOSHADER_AST_SWITCHATTR_NONE,
		MOJOSHADER_AST_SWITCHATTR_FLATTEN,
		MOJOSHADER_AST_SWITCHATTR_BRANCH,
		MOJOSHADER_AST_SWITCHATTR_FORCECASE,
		MOJOSHADER_AST_SWITCHATTR_CALL
	}

	public struct MOJOSHADER_astGeneric
	{
		public MOJOSHADER_astNodeInfo ast;
	}

	public struct MOJOSHADER_astExpression
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr datatype;
	}

	public struct MOJOSHADER_astArguments
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr argument;

		public IntPtr next;
	}

	public struct MOJOSHADER_astExpressionUnary
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr datatype;

		public IntPtr operand;
	}

	public struct MOJOSHADER_astExpressionBinary
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr datatype;

		public IntPtr left;

		public IntPtr right;
	}

	public struct MOJOSHADER_astExpressionTernary
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr datatype;

		public IntPtr left;

		public IntPtr center;

		public IntPtr right;
	}

	public struct MOJOSHADER_astExpressionIdentifier
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr datatype;

		public IntPtr identifier;

		public int index;
	}

	public struct MOJOSHADER_astExpressionIntLiteral
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr datatype;

		public int value;
	}

	public struct MOJOSHADER_astExpressionFloatLiteral
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr datatype;

		public double value;
	}

	public struct MOJOSHADER_astExpressionStringLiteral
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr datatype;

		public IntPtr @string;
	}

	public struct MOJOSHADER_astExpressionBooleanLiteral
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr datatype;

		public int value;
	}

	public struct MOJOSHADER_astExpressionConstructor
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr datatype;

		public IntPtr args;
	}

	public struct MOJOSHADER_astExpressionDerefStruct
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr datatype;

		public IntPtr identifier;

		public IntPtr member;

		public int isswizzle;

		public int member_index;
	}

	public struct MOJOSHADER_astExpressionCallFunction
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr datatype;

		public IntPtr identifier;

		public IntPtr args;
	}

	public struct MOJOSHADER_astExpressionCast
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr datatype;

		public IntPtr operand;
	}

	public struct MOJOSHADER_astCompilationUnit
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr next;
	}

	public enum MOJOSHADER_astFunctionStorageClass
	{
		MOJOSHADER_AST_FNSTORECLS_NONE,
		MOJOSHADER_AST_FNSTORECLS_INLINE
	}

	public enum MOJOSHADER_astInputModifier
	{
		MOJOSHADER_AST_INPUTMOD_NONE,
		MOJOSHADER_AST_INPUTMOD_IN,
		MOJOSHADER_AST_INPUTMOD_OUT,
		MOJOSHADER_AST_INPUTMOD_INOUT,
		MOJOSHADER_AST_INPUTMOD_UNIFORM
	}

	public enum MOJOSHADER_astInterpolationModifier
	{
		MOJOSHADER_AST_INTERPMOD_NONE,
		MOJOSHADER_AST_INTERPMOD_LINEAR,
		MOJOSHADER_AST_INTERPMOD_CENTROID,
		MOJOSHADER_AST_INTERPMOD_NOINTERPOLATION,
		MOJOSHADER_AST_INTERPMOD_NOPERSPECTIVE,
		MOJOSHADER_AST_INTERPMOD_SAMPLE
	}

	public struct MOJOSHADER_astFunctionParameters
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr datatype;

		public MOJOSHADER_astInputModifier input_modifier;

		public IntPtr identifier;

		public IntPtr semantic;

		public MOJOSHADER_astInterpolationModifier interpolation_modifier;

		public IntPtr initializer;

		public IntPtr next;
	}

	public struct MOJOSHADER_astFunctionSignature
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr datatype;

		public IntPtr identifier;

		public IntPtr @params;

		public MOJOSHADER_astFunctionStorageClass storage_class;

		public IntPtr semantic;
	}

	public struct MOJOSHADER_astScalarOrArray
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr identifier;

		public int isarray;

		public IntPtr dimension;
	}

	public struct MOJOSHADER_astAnnotations
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr datatype;

		public IntPtr initializer;

		public IntPtr next;
	}

	public struct MOJOSHADER_astPackOffset
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr ident1;

		public IntPtr ident2;
	}

	public struct MOJOSHADER_astVariableLowLevel
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr packoffset;

		public IntPtr register_name;
	}

	public struct MOJOSHADER_astStructMembers
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr datatype;

		public IntPtr semantic;

		public IntPtr details;

		public MOJOSHADER_astInterpolationModifier interpolation_mod;

		public IntPtr next;
	}

	public struct MOJOSHADER_astStructDeclaration
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr datatype;

		public IntPtr name;

		public IntPtr members;
	}

	public struct MOJOSHADER_astVariableDeclaration
	{
		public MOJOSHADER_astNodeInfo ast;

		public int attributes;

		public IntPtr datatype;

		public IntPtr anonymous_datatype;

		public IntPtr details;

		public IntPtr semantic;

		public IntPtr annotations;

		public IntPtr initializer;

		public IntPtr lowlevel;

		public IntPtr next;
	}

	public struct MOJOSHADER_astStatement
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr next;
	}

	public struct MOJOSHADER_astBlockStatement
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr next;

		public IntPtr statements;
	}

	public struct MOJOSHADER_astReturnStatement
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr next;

		public IntPtr expr;
	}

	public struct MOJOSHADER_astExpressionStatement
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr next;

		public IntPtr expr;
	}

	public struct MOJOSHADER_astIfStatement
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr next;

		public int attributes;

		public IntPtr expr;

		public IntPtr statement;

		public IntPtr else_statement;
	}

	public struct MOJOSHADER_astSwitchCases
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr expr;

		public IntPtr statement;

		public IntPtr next;
	}

	public struct MOJOSHADER_astSwitchStatement
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr next;

		public int attributes;

		public IntPtr expr;

		public IntPtr cases;
	}

	public struct MOJOSHADER_astWhileStatement
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr next;

		public int unroll;

		public IntPtr expr;

		public IntPtr statement;
	}

	public struct MOJOSHADER_astForStatement
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr next;

		public int unroll;

		public IntPtr var_decl;

		public IntPtr initializer;

		public IntPtr looptest;

		public IntPtr counter;

		public IntPtr statement;
	}

	public struct MOJOSHADER_astTypedef
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr datatype;

		public int isconst;

		public IntPtr details;
	}

	public struct MOJOSHADER_astTypedefStatement
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr next;

		public IntPtr type_info;
	}

	public struct MOJOSHADER_astVarDeclStatement
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr next;

		public IntPtr declaration;
	}

	public struct MOJOSHADER_astStructStatement
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr next;

		public IntPtr struct_info;
	}

	public struct MOJOSHADER_astCompilationUnitFunction
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr next;

		public IntPtr declaration;

		public IntPtr definition;

		public int index;
	}

	public struct MOJOSHADER_astCompilationUnitTypedef
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr next;

		public IntPtr type_info;
	}

	public struct MOJOSHADER_astCompilationUnitStruct
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr next;

		public IntPtr struct_info;
	}

	public struct MOJOSHADER_astCompilationUnitVariable
	{
		public MOJOSHADER_astNodeInfo ast;

		public IntPtr next;

		public IntPtr declaration;
	}

	[StructLayout(LayoutKind.Explicit)]
	public struct MOJOSHADER_astNode
	{
		[FieldOffset(0)]
		public MOJOSHADER_astNodeInfo ast;

		[FieldOffset(0)]
		public MOJOSHADER_astGeneric generic;

		[FieldOffset(0)]
		public MOJOSHADER_astExpression expression;

		[FieldOffset(0)]
		public MOJOSHADER_astArguments arguments;

		[FieldOffset(0)]
		public MOJOSHADER_astExpressionUnary unary;

		[FieldOffset(0)]
		public MOJOSHADER_astExpressionBinary binary;

		[FieldOffset(0)]
		public MOJOSHADER_astExpressionTernary ternary;

		[FieldOffset(0)]
		public MOJOSHADER_astExpressionIdentifier identifier;

		[FieldOffset(0)]
		public MOJOSHADER_astExpressionIntLiteral intliteral;

		[FieldOffset(0)]
		public MOJOSHADER_astExpressionFloatLiteral floatliteral;

		[FieldOffset(0)]
		public MOJOSHADER_astExpressionStringLiteral stringliteral;

		[FieldOffset(0)]
		public MOJOSHADER_astExpressionBooleanLiteral boolliteral;

		[FieldOffset(0)]
		public MOJOSHADER_astExpressionConstructor constructor;

		[FieldOffset(0)]
		public MOJOSHADER_astExpressionDerefStruct derefstruct;

		[FieldOffset(0)]
		public MOJOSHADER_astExpressionCallFunction callfunc;

		[FieldOffset(0)]
		public MOJOSHADER_astExpressionCast cast;

		[FieldOffset(0)]
		public MOJOSHADER_astCompilationUnit compunit;

		[FieldOffset(0)]
		public MOJOSHADER_astFunctionParameters @params;

		[FieldOffset(0)]
		public MOJOSHADER_astFunctionSignature funcsig;

		[FieldOffset(0)]
		public MOJOSHADER_astScalarOrArray soa;

		[FieldOffset(0)]
		public MOJOSHADER_astAnnotations annotations;

		[FieldOffset(0)]
		public MOJOSHADER_astPackOffset packoffset;

		[FieldOffset(0)]
		public MOJOSHADER_astVariableLowLevel varlowlevel;

		[FieldOffset(0)]
		public MOJOSHADER_astStructMembers structmembers;

		[FieldOffset(0)]
		public MOJOSHADER_astStructDeclaration structdecl;

		[FieldOffset(0)]
		public MOJOSHADER_astVariableDeclaration vardecl;

		[FieldOffset(0)]
		public MOJOSHADER_astStatement stmt;

		[FieldOffset(0)]
		public MOJOSHADER_astStatement emptystmt;

		[FieldOffset(0)]
		public MOJOSHADER_astStatement breakstmt;

		[FieldOffset(0)]
		public MOJOSHADER_astStatement contstmt;

		[FieldOffset(0)]
		public MOJOSHADER_astStatement discardstmt;

		[FieldOffset(0)]
		public MOJOSHADER_astBlockStatement blockstmt;

		[FieldOffset(0)]
		public MOJOSHADER_astReturnStatement returnstmt;

		[FieldOffset(0)]
		public MOJOSHADER_astExpressionStatement exprstmt;

		[FieldOffset(0)]
		public MOJOSHADER_astIfStatement ifstmt;

		[FieldOffset(0)]
		public MOJOSHADER_astSwitchCases cases;

		[FieldOffset(0)]
		public MOJOSHADER_astSwitchStatement switchstmt;

		[FieldOffset(0)]
		public MOJOSHADER_astWhileStatement whilestmt;

		[FieldOffset(0)]
		public MOJOSHADER_astWhileStatement dostmt;

		[FieldOffset(0)]
		public MOJOSHADER_astForStatement forstmt;

		[FieldOffset(0)]
		public MOJOSHADER_astTypedef typdef;

		[FieldOffset(0)]
		public MOJOSHADER_astTypedefStatement typedefstmt;

		[FieldOffset(0)]
		public MOJOSHADER_astVarDeclStatement vardeclstmt;

		[FieldOffset(0)]
		public MOJOSHADER_astStructStatement structstmt;

		[FieldOffset(0)]
		public MOJOSHADER_astCompilationUnitFunction funcunit;

		[FieldOffset(0)]
		public MOJOSHADER_astCompilationUnitTypedef typedefunit;

		[FieldOffset(0)]
		public MOJOSHADER_astCompilationUnitStruct structunit;

		[FieldOffset(0)]
		public MOJOSHADER_astCompilationUnitVariable varunit;
	}

	public struct MOJOSHADER_astData
	{
		public int error_count;

		public IntPtr errors;

		[MarshalAs(UnmanagedType.LPStr)]
		public string source_profile;

		public IntPtr ast;

		public IntPtr malloc;

		public IntPtr free;

		public IntPtr malloc_data;

		public IntPtr opaque;
	}

	public enum MOJOSHADER_irNodeType
	{
		MOJOSHADER_IR_START_RANGE_EXPR,
		MOJOSHADER_IR_CONSTANT,
		MOJOSHADER_IR_TEMP,
		MOJOSHADER_IR_BINOP,
		MOJOSHADER_IR_MEMORY,
		MOJOSHADER_IR_CALL,
		MOJOSHADER_IR_ESEQ,
		MOJOSHADER_IR_ARRAY,
		MOJOSHADER_IR_CONVERT,
		MOJOSHADER_IR_SWIZZLE,
		MOJOSHADER_IR_CONSTRUCT,
		MOJOSHADER_IR_END_RANGE_EXPR,
		MOJOSHADER_IR_START_RANGE_STMT,
		MOJOSHADER_IR_MOVE,
		MOJOSHADER_IR_EXPR_STMT,
		MOJOSHADER_IR_JUMP,
		MOJOSHADER_IR_CJUMP,
		MOJOSHADER_IR_SEQ,
		MOJOSHADER_IR_LABEL,
		MOJOSHADER_IR_DISCARD,
		MOJOSHADER_IR_END_RANGE_STMT,
		MOJOSHADER_IR_START_RANGE_MISC,
		MOJOSHADER_IR_EXPRLIST,
		MOJOSHADER_IR_END_RANGE_MISC,
		MOJOSHADER_IR_END_RANGE
	}

	public struct MOJOSHADER_irNodeInfo
	{
		public MOJOSHADER_irNodeType type;

		public IntPtr filename;

		public uint line;
	}

	public struct MOJOSHADER_irGeneric
	{
		public MOJOSHADER_irNodeInfo ir;
	}

	public enum MOJOSHADER_irBinOpType
	{
		MOJOSHADER_IR_BINOP_ADD,
		MOJOSHADER_IR_BINOP_SUBTRACT,
		MOJOSHADER_IR_BINOP_MULTIPLY,
		MOJOSHADER_IR_BINOP_DIVIDE,
		MOJOSHADER_IR_BINOP_MODULO,
		MOJOSHADER_IR_BINOP_AND,
		MOJOSHADER_IR_BINOP_OR,
		MOJOSHADER_IR_BINOP_XOR,
		MOJOSHADER_IR_BINOP_LSHIFT,
		MOJOSHADER_IR_BINOP_RSHIFT,
		MOJOSHADER_IR_BINOP_UNKNOWN
	}

	public enum MOJOSHADER_irConditionType
	{
		MOJOSHADER_IR_COND_EQL,
		MOJOSHADER_IR_COND_NEQ,
		MOJOSHADER_IR_COND_LT,
		MOJOSHADER_IR_COND_GT,
		MOJOSHADER_IR_COND_LEQ,
		MOJOSHADER_IR_COND_GEQ,
		MOJOSHADER_IR_COND_UNKNOWN
	}

	public struct MOJOSHADER_irExprInfo
	{
		public MOJOSHADER_irNodeInfo ir;

		public MOJOSHADER_astDataTypeType type;

		public int elements;
	}

	public struct MOJOSHADER_irConstant
	{
		public MOJOSHADER_irExprInfo info;

		public Anonymous_3a13e6d2_72d8_4c86_b5bf_9aff36c73111 value;
	}

	public struct MOJOSHADER_irTemp
	{
		public MOJOSHADER_irExprInfo info;

		public int index;
	}

	public struct MOJOSHADER_irBinOp
	{
		public MOJOSHADER_irExprInfo info;

		public MOJOSHADER_irBinOpType op;

		public IntPtr left;

		public IntPtr right;
	}

	public struct MOJOSHADER_irMemory
	{
		public MOJOSHADER_irExprInfo info;

		public int index;
	}

	public struct MOJOSHADER_irCall
	{
		public MOJOSHADER_irExprInfo info;

		public int index;

		public IntPtr args;
	}

	public struct MOJOSHADER_irESeq
	{
		public MOJOSHADER_irExprInfo info;

		public IntPtr stmt;

		public IntPtr expr;
	}

	public struct MOJOSHADER_irArray
	{
		public MOJOSHADER_irExprInfo info;

		public IntPtr array;

		public IntPtr element;
	}

	public struct MOJOSHADER_irConvert
	{
		public MOJOSHADER_irExprInfo info;

		public IntPtr expr;
	}

	public struct MOJOSHADER_irSwizzle
	{
		public MOJOSHADER_irExprInfo info;

		public IntPtr expr;

		public IntPtr channels;
	}

	public struct MOJOSHADER_irConstruct
	{
		public MOJOSHADER_irExprInfo info;

		public IntPtr args;
	}

	[StructLayout(LayoutKind.Explicit)]
	public struct MOJOSHADER_irExpression
	{
		[FieldOffset(0)]
		public MOJOSHADER_irNodeInfo ir;

		[FieldOffset(0)]
		public MOJOSHADER_irExprInfo info;

		[FieldOffset(0)]
		public MOJOSHADER_irConstant constant;

		[FieldOffset(0)]
		public MOJOSHADER_irTemp temp;

		[FieldOffset(0)]
		public MOJOSHADER_irBinOp binop;

		[FieldOffset(0)]
		public MOJOSHADER_irMemory memory;

		[FieldOffset(0)]
		public MOJOSHADER_irCall call;

		[FieldOffset(0)]
		public MOJOSHADER_irESeq eseq;

		[FieldOffset(0)]
		public MOJOSHADER_irArray array;

		[FieldOffset(0)]
		public MOJOSHADER_irConvert convert;

		[FieldOffset(0)]
		public MOJOSHADER_irSwizzle swizzle;

		[FieldOffset(0)]
		public MOJOSHADER_irConstruct construct;
	}

	public struct MOJOSHADER_irMove
	{
		public MOJOSHADER_irNodeInfo ir;

		public IntPtr dst;

		public IntPtr src;

		public int writemask;
	}

	public struct MOJOSHADER_irExprStmt
	{
		public MOJOSHADER_irNodeInfo ir;

		public IntPtr expr;
	}

	public struct MOJOSHADER_irJump
	{
		public MOJOSHADER_irNodeInfo ir;

		public int label;
	}

	public struct MOJOSHADER_irCJump
	{
		public MOJOSHADER_irNodeInfo ir;

		public MOJOSHADER_irConditionType cond;

		public IntPtr left;

		public IntPtr right;

		public int iftrue;

		public int iffalse;
	}

	public struct MOJOSHADER_irSeq
	{
		public MOJOSHADER_irNodeInfo ir;

		public IntPtr first;

		public IntPtr next;
	}

	public struct MOJOSHADER_irLabel
	{
		public MOJOSHADER_irNodeInfo ir;

		public int index;
	}

	[StructLayout(LayoutKind.Explicit)]
	public struct MOJOSHADER_irStatement
	{
		[FieldOffset(0)]
		public MOJOSHADER_irNodeInfo ir;

		[FieldOffset(0)]
		public MOJOSHADER_irGeneric generic;

		[FieldOffset(0)]
		public MOJOSHADER_irMove move;

		[FieldOffset(0)]
		public MOJOSHADER_irExprStmt expr;

		[FieldOffset(0)]
		public MOJOSHADER_irJump jump;

		[FieldOffset(0)]
		public MOJOSHADER_irCJump cjump;

		[FieldOffset(0)]
		public MOJOSHADER_irSeq seq;

		[FieldOffset(0)]
		public MOJOSHADER_irLabel label;

		[FieldOffset(0)]
		public MOJOSHADER_irGeneric discard;
	}

	public struct MOJOSHADER_irExprList
	{
		public MOJOSHADER_irNodeInfo ir;

		public IntPtr expr;

		public IntPtr next;
	}

	[StructLayout(LayoutKind.Explicit)]
	public struct MOJOSHADER_irMisc
	{
		[FieldOffset(0)]
		public MOJOSHADER_irNodeInfo ir;

		[FieldOffset(0)]
		public MOJOSHADER_irGeneric generic;

		[FieldOffset(0)]
		public MOJOSHADER_irExprList exprlist;
	}

	[StructLayout(LayoutKind.Explicit)]
	public struct MOJOSHADER_irNode
	{
		[FieldOffset(0)]
		public MOJOSHADER_irNodeInfo ir;

		[FieldOffset(0)]
		public MOJOSHADER_irGeneric generic;

		[FieldOffset(0)]
		public MOJOSHADER_irExpression expr;

		[FieldOffset(0)]
		public MOJOSHADER_irStatement stmt;

		[FieldOffset(0)]
		public MOJOSHADER_irMisc misc;
	}

	public struct MOJOSHADER_compileData
	{
		public int error_count;

		public IntPtr errors;

		public int warning_count;

		public IntPtr warnings;

		[MarshalAs(UnmanagedType.LPStr)]
		public string source_profile;

		[MarshalAs(UnmanagedType.LPStr)]
		public string output;

		public int output_len;

		public int symbol_count;

		public IntPtr symbols;

		public IntPtr malloc;

		public IntPtr free;

		public IntPtr malloc_data;
	}

	public delegate IntPtr MOJOSHADER_glGetProcAddress([In][MarshalAs(UnmanagedType.LPStr)] string fnname, IntPtr data);

	[StructLayout(LayoutKind.Explicit)]
	public struct Anonymous_5371dd6a_e42a_47c1_91d1_a2af9a8283be
	{
		[FieldOffset(0)]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.R4)]
		public IntPtr f;

		[FieldOffset(0)]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = UnmanagedType.I4)]
		public IntPtr i;

		[FieldOffset(0)]
		public int b;
	}

	[StructLayout(LayoutKind.Explicit)]
	public struct Anonymous_3a13e6d2_72d8_4c86_b5bf_9aff36c73111
	{
		[FieldOffset(0)]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.I4)]
		public IntPtr ival;

		[FieldOffset(0)]
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16, ArraySubType = UnmanagedType.R4)]
		public IntPtr fval;
	}

	public enum MOJOSHADER_uniformType
	{
		MOJOSHADER_UNIFORM_UNKNOWN = -1,
		MOJOSHADER_UNIFORM_FLOAT,
		MOJOSHADER_UNIFORM_INT,
		MOJOSHADER_UNIFORM_BOOL
	}

	public enum MOJOSHADER_samplerType
	{
		MOJOSHADER_SAMPLER_UNKNOWN = -1,
		MOJOSHADER_SAMPLER_2D,
		MOJOSHADER_SAMPLER_CUBE,
		MOJOSHADER_SAMPLER_VOLUME,
		MOJOSHADER_SAMPLER_1D
	}

	public enum MOJOSHADER_usage
	{
		MOJOSHADER_USAGE_UNKNOWN = -1,
		MOJOSHADER_USAGE_POSITION,
		MOJOSHADER_USAGE_BLENDWEIGHT,
		MOJOSHADER_USAGE_BLENDINDICES,
		MOJOSHADER_USAGE_NORMAL,
		MOJOSHADER_USAGE_POINTSIZE,
		MOJOSHADER_USAGE_TEXCOORD,
		MOJOSHADER_USAGE_TANGENT,
		MOJOSHADER_USAGE_BINORMAL,
		MOJOSHADER_USAGE_TESSFACTOR,
		MOJOSHADER_USAGE_POSITIONT,
		MOJOSHADER_USAGE_COLOR,
		MOJOSHADER_USAGE_FOG,
		MOJOSHADER_USAGE_DEPTH,
		MOJOSHADER_USAGE_SAMPLE,
		MOJOSHADER_USAGE_TOTAL
	}

	public enum MOJOSHADER_symbolClass
	{
		MOJOSHADER_SYMCLASS_SCALAR,
		MOJOSHADER_SYMCLASS_VECTOR,
		MOJOSHADER_SYMCLASS_MATRIX_ROWS,
		MOJOSHADER_SYMCLASS_MATRIX_COLUMNS,
		MOJOSHADER_SYMCLASS_OBJECT,
		MOJOSHADER_SYMCLASS_STRUCT
	}

	public enum MOJOSHADER_symbolType
	{
		MOJOSHADER_SYMTYPE_VOID,
		MOJOSHADER_SYMTYPE_BOOL,
		MOJOSHADER_SYMTYPE_INT,
		MOJOSHADER_SYMTYPE_FLOAT,
		MOJOSHADER_SYMTYPE_STRING,
		MOJOSHADER_SYMTYPE_TEXTURE,
		MOJOSHADER_SYMTYPE_TEXTURE1D,
		MOJOSHADER_SYMTYPE_TEXTURE2D,
		MOJOSHADER_SYMTYPE_TEXTURE3D,
		MOJOSHADER_SYMTYPE_TEXTURECUBE,
		MOJOSHADER_SYMTYPE_SAMPLER,
		MOJOSHADER_SYMTYPE_SAMPLER1D,
		MOJOSHADER_SYMTYPE_SAMPLER2D,
		MOJOSHADER_SYMTYPE_SAMPLER3D,
		MOJOSHADER_SYMTYPE_SAMPLERCUBE,
		MOJOSHADER_SYMTYPE_PIXELSHADER,
		MOJOSHADER_SYMTYPE_VERTEXSHADER,
		MOJOSHADER_SYMTYPE_PIXELFRAGMENT,
		MOJOSHADER_SYMTYPE_VERTEXFRAGMENT,
		MOJOSHADER_SYMTYPE_UNSUPPORTED
	}

	public enum MOJOSHADER_symbolRegisterSet
	{
		MOJOSHADER_SYMREGSET_BOOL,
		MOJOSHADER_SYMREGSET_INT4,
		MOJOSHADER_SYMREGSET_FLOAT4,
		MOJOSHADER_SYMREGSET_SAMPLER
	}

	public enum MOJOSHADER_shaderType
	{
		MOJOSHADER_TYPE_UNKNOWN = 0,
		MOJOSHADER_TYPE_PIXEL = 1,
		MOJOSHADER_TYPE_VERTEX = 2,
		MOJOSHADER_TYPE_GEOMETRY = 4,
		MOJOSHADER_TYPE_ANY = -1
	}

	public enum MOJOSHADER_includeType
	{
		MOJOSHADER_INCLUDETYPE_LOCAL,
		MOJOSHADER_INCLUDETYPE_SYSTEM
	}

	public enum MOJOSHADER_attributeType
	{
		MOJOSHADER_ATTRIBUTE_UNKNOWN = -1,
		MOJOSHADER_ATTRIBUTE_BYTE,
		MOJOSHADER_ATTRIBUTE_UBYTE,
		MOJOSHADER_ATTRIBUTE_SHORT,
		MOJOSHADER_ATTRIBUTE_USHORT,
		MOJOSHADER_ATTRIBUTE_INT,
		MOJOSHADER_ATTRIBUTE_UINT,
		MOJOSHADER_ATTRIBUTE_FLOAT,
		MOJOSHADER_ATTRIBUTE_DOUBLE,
		MOJOSHADER_ATTRIBUTE_HALF_FLOAT
	}

	public class NativeMethods
	{
		[DllImport("libmojoshader_64.dll")]
		public static extern int MOJOSHADER_version();

		[DllImport("libmojoshader_64.dll")]
		public static extern IntPtr MOJOSHADER_changeset();

		[DllImport("libmojoshader_64.dll")]
		public static extern int MOJOSHADER_maxShaderModel([In][MarshalAs(UnmanagedType.LPStr)] string profile);

		[DllImport("libmojoshader_64.dll")]
		public static extern IntPtr MOJOSHADER_parseExpression([In] byte[] tokenbuf, int bufsize, IntPtr m, IntPtr f, IntPtr d);

		[DllImport("libmojoshader_64.dll")]
		public static extern void MOJOSHADER_runPreshader(ref MOJOSHADER_preshader param0, ref float param1, ref float param2);

		[DllImport("libmojoshader_64.dll")]
		public static extern IntPtr MOJOSHADER_parse([In][MarshalAs(UnmanagedType.LPStr)] string profile, [In] byte[] tokenbuf, int bufsize, IntPtr swiz, int swizcount, IntPtr smap, int smapcount, IntPtr m, IntPtr f, IntPtr d);

		[DllImport("libmojoshader_64.dll")]
		public static extern void MOJOSHADER_freeParseData(ref MOJOSHADER_parseData data);

		[DllImport("libmojoshader_64.dll")]
		public static extern IntPtr MOJOSHADER_parseEffect([In][MarshalAs(UnmanagedType.LPStr)] string profile, [In][MarshalAs(UnmanagedType.LPStr)] string buf, int _len, ref MOJOSHADER_swizzle swiz, int swizcount, ref MOJOSHADER_samplerMap smap, int smapcount, IntPtr m, IntPtr f, IntPtr d);

		[DllImport("libmojoshader_64.dll")]
		public static extern void MOJOSHADER_freeEffect(ref MOJOSHADER_effect effect);

		[DllImport("libmojoshader_64.dll")]
		public static extern IntPtr MOJOSHADER_preprocess([In][MarshalAs(UnmanagedType.LPStr)] string filename, [In][MarshalAs(UnmanagedType.LPStr)] string source, uint sourcelen, ref MOJOSHADER_preprocessorDefine defines, uint define_count, MOJOSHADER_includeOpen include_open, MOJOSHADER_includeClose include_close, IntPtr m, IntPtr f, IntPtr d);

		[DllImport("libmojoshader_64.dll")]
		public static extern void MOJOSHADER_freePreprocessData(ref MOJOSHADER_preprocessData data);

		[DllImport("libmojoshader_64.dll")]
		public static extern IntPtr MOJOSHADER_assemble([In][MarshalAs(UnmanagedType.LPStr)] string filename, [In][MarshalAs(UnmanagedType.LPStr)] string source, uint sourcelen, ref IntPtr comments, uint comment_count, ref MOJOSHADER_symbol symbols, uint symbol_count, ref MOJOSHADER_preprocessorDefine defines, uint define_count, MOJOSHADER_includeOpen include_open, MOJOSHADER_includeClose include_close, IntPtr m, IntPtr f, IntPtr d);

		[DllImport("libmojoshader_64.dll")]
		public static extern IntPtr MOJOSHADER_parseAst([In][MarshalAs(UnmanagedType.LPStr)] string srcprofile, [In][MarshalAs(UnmanagedType.LPStr)] string filename, [In][MarshalAs(UnmanagedType.LPStr)] string source, uint sourcelen, ref MOJOSHADER_preprocessorDefine defs, uint define_count, MOJOSHADER_includeOpen include_open, MOJOSHADER_includeClose include_close, IntPtr m, IntPtr f, IntPtr d);

		[DllImport("libmojoshader_64.dll")]
		public static extern void MOJOSHADER_freeAstData(ref MOJOSHADER_astData data);

		[DllImport("libmojoshader_64.dll")]
		public static extern IntPtr MOJOSHADER_compile([In][MarshalAs(UnmanagedType.LPStr)] string srcprofile, [In][MarshalAs(UnmanagedType.LPStr)] string filename, [In][MarshalAs(UnmanagedType.LPStr)] string source, uint sourcelen, ref MOJOSHADER_preprocessorDefine defs, uint define_count, MOJOSHADER_includeOpen include_open, MOJOSHADER_includeClose include_close, IntPtr m, IntPtr f, IntPtr d);

		[DllImport("libmojoshader_64.dll")]
		public static extern void MOJOSHADER_freeCompileData(ref MOJOSHADER_compileData data);

		[DllImport("libmojoshader_64.dll")]
		public static extern int MOJOSHADER_glAvailableProfiles(MOJOSHADER_glGetProcAddress lookup, IntPtr d, ref IntPtr profs, int size);

		[DllImport("libmojoshader_64.dll")]
		public static extern IntPtr MOJOSHADER_glBestProfile(MOJOSHADER_glGetProcAddress lookup, IntPtr d);

		[DllImport("libmojoshader_64.dll")]
		public static extern IntPtr MOJOSHADER_glGetError();

		[DllImport("libmojoshader_64.dll")]
		public static extern int MOJOSHADER_glMaxUniforms(MOJOSHADER_shaderType shader_type);

		[DllImport("libmojoshader_64.dll")]
		public static extern void MOJOSHADER_glSetVertexShaderUniformF(uint idx, ref float data, uint vec4count);

		[DllImport("libmojoshader_64.dll")]
		public static extern void MOJOSHADER_glGetVertexShaderUniformF(uint idx, ref float data, uint vec4count);

		[DllImport("libmojoshader_64.dll")]
		public static extern void MOJOSHADER_glSetVertexShaderUniformI(uint idx, ref int data, uint ivec4count);

		[DllImport("libmojoshader_64.dll")]
		public static extern void MOJOSHADER_glGetVertexShaderUniformI(uint idx, ref int data, uint ivec4count);

		[DllImport("libmojoshader_64.dll")]
		public static extern void MOJOSHADER_glSetVertexShaderUniformB(uint idx, ref int data, uint bcount);

		[DllImport("libmojoshader_64.dll")]
		public static extern void MOJOSHADER_glGetVertexShaderUniformB(uint idx, ref int data, uint bcount);

		[DllImport("libmojoshader_64.dll")]
		public static extern void MOJOSHADER_glSetPixelShaderUniformF(uint idx, ref float data, uint vec4count);

		[DllImport("libmojoshader_64.dll")]
		public static extern void MOJOSHADER_glGetPixelShaderUniformF(uint idx, ref float data, uint vec4count);

		[DllImport("libmojoshader_64.dll")]
		public static extern void MOJOSHADER_glSetPixelShaderUniformI(uint idx, ref int data, uint ivec4count);

		[DllImport("libmojoshader_64.dll")]
		public static extern void MOJOSHADER_glGetPixelShaderUniformI(uint idx, ref int data, uint ivec4count);

		[DllImport("libmojoshader_64.dll")]
		public static extern void MOJOSHADER_glSetPixelShaderUniformB(uint idx, ref int data, uint bcount);

		[DllImport("libmojoshader_64.dll")]
		public static extern void MOJOSHADER_glGetPixelShaderUniformB(uint idx, ref int data, uint bcount);

		[DllImport("libmojoshader_64.dll")]
		public static extern void MOJOSHADER_glSetLegacyBumpMapEnv(uint sampler, float mat00, float mat01, float mat10, float mat11, float lscale, float loffset);

		[DllImport("libmojoshader_64.dll")]
		public static extern void MOJOSHADER_glSetVertexAttribute(MOJOSHADER_usage usage, int index, uint size, MOJOSHADER_attributeType type, int normalized, uint stride, IntPtr ptr);

		[DllImport("libmojoshader_64.dll")]
		public static extern void MOJOSHADER_glSetVertexPreshaderUniformF(uint idx, ref float data, uint vec4n);

		[DllImport("libmojoshader_64.dll")]
		public static extern void MOJOSHADER_glGetVertexPreshaderUniformF(uint idx, ref float data, uint vec4n);

		[DllImport("libmojoshader_64.dll")]
		public static extern void MOJOSHADER_glSetPixelPreshaderUniformF(uint idx, ref float data, uint vec4n);

		[DllImport("libmojoshader_64.dll")]
		public static extern void MOJOSHADER_glGetPixelPreshaderUniformF(uint idx, ref float data, uint vec4n);

		[DllImport("libmojoshader_64.dll")]
		public static extern void MOJOSHADER_glProgramReady();
	}

	private const string mojoshader_dll = "libmojoshader_64.dll";
}
