using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarPuzzle : MonoBehaviour
{
    public PasoPuzzles pPuzzle;
    public string activarN;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Llave"))
        {
            switch (activarN)
            {
                case "lineas":
                    pPuzzle.ActivarLineas();
                    break;
                case "tangram":
                    pPuzzle.ActivarTangram();
                    break;
                case "engranajes":
                    pPuzzle.ActivarEngranajes();
                    break;
                default:
                    break;
            }
            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}