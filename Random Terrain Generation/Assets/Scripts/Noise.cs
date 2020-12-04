using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise {
   
    // Terá vários argumentos como lacunaridade e persistencia
    // mapWidth, mapHeight - dimensoes do mapa
    // scale - scale do noise
    public static float [,] GanerateNoiseMap (int mapWidth, int mapHeight, float scale) {

        // cria o marray nas dimensões do mapa
        float[,] noiseMap = new float[mapWidth, mapHeight];

        // Evita divisões por 0
        if (scale <= 0) {
            scale = 0.0001f;
        }

        // passeia pelo mapa
        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {

                float sampleX = x / scale;
                float sampleY = y / scale;

                float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                noiseMap[x, y] = perlinValue;

            }
        }

        return noiseMap;

    }

}
