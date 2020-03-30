﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    public Transform point;
    public bool press = false;
    public Transform piece;
    private Vector3 startPos;
    public bool collision = false;
    public GameObject checker;
    public string nameTag;
    public static int piecesCompleted = 0;
    public int alternativeAngleCorrect = 999, alternativeAngleCorrect2 = 999;
    public Transform myParent;
    public RotateController rotateController;
    private void Start()
    {
        startPos = piece.transform.position;
        myParent = transform.parent;
        rotateController = GetComponentInParent<RotateController>();
    }
    public void MovePiece()
    {
        if(!press)
        {
            press = true;
            rotateController.currentDraggingPiece = myParent.gameObject;
        }
        else
        {
            press = false;
            rotateController.currentDraggingPiece = null;
        }
    }
    private void Update()
    {
        if(press)
        {
            piece.transform.position = new Vector3(point.position.x, point.position.y, piece.transform.position.z);
        }
        else
        {
            if (!collision)
            {
                piece.transform.position = startPos;
            }
            else
            {
                if (myParent.transform.localScale == checker.transform.localScale)
                {
                    if ((int)myParent.transform.rotation.eulerAngles.z == (int)checker.transform.rotation.eulerAngles.z || (int)myParent.transform.rotation.eulerAngles.z == alternativeAngleCorrect || (int)myParent.transform.rotation.eulerAngles.z == alternativeAngleCorrect2)
                    {
                        myParent.transform.position = new Vector3(checker.transform.position.x, checker.transform.position.y, myParent.transform.position.z);
                        Destroy(checker.GetComponent<Outline>());
                        Destroy(gameObject.GetComponent<Outline>());
                        Destroy(checker);
                        if (piecesCompleted < 5)
                        {
                            piecesCompleted++;
                            Debug.Log(piecesCompleted);
                        }
                        else
                        {
                            StartCoroutine(FinishTangram());
                        }
                        this.enabled = false;
                    }
                    else
                    {
                        piece.transform.position = startPos;
                    }
                }

            }   
        }
    }
    IEnumerator FinishTangram()
    {
        Debug.Log("Puzzle terminado");
        yield return new WaitForSeconds(2.5f);
        Debug.Log("Realizando animacion");
        yield return null;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(nameTag)) //Cada pieza debe tener el nameTag correspondiente al mismo nombre que debe tener el Tag de la pieza a armar
        {
            collision = true;
            checker = other.transform.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(nameTag))
        {
            collision = false;
            checker = null;
        }
    }
}
