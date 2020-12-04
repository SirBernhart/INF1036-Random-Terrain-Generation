using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour {

    // Cria uma textura e aplica ela em um plano da Scene
    public Renderer textureRenderer;

    public void DrawTexture (Texture2D texture) {

        
        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }
   
}
