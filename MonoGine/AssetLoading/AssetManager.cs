using System;
using System.Collections.Generic;
using System.IO;
using MonoGine.Audio;
using MonoGine.Rendering;

namespace MonoGine.AssetLoading;

public sealed class AssetManager : IAssetManager
{
    private readonly IEngine _engine;
    private readonly Dictionary<string, IAsset> _cachedAssets = new();
    private readonly Dictionary<Type, IAssetProcessor> _processors = new();

    internal AssetManager(IEngine engine)
    {
        _engine = engine;
    }

    public void Initialize(IEngine engine)
    {
        Directory.CreateDirectory(PathUtils.AssetsPath);
        RegisterProcessor<Sprite>(new SpriteReader());
        RegisterProcessor<Shader>(new ShaderReader());
        RegisterProcessor<AudioClip>(new AudioClipReader());
    }

    public void RegisterProcessor<T>(IAssetProcessor assetProcessor) where T : class, IAsset
    {
        _processors.Add(typeof(T), assetProcessor);
    }

    public T LoadFromFile<T>(string path) where T : class, IAsset
    {
        if (_cachedAssets.TryGetValue(path, out IAsset? asset) && asset is T specificAsset)
        {
            return specificAsset;
        }

        if (_processors.TryGetValue(typeof(T), out IAssetProcessor? processor) && processor is IAssetReader<T> reader)
        {
            T loadedAsset = reader.Read(_engine, path);
            _cachedAssets.Add(path, loadedAsset);
            return loadedAsset;
        }

        throw new IOException($"Can't load file from path {path}");
    }

    public void SaveToFile<T>(string path, T asset) where T : class, IAsset
    {
        if (_processors.TryGetValue(typeof(T), out IAssetProcessor? processor) && processor is IAssetWriter<T> writer)
        {
            writer.Write(_engine, path, asset);
        }
        else
        {
            throw new IOException($"Can't save file to path {path}");
        }
    }

    public void Unload(string path)
    {
        if (_cachedAssets.TryGetValue(path, out IAsset? asset))
        {
            asset.Dispose();
        }

        _cachedAssets.Remove(path);
    }

    public void Dispose()
    {
        foreach (IAsset asset in _cachedAssets.Values)
        {
            asset.Dispose();
        }

        _cachedAssets.Clear();
    }
}