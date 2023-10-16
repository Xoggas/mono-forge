using System.Collections.Generic;
using MonoGine.Ecs;

namespace MonoGine.Animations;

public sealed class Animation : Component
{
    private List<Sequence> _sequences = new();
}