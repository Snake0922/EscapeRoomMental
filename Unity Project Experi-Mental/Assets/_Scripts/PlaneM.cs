using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneM : MonoBehaviour
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
        if (offset2.x > 500)
        {
            offset2.x = 0;
        }
        if (offset2.y > 500)
        {
            offset2.y = 0;
        }
        offset2 += new Vector2(0.1f * offset.x, 0.1f * offset.y);
        mat.SetTextureOffset(mainT, offset2);
    }
}