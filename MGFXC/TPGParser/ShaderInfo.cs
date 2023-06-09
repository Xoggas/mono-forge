using System.Collections.Generic;

namespace MGFXC.Effect.TPGParser;

public class ShaderInfo
{
	public List<TechniqueInfo> Techniques = new List<TechniqueInfo>();

	public Dictionary<string, SamplerStateInfo> SamplerStates = new Dictionary<string, SamplerStateInfo>();
}
