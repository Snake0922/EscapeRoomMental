using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasoPuzzles : MonoBehaviour
{
    public GameObject Llaves;

    [Header("Engranajes")]
    public GameObject Engra_obj;
    public Transform posLlaves1;
    [Header("Tangram")]
    public GameObject Tan_obj;
    public Transform posLlaves2;
    [Header("Lineas")]
    public GameObject Lin_obj;
    public Transform posLlaves3;

    private void Awake()
    {
        Llaves.SetActive(false);
        Engra_obj.SetActive(false);
        Lin_obj.SetActive(false);
    }

    public void TerminadoTangram()
    {
        Tan_obj.SetActive(false);
        Lin_obj.SetActive(true);
    }
    public void TerminadoLineas()
    {
        Lin_obj.SetActive(false);
        Engra_obj.SetActive(true);
    }
    public void TerminadoEngranajes()
    {

    }
}