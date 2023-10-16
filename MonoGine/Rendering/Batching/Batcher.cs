using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGine.Rendering.Batching;

internal sealed class Batcher : IBatcher
{
    private const int InitialBatchSize = 256;

    private readonly SpriteEffect _spriteEffect;
    private readonly EffectPass _effectPass;

    private VertexPositionColorTexture[] _vertices;
    private int[] _indices;

    private BatchItem[] _itemsToBatch;
    private int _itemCount;

    internal Batcher(IEngine engine)
    {
        _spriteEffect = new SpriteEffect(engine.GraphicsDevice);
        _effectPass = _spriteEffect.CurrentTechnique.Passes[0];
        _itemsToBatch = new BatchItem[InitialBatchSize];
        _vertices = new VertexPositionColorTexture[InitialBatchSize * 4];
        _indices = new int[InitialBatchSize * 6];
    }

    public void Push(BatchItem batchItem)
    {
        ExtendArrayIfNeeded(ref _itemsToBatch, _itemCount + 1);
        _itemsToBatch[_itemCount++] = batchItem;
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

        Array.Sort(_itemsToBatch, 0, _itemCount);

        BatchItem lastItem = _itemsToBatch[0];
        var itemCount = 0;
        var verticesCount = 0;
        var indicesCount = 0;
        var primitiveCount = 0;

        for (var i = 0; i < _itemCount; i++)
        {
            BatchItem currentItem = _itemsToBatch[i];

            if (lastItem.Equals(currentItem))
            {
                PushVerticesFromItem(currentItem, verticesCount);
                verticesCount += currentItem._mesh.Vertices.Length;

                PushIndicesFromItem(currentItem, indicesCount, primitiveCount);
                indicesCount += currentItem._mesh.Indices.Length;

                lastItem = currentItem;
                itemCount++;
                primitiveCount += currentItem._mesh.Indices.Length / 3;
            }
            else
            {
                Flush(engine, lastItem._texture, lastItem._shader, verticesCount, primitiveCount);
                verticesCount = 0;
                indicesCount = 0;
                itemCount = 0;
                primitiveCount = 0;
            }
        }

        if (itemCount > 0)
        {
            Flush(engine, lastItem._texture, lastItem._shader, verticesCount, primitiveCount);
        }
    }

    public void Flush(IEngine engine, Texture2D? texture, Shader? shader, int verticesCount, int primitiveCount)
    {
        if (verticesCount == 0 || primitiveCount == 0)
        {
            throw new InvalidOperationException("Can't draw an empty batch!");
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
                DrawPrimitives(engine, texture, verticesCount, primitiveCount);
            }
        }
        else
        {
            DrawPrimitives(engine, texture, verticesCount, primitiveCount);
        }
    }

    public void Dispose()
    {
        _spriteEffect.Dispose();
    }

    private void PushVerticesFromItem(BatchItem item, int index)
    {
        ExtendArrayIfNeeded(ref _vertices, index + item._mesh.Vertices.Length);

        Mesh mesh = item._mesh;

        for (var i = 0; i < item._mesh.Vertices.Length; i++)
        {
            Vertex vertex = mesh.Vertices[i];
            _vertices[index + i] = new VertexPositionColorTexture(vertex.Position, vertex.Color, mesh.Uvs[i]);
        }
    }

    private void PushIndicesFromItem(BatchItem item, int index, int offset)
    {
        ExtendArrayIfNeeded(ref _indices, index + item._mesh.Indices.Length);

        for (var i = 0; i < item._mesh.Indices.Length; i++)
        {
            _indices[index + i] = offset * 2 + item._mesh.Indices[i];
        }
    }

    private void ExtendArrayIfNeeded<T>(ref T[] array, int minLength)
    {
        while (minLength >= array.Length)
        {
            Array.Resize(ref array, array.Length + array.Length / 2);
        }
    }

    private void DrawPrimitives(IEngine engine, Texture texture, int vertexCount, int primitiveCount)
    {
        engine.GraphicsDevice.Textures[0] = texture;
        engine.GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, _vertices, 0, vertexCount, _indices,
            0, primitiveCount, VertexPositionColorTexture.VertexDeclaration);
    }
}