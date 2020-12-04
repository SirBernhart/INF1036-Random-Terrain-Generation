using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof(MapGenerator))]
public class MapGeneratorEditor : Editor {

    public override void OnInspectorGUI() {

        MapGenerator mapGen = (MapGenerator) target;

        // Se algum valor mudou - atualiza automaticamente quando algum dos valroes do inspector muda
        if (DrawDefaultInspector()) {
            if (mapGen.autoUpdate) {
                mapGen.GenerateMap();
            }
        }

        // Button generate a map
        if (GUILayout.Button("Generate")) {
            mapGen.GenerateMap();
        }
    }
}
