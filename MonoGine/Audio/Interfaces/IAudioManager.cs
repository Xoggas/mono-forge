namespace MonoGine.Audio;

public interface IAudioManager : IObject, IUpdatable
{
    public IAudioChannel Master { get; }
    public IAudioChannel Sfx { get; }
    public IAudioChannel GetOrAddChannel(int id);
}
