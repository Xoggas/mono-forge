using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGine.Rendering.Batching;

internal sealed class Batcher : IBatcher
{
    private const int _initialBatchSize = 128;
    private const int _maxPassSize = 4096;

    private readonly SpriteEffect _spriteEffect;
    private readonly EffectPass _effectPass;

    private readonly VertexPositionColorTexture[] _vertices;
    private readonly short[] _indices;

    private BatchItem[] _batchItems;
    private int _itemCount;

    internal Batcher(IEngine engine)
    {
        _spriteEffect = new SpriteEffect(engine.GraphicsDevice);
        _effectPass = _spriteEffect.CurrentTechnique.Passes[0];
        _batchItems = new BatchItem[_initialBatchSize];
        _vertices = new VertexPositionColorTexture[_maxPassSize * 4];
        _indices = new short[_maxPassSize * 6];

        for (var i = 0; i < _maxPassSize; i++)
        {
            _indices[i * 6] = (short)(i * 4);
            _indices[i * 6 + 1] = (short)(i * 4 + 1);
            _indices[i * 6 + 2] = (short)(i * 4 + 2);
            _indices[i * 6 + 3] = (short)(i * 4 + 2);
            _indices[i * 6 + 4] = (short)(i * 4 + 1);
            _indices[i * 6 + 5] = (short)(i * 4 + 3);
        }
    }

    public void Push(BatchItem batchItem)
    {
        if (_itemCount >= _batchItems.Length)
        {
            Array.Resize(ref _batchItems, _batchItems.Length + _batchItems.Length / 2);
        }

        _batchItems[_itemCount++] = batchItem;
    }

    public void Begin(IEngine engine, Matrix? transformMatrix)
    {
        _itemCount = 0;
        _spriteEffect.TransformMatrix = transformMatrix;
        _effectPass.Apply();
    }

    public void End(IEngine engine)
    {
        if (_itemCount == 0)
        {
            return;
        }

        Array.Sort(_batchItems, 0, _itemCount);

        BatchItem lastItem = _batchItems[0];
        var length = 0;

        for (var i = 0; i < _itemCount; i++)
        {
            BatchItem item = _batchItems[i];

            if (length >= _maxPassSize - 1 || !item.Equals(lastItem))
            {
                Flush(engine, lastItem._texture, lastItem._shader, length);
                lastItem = item;
                length = 0;
            }

            PushVerticesFromItemToIndex(item, length++);
        }

        if (length > 0)
        {
            Flush(engine, lastItem._texture, lastItem._shader, length);
        }

        Array.Clear(_batchItems);
    }

    public void Flush(IEngine engine, Texture2D? texture, Shader? shader, int length)
    {
        if (length == 0)
        {
            throw new InvalidOperationException("Can't draw a batch with length 0");
        }

        if (texture == null)
        {
            throw new NullReferenceException(nameof(texture));
        }

        if (shader != null)
        {
            shader.ApplyProperties();

            foreach (EffectPass pass in shader.Passes)
            {
                pass.Apply();
                DrawPrimitives(engine, texture, length);
            }
        }
        else
        {
            DrawPrimitives(engine, texture, length);
        }
    }

    public void Dispose()
    {
        _spriteEffect.Dispose();
    }

    private void PushVerticesFromItemToIndex(BatchItem item, int index)
    {
        _vertices[index * 4] = item._topLeft;
        _vertices[index * 4 + 1] = item._topRight;
        _vertices[index * 4 + 2] = item._bottomLeft;
        _vertices[index * 4 + 3] = item._bottomRight;
    }

    private void DrawPrimitives(IEngine engine, Texture texture, int length)
    {
        engine.GraphicsDevice.Textures[0] = texture;
        engine.GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, _vertices, 0, length * 4, _indices,
            0, length * 2, VertexPositionColorTexture.VertexDeclaration);
    }
}