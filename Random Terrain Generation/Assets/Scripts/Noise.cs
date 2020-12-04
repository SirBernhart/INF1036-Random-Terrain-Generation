using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise {
   
    // Terá vários argumentos como lacunaridade e persistencia
    // mapWidth, mapHeight - dimensoes do mapa
    // scale - scale do noise
    // octaves
    // persistance, lacunarity - valores de amplitude e lacunas entre um ruído e outro
    public static float [,] GanerateNoiseMap (int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistence, float lacunarity, Vector2 offset) {

        // cria o marray nas dimensões do mapa
        float[,] noiseMap = new float[mapWidth, mapHeight];

        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];

        // aplica os valores randomicos
        for (int i = 0; i < octaves; i++) {

            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) + offset.y;

            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        // Evita divisões por 0
        if (scale <= 0) {
            scale = 0.0001f;
        }

        // pega o valor mais alto e mais baixo do noise
        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        // scale zoom in in the center of the screen
        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

        // passeia pelo mapa
        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {

                float amplitude = 1;
                float frequency = 1;    // quanto maior a frequencia, mais separados são os ruídos
                float noiseHeight = 0;

                // faz o zoom do scale
                for (int i = 0; i < octaves; i++) {
                    float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x;
                    float sampleY = (y - halfHeight) / scale * frequency + octaveOffsets[i].y;

                    // * 2 - 1 -> permite que o perlin noise gere valores negativos também
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistence;
                    frequency *= lacunarity;   
                }

                if (noiseHeight > maxNoiseHeight) {
                    maxNoiseHeight = noiseHeight;
                } else if (noiseHeight < minNoiseHeight) {
                    minNoiseHeight = noiseHeight;
                }
                
                noiseMap[x, y] = noiseHeight;
            }
        }

        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }

        return noiseMap;

    }

}
