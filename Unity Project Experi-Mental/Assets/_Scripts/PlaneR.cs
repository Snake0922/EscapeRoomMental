using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneR : MonoBehaviour
{
    public Vector2 offset;

    private Vector2 offset2;
    private Material mat;

    private readonly int mainT = Shader.PropertyToID("_BaseMap");

    private void Awake()
    {
        mat = GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        offset2 = new Vector2(0, Mathf.PerlinNoise(0, Time.time * offset.y));
        mat.SetTextureOffset(mainT, offset2 * offset.x);
    }
}