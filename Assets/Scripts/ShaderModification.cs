using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderModification : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public float horizontalOffset;
    public Color color;
    public Texture2D texture;

    private void Update()
    {
        meshRenderer.material.SetFloat("_HorizontalOffset", horizontalOffset);
        meshRenderer.material.SetColor("_Color", color);
        meshRenderer.material.SetTexture("Texture2D", texture);
    }
}
