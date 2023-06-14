namespace MonoGine.Audio;

public interface IAudioManager : IObject, ISystem
{
    public AudioChannel? Master { get; }
    public AudioChannel? Sfx { get; }
    public AudioChannel GetOrAddChannel(int id);
}
