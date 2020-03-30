using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePiece : MonoBehaviour
{
    public int anglesToRotate;

    public void RotateObject(GameObject piece)
    {
        piece.transform.Rotate(0, 0, anglesToRotate);
    }
}
