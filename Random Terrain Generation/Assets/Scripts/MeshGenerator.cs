﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator {
    
    // HeightMap tem a altura de cada vértice que gera vai difinir a posição de cada triângulo da mesh
    public static MeshData GenerateTerrainMesh(float [,] heightMap, float heightMultiplier, AnimationCurve heightCurve, int levelOfDetail) {

        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        // center the triangle
        float topLeftX = (width - 1) / -2f;
        float topLeftZ = (height - 1) / 2f;

        // if ? (then) set to value :(otherwise) set to value
        int meshSimplificationIncrement = (levelOfDetail == 0)?1:levelOfDetail * 2;
        int verticesPerLine = (width - 1) / meshSimplificationIncrement + 1;

        MeshData meshData = new MeshData(verticesPerLine, verticesPerLine);
        int vertexIndex = 0;

        for (int y = 0; y < height; y += meshSimplificationIncrement) {
            for (int x = 0; x < width; x += meshSimplificationIncrement) {

                // cria os vertices
                meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, heightCurve.Evaluate(heightMap[x,y]) * heightMultiplier, topLeftZ - y) ;
                meshData.uvs[vertexIndex] = new Vector2(x / (float) width, y / (float) height);

                // ignora as pontas do mapa e gera o quadrado contendo os dois triangulos da mesh
                if (x < width - 1 && y < height - 1) {
                    meshData.AddTriangle(vertexIndex, vertexIndex + verticesPerLine + 1, vertexIndex + verticesPerLine);
                    meshData.AddTriangle(vertexIndex + verticesPerLine + 1, vertexIndex, vertexIndex + 1);
                }

                vertexIndex += 1;

            }
        }

        return meshData;

    }

}

public class MeshData {

    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uvs;

    int triangleIndex;

    public MeshData(int meshWidth, int meshHeight) {
        vertices = new Vector3[meshWidth * meshHeight];
        uvs = new Vector2[meshWidth * meshHeight];
        triangles = new int[(meshWidth - 1) * (meshHeight - 1) * 6];
    }

    public void AddTriangle(int a, int b, int c) {
        triangles[triangleIndex] = a;
        triangles[triangleIndex + 1] = b;
        triangles[triangleIndex + 2] = c;
        triangleIndex += 3;
    }

    public Mesh CreateMesh() {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();

        return mesh;
    }

}
