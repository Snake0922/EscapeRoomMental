using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    public SoundManager Smanager;
    public PasoPuzzles pPuzzle;
    [Space]
    public Transform point;
    public bool press = false;
    public Transform piece;
    private Vector3 startPos;
    private Vector3 startRot;
    public bool _collisionTrigger = false;
    public bool _collision = false;
    public GameObject checker;
    public string nameTag;
    public static int piecesCompleted = 0;
    public int alternativeAngleCorrect = 999, alternativeAngleCorrect2 = 999;
    public Transform myParent;
    public RotateController rotateController;
    private void Start()
    {
        startPos = piece.transform.position;
        startRot = piece.transform.rotation.eulerAngles;
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
        if(press && !_collision)
        {
            piece.transform.position = new Vector3(point.position.x, point.position.y, piece.transform.position.z);  
        }
        else
        {
            if (!_collisionTrigger)
            {
                piece.transform.position = startPos;
                piece.transform.rotation = Quaternion.Euler(startRot);
                
            }
            else
            {
                if (myParent.transform.localScale == checker.transform.localScale)
                {

                    if ((int)myParent.transform.rotation.eulerAngles.z == (int)checker.transform.rotation.eulerAngles.z || (int)myParent.transform.rotation.eulerAngles.z == alternativeAngleCorrect || (int)myParent.transform.rotation.eulerAngles.z == alternativeAngleCorrect2)
                    {
                        myParent.transform.position = new Vector3(checker.transform.position.x, checker.transform.position.y, myParent.transform.position.z);
                        Destroy(checker.GetComponent<Outline2>());
                        Destroy(gameObject.GetComponent<Outline2>());
                        Destroy(checker);
                        if (piecesCompleted < 6)
                        {
                            piecesCompleted++;  
                            Smanager.TangramEncaja();
                            if (Smanager.PhaseDistortion > 0)
                            {
                                Smanager.PhaseDistortion--;  
                            }
                            Smanager._Distortion(true);
                            this.enabled = false;
                        }
                        else
                        {
                            StartCoroutine(FinishTangram());
                        }                         
                        this.enabled = false;
                    }
                    else
                    {
                        Debug.Log(myParent.transform.rotation.eulerAngles.z);
                        piece.transform.position = startPos;
                        piece.transform.rotation = Quaternion.Euler(startRot);
                        Smanager.TangramNoEncaja();
                        if (Smanager.PhaseDistortion < 2)
                        {
                            Smanager.PhaseDistortion++;   
                        }
                        Smanager._Distortion(false);
                    }
                }
            }   
        }
    }
    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
    IEnumerator FinishTangram()
    {
        Smanager.TangramTerminado();
        yield return new WaitForSeconds(1.5f);
        pPuzzle.TerminadoTangram();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(nameTag)) //Cada pieza debe tener el nameTag correspondiente al mismo nombre que debe tener el Tag de la pieza a armar
        {
            _collisionTrigger = true;
            checker = other.transform.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(nameTag))
        {
            _collisionTrigger = false;
            checker = null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _collision = true;
        piece.transform.position = startPos;
        piece.transform.rotation = Quaternion.Euler(startRot);
        press = false;
        Smanager.TangramNoEncaja();
    }
    private void OnCollisionExit(Collision collision)
    {
        _collision = false;
    }
}
