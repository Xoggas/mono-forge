using System.Collections.Generic;
using System.Linq;

namespace MonoGine.ContentPipeline;

public sealed class ExtensionFilter
{
    private List<string> _extensions;

    public ExtensionFilter(params string[] extensions)
    {
        _extensions = extensions.ToList();
    }

    public bool Contains(string extension)
    {
        return _extensions.Contains(extension);
    }
}
