using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateController : MonoBehaviour
{
    public GameObject currentDraggingPiece;
    public float anglesToRotate = 45;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            currentDraggingPiece.transform.Rotate(0, 0, anglesToRotate);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentDraggingPiece.transform.Rotate(0, 0, -anglesToRotate);
        }
    }
}
