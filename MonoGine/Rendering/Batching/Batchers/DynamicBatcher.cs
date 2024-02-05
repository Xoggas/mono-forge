using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGine.Utilities;

namespace MonoGine.Rendering.Batching;

internal sealed class DynamicBatcher : IBatcher
{
    private const int InitialBatchSize = 1024;

    private BatchableGraphics[] _graphicsToBatch;
    private int _totalGraphicsToBatch;
    private int _firstItemToBatchIndex;

    private VertexPositionColorTexture[] _vertices;
    private int _totalBatchedVertices;
    private int[] _indices;
    private int _totalBatchedIndices;
    private int _totalBatchedPrimitives;
    private int _totalBatchedItems;

    internal DynamicBatcher()
    {
        _graphicsToBatch = new BatchableGraphics[InitialBatchSize];
        _vertices = new VertexPositionColorTexture[InitialBatchSize * 4];
        _indices = new int[InitialBatchSize * 6];
    }

    public void Push(Texture2D texture, Mesh mesh, Shader? shader, float depth)
    {
        ArrayUtility.ExtendArrayIfNeeded(ref _graphicsToBatch, _totalGraphicsToBatch + 1);

        ArrayUtility.InitializeElementsInArray(_graphicsToBatch);

        _graphicsToBatch[_totalGraphicsToBatch++].Set(texture, mesh, shader, depth);
    }

    public IEnumerable<BatchPassResult> GetPasses()
    {
        if (_totalGraphicsToBatch == 0 || _firstItemToBatchIndex >= _totalGraphicsToBatch)
        {
            yield break;
        }

        Array.Sort(_graphicsToBatch, 0, _totalGraphicsToBatch);

        BatchableGraphics lastBatchableGraphics = _graphicsToBatch[_firstItemToBatchIndex];

        for (var i = _firstItemToBatchIndex; i < _totalGraphicsToBatch; i++)
        {
            BatchableGraphics currentBatchableGraphics = _graphicsToBatch[i];

            if (lastBatchableGraphics.Equals(currentBatchableGraphics) == false)
            {
                yield return GeneratePassResultAndReset(lastBatchableGraphics);
            }

            PushGraphics(currentBatchableGraphics);

            lastBatchableGraphics = currentBatchableGraphics;
        }

        if (_totalGraphicsToBatch <= 0)
        {
            yield break;
        }

        yield return GeneratePassResultAndReset(lastBatchableGraphics);
    }

    public void Reset()
    {
        _totalGraphicsToBatch = 0;
        _firstItemToBatchIndex = 0;
        _totalBatchedVertices = 0;
        _totalBatchedIndices = 0;
        _totalBatchedPrimitives = 0;
        _totalBatchedItems = 0;
    }

    private BatchPassResult GeneratePassResultAndReset(BatchableGraphics graphics)
    {
        if (graphics.IsInvalid)
        {
            throw new InvalidOperationException("Invalid graphics!");
        }

        var batchResult = new BatchPassResult
        {
            Texture = graphics.Texture!,
            Shader = graphics.Shader,
            Vertices = _vertices,
            VertexCount = _totalBatchedVertices,
            Indices = _indices,
            PrimitiveCount = _totalBatchedPrimitives
        };

        _totalBatchedItems = 0;
        _totalBatchedVertices = 0;
        _totalBatchedIndices = 0;
        _totalBatchedPrimitives = 0;

        return batchResult;
    }

    private void PushGraphics(BatchableGraphics graphics)
    {
        if (graphics.IsInvalid)
        {
            throw new InvalidOperationException("Invalid graphics!");
        }

        PushVerticesAndUVs(graphics.Mesh!.Vertices, graphics.Mesh.Uvs);
        PushIndices(graphics.Mesh.Indices);

        _totalBatchedItems++;
        _firstItemToBatchIndex = _totalBatchedItems;
    }

    private void PushVerticesAndUVs(IReadOnlyList<Vertex> vertices, IReadOnlyList<Vector2> uvs)
    {
        ArrayUtility.ExtendArrayIfNeeded(ref _vertices, _totalBatchedVertices + vertices.Count);

        for (var i = 0; i < vertices.Count; i++)
        {
            Vertex vertex = vertices[i];

            VertexPositionColorTexture monogameVertex = new(vertex.Position, vertex.Color, uvs[i]);

            _vertices[_totalBatchedVertices + i] = monogameVertex;
        }

        _totalBatchedVertices += vertices.Count;
    }

    private void PushIndices(IReadOnlyList<int> indices)
    {
        ArrayUtility.ExtendArrayIfNeeded(ref _indices, _totalBatchedIndices + indices.Count);

        for (var i = 0; i < indices.Count; i++)
        {
            _indices[_totalBatchedIndices + i] = _totalBatchedPrimitives * 2 + indices[i];
        }

        _totalBatchedIndices += indices.Count;
        _totalBatchedPrimitives += indices.Count / 3;
    }
}