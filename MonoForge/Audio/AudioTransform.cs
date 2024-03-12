using FmodForFoxes;
using Microsoft.Xna.Framework;

namespace MonoForge.Audio;

public sealed class AudioTransform : IUpdatable
{
    private readonly FmodChannel _channel;

    internal AudioTransform(FmodChannel channel)
    {
        _channel = channel;
    }

    public bool Is3D { get; set; }

    public Vector3 Position { get; set; }

    public Vector3 Velocity { get; set; }

    public ConeSettings3D ConeSettings { get; set; }

    public float Rotation { get; set; }

    public float MinAudibleDistance { get; set; }

    public float MaxAudibleDistance { get; set; }

    public void Update(GameBase gameBase, float deltaTime)
    {
    }
}