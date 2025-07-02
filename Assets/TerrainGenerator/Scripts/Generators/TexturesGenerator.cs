using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.MapGenerator.Generators
{
    public class TexturesGenerator : MonoBehaviour, IGenerator
    {
        public List<_Texture> textures = new List<_Texture>();

        public void Generate()
        {
            if (textures == null || textures.Count == 0)
                throw new NullReferenceException("Textures list not set");

            Terrain terrain = Terrain.activeTerrain;
            TerrainData terrainData = terrain.terrainData;

            TerrainLayer[] terrainLayers = new TerrainLayer[textures.Count];
            for (int i = 0; i < textures.Count; i++)
            {
                TerrainLayer layer = new TerrainLayer();
                layer.diffuseTexture = textures[i].Texture;
                layer.tileSize = textures[i].Tilesize;
                layer.name = $"Layer_{i}";
                terrainLayers[i] = layer;
            }

            terrainData.terrainLayers = terrainLayers;

            float[,,] splatmaps = new float[terrainData.alphamapWidth, terrainData.alphamapHeight, terrainData.alphamapLayers];
            float maxHeight = terrainData.size.y;

            for (int y = 0; y < terrainData.alphamapHeight; y++)
            {
                for (int x = 0; x < terrainData.alphamapWidth; x++)
                {
                    float normX = (float)x / (terrainData.alphamapWidth - 1);
                    float normY = (float)y / (terrainData.alphamapHeight - 1);

                    float worldX = normX * terrainData.size.x;
                    float worldZ = normY * terrainData.size.z;

                    float height = terrainData.GetInterpolatedHeight(normX, normY);
                    float heightNorm = height / maxHeight;

                    float angle = terrainData.GetSteepness(normX, normY);
                    float angleNorm = angle / 90f;

                    for (int i = 0; i < textures.Count; i++)
                    {
                        float val = 0f;
                        switch (textures[i].Type)
                        {
                            case 0:
                                val = textures[i].HeightCurve.Evaluate(heightNorm);
                                break;
                            case 1:
                                val = textures[i].AngleCurve.Evaluate(angleNorm);
                                break;
                        }
                        splatmaps[y, x, i] = val;
                    }

                    // Normalize so total = 1
                    float sum = 0f;
                    for (int i = 0; i < textures.Count; i++) sum += splatmaps[y, x, i];
                    if (sum > 0)
                    {
                        for (int i = 0; i < textures.Count; i++) splatmaps[y, x, i] /= sum;
                    }
                }
            }

            terrainData.SetAlphamaps(0, 0, splatmaps);
        }

        public void Clear()
        {
            textures = new List<_Texture>();
            Generate();
        }
    }

    [Serializable]
    public class _Texture
    {
        public Texture2D Texture;
        public Color Color;
        public Vector2 Tilesize = new Vector2(1, 1);
        public int Type; // 0 = Height, 1 = Angle
        public AnimationCurve HeightCurve = AnimationCurve.Linear(0f, 1f, 1f, 1f);
        public AnimationCurve AngleCurve = AnimationCurve.Linear(0f, 1f, 1f, 1f);
    }
}
