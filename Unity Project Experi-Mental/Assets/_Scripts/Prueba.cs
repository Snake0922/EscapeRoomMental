using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prueba : MonoBehaviour
{
    public Transform camara;
    private bool agarrado;
    private Transform original;

    private void Awake()
    {
        original = transform.parent;
    }

    public void DejarseMover()
    {
        if (agarrado)
        {
            agarrado = false;
            transform.parent = original;
        }
        else
        {
            agarrado = true;
            transform.parent = camara;
        }
    }
}