using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Apuntar : MonoBehaviour
{
    public Image Point;
    private Animator anim;
    public Camera cMain;
    public Canvas cPointer;
    public float cPointerDist;
    public float maxDistance = 100;
    public LayerMask Layer;
    public EventSystem eventSys;
    private GameObject objMirando;
    public CharacterController cControler;
    public float velocidad = 1;
    private Vector3 move;
    public int currentPuzzle = 0;
    private void Awake()
    {
        anim = Point.GetComponent<Animator>();
        cPointerDist = cPointer.planeDistance;
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(cMain.transform.position, cMain.transform.forward, out hit, maxDistance, Layer))
        {
            if (objMirando != hit.collider.gameObject)
            {
                if (objMirando != null)
                {
                    if (objMirando.GetComponent<Outline2>() != null)
                    {
                        objMirando.GetComponent<Outline2>().enabled = false;
                    }
                    if (objMirando.GetComponent<Outline>() != null)
                    {
                        objMirando.GetComponent<Outline>().enabled = false;
                    }
                    if (currentPuzzle == 2)
                    {
                        if (objMirando.GetComponent<ActivarAccion>()!=null)
                        {
                            objMirando.GetComponent<ActivarAccion>().OnTriggerExit.Invoke();
                        }
                        
                    }
                }
               
                

                objMirando = hit.collider.gameObject;

                cPointer.planeDistance = cPointerDist;
                eventSys.SetSelectedGameObject(null);

                if (objMirando.layer == 5)
                {
                    if (objMirando.GetComponent<Button>() != null)
                    {
                        objMirando.GetComponent<Button>().Select();
                    }
                }
                else
                {
                    if(objMirando.GetComponent<Outline2>()!=null)
                    {
                        objMirando.GetComponent<Outline2>().enabled = true;
                        objMirando.GetComponent<ActivarAccion>().OnTriggerEnter.Invoke();
                    }
                    if (objMirando.GetComponent<Outline>() != null)
                    {
                        objMirando.GetComponent<Outline>().enabled = true;
                        objMirando.GetComponent<ActivarAccion>().OnTriggerEnter.Invoke();
                    }

                }
            }

            cPointer.planeDistance = hit.distance;

            Debug.DrawRay(cMain.transform.position, cMain.transform.forward * hit.distance, Color.yellow);

            if (Input.GetButtonDown("Fire1"))
            {
                anim.SetTrigger("Bump");

                if (objMirando.layer == 5 && objMirando.GetComponent<Button>() != null)
                {
                    objMirando.GetComponent<Button>().onClick.Invoke();
                    objMirando.GetComponent<Button>().Select();
                }
                else if (objMirando.GetComponent<ActivarAccion>() != null)
                {
                    objMirando.GetComponent<ActivarAccion>().Press.Invoke();
                }
            }
        }
        else
        {
            if (objMirando != null)
            {
                if (objMirando.GetComponent<Outline2>() != null)
                {
                    objMirando.GetComponent<Outline2>().enabled = false;
                    objMirando.GetComponent<ActivarAccion>().OnTriggerExit.Invoke();
                }
                if (objMirando.GetComponent<Outline>() != null)
                {
                    objMirando.GetComponent<Outline>().enabled = false;
                    objMirando.GetComponent<ActivarAccion>().OnTriggerExit.Invoke();
                }
                objMirando = null;
                cPointer.planeDistance = cPointerDist;
                eventSys.SetSelectedGameObject(null);
            }
            Debug.DrawRay(cMain.transform.position, cMain.transform.forward * maxDistance, Color.white);
        }

        move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = cControler.transform.TransformDirection(move);

        cControler.Move(move * velocidad * Time.unscaledDeltaTime);
    }
}