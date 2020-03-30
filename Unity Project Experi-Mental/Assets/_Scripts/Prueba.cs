using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prueba : MonoBehaviour
{
    public enum Estado { NoEstoyColisionando, EstoyColisionando, MePresionaron}
    public Estado estado;
    private void Awake()
    {
        estado = Estado.NoEstoyColisionando;
    }

    public void Presionar()
    {
        estado = Estado.MePresionaron;
    }
    public void NoEstaColisionando()
    {
        estado = Estado.NoEstoyColisionando;
    }
    public void EstaColisionando()
    {
        estado = Estado.EstoyColisionando;
    }
}