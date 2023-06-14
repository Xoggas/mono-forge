namespace MonoGine.Audio;

public sealed class AudioManager : IAudioManager
{
    public AudioChannel? Master => throw new System.NotImplementedException();

    public AudioChannel? Sfx => throw new System.NotImplementedException();

    public AudioChannel GetOrAddChannel(int id)
    {
        return default;
    }

    public void Initialize(Engine engine)
    {

    }

    public void Update(Engine engine)
    {

    }

    public void Dispose()
    {

    }
}
