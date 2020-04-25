using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipPointerTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other != null && other.GetComponent<BSelect>() != null)
        {
            other.GetComponent<BSelect>().UndidoFlip();
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other != null && other.GetComponent<BSelect>() != null)
    //    {
    //        other.GetComponent<BSelect>().fueTocadoFlip = true;
    //    }
    //}
}