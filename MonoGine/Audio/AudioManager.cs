namespace MonoGine.Audio;

public sealed class AudioManager : IAudioManager
{
    public AudioChannel Master => throw new System.NotImplementedException();
    public AudioChannel Sfx => throw new System.NotImplementedException();

    public AudioChannel GetOrAddChannel(int id)
    {
        return default;
    }

    public void Initialize(IEngine engine)
    {

    }

    public void Update(IEngine engine)
    {

    }

    public void Dispose()
    {

    }
}
