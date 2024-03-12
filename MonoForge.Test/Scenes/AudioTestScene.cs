using MonoForge.Audio;
using MonoForge.SceneManagement;

namespace MonoForge.Test;

public sealed class AudioTestScene : Scene
{
    private AudioClip _song = default!;

    protected override void OnLoadResources(GameBase gameBase)
    {
        _song = AudioClip.FromFileStreamed("Content/Song.mp3");
    }

    protected override void OnLoad(GameBase gameBase, object[]? args)
    {
        IAudioSource source = gameBase.AudioManager.Master.CreateSource();
        source.Clip = _song;
        source.Volume = 0.5f;
        source.Play();
    }

    protected override void OnUnload(GameBase gameBase, object[]? args)
    {
        gameBase.AudioManager.Master.DestroyAll();
        _song.Dispose();
    }
}