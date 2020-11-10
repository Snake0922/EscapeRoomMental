using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics.Eventing.Reader;
using UnityEditor;
using UnityEngine;

public class BloomAnim : MonoBehaviour
{
    public bool animBloom = false;
    public Material mat;
    public float velocidad;
    private float val = 1.6f;
    private Color col = new Color(1.6f, 2.15f, 0);

    private bool subiendo = true;

    private readonly int matID = Shader.PropertyToID("_EmissionColor");

    private void Update()
    {
        if (animBloom)
        {
            if (subiendo)
            {
                val += velocidad * Time.deltaTime;
            }
            else
            {
                val -= velocidad * Time.deltaTime;
            }
            if (val <= 1.6f)
            {
                subiendo = true;
            }
            else if (val >= 2.15f)
            {
                subiendo = false;
            }
            col.r = val;
            col.g = val;
            mat.SetColor(matID, col);
        }
        transform.Rotate(0, velocidad, 0);
    }
}