using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateController : MonoBehaviour
{
    public GameObject currentDraggingPiece;
    public float anglesToRotate = 45;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            currentDraggingPiece.transform.Rotate(0, 0, anglesToRotate);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            currentDraggingPiece.transform.Rotate(0, 0, -anglesToRotate);;
        }
    }
}
