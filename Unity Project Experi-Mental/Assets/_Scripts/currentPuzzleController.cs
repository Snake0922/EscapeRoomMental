using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class currentPuzzleController : MonoBehaviour
{
    public Apuntar apuntar;
    public int ID = 0; //1 para Tangram, 2 para Engranajes y 3 para lineas
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            apuntar.currentPuzzle = ID;
        }    
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            apuntar.currentPuzzle = 0;
        }
    }
}
