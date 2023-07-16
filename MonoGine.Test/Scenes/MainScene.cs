using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGine.Audio;
using MonoGine.SceneGraph;
using MonoGine.SceneManagement;

namespace MonoGine.Test;

public sealed class MainScene : Scene
{
    private IAudioSource _source = default!;

    public override void Update(IEngine engine)
    {
        base.Update(engine);

        if (engine.Input.Keyboard.WasPressed(Keys.S))
        {
            _source.Stop();
        }

        if (engine.Input.Keyboard.WasPressed(Keys.P))
        {
            _source.Play();
        }
        
        if (engine.Input.Keyboard.IsPressed(Keys.LeftShift) && engine.Input.Keyboard.WasPressed(Keys.P))
        {
            _source.Pause();
        }
        
        if (engine.Input.Keyboard.WasPressed(Keys.End))
        {
            engine.AudioManager.StopAll();
        }
    }

    protected override void OnLoadResources(IEngine engine)
    {
    }

    protected override void OnLoad(IEngine engine, object[]? args)
    {
        Camera.BackgroundColor = Color.Gray;

        var audioClip = engine.ResourceManager.Load<AudioClip>("Liftoff.mp3");

        var source = engine.AudioManager.Master.CreateSource();

        source.Clip = audioClip;

        source.Volume = 1f;

        source.Pitch = 1f;

        source.IsLooping = false;

        source.Id = "ambience";

        source.Play();

        _source = source;

        var texture = engine.ResourceManager.Load<Texture2D>("Rect.png");

        var spriteNode = new SpriteNode();

        spriteNode.Texture = texture;

        spriteNode.Transform.Position = new Vector2(640, 360);

        spriteNode.Transform.Scale = new Vector2(100, 100);

        spriteNode.SetParent(Root);
    }

    protected override void OnUnload(IEngine engine, object[]? args)
    {
    }
}
