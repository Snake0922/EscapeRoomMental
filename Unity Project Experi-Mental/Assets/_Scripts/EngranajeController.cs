using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngranajeController : MonoBehaviour
{
    public int identificador; //cada engranaje debe tener este script y un identificador diferente de acuerdo cada identificador correspondiente al index del array del engranaje
    public bool Ready = false;
    private PuzzleEngranajesController puzzleEngranajesController;
    public GameObject previousGear, nextGear;
    public float speed;
    public Vector3 initialRotation;
    public string nameTagCorrectPosition;
    public SoundManager sManager;
    public Light myLight;
    [Header("Arrow")]
    public GameObject FlechaSentidoReloj;
    public GameObject FlechaSentidoContraReloj;
    
    [Header("PieceMovement")]
    public float lerpTime = 1.5f;
    public float currentLerpTime = 0;
    public bool keyHit = false;

    private void Start()
    {
        puzzleEngranajesController = GetComponentInParent<PuzzleEngranajesController>();
        initialRotation = transform.rotation.eulerAngles;
        myLight = GetComponentInChildren<Light>();
    }

    public void ResetGearPositions()
    {
        transform.eulerAngles = initialRotation;
    }
    private void Update()
    {
        if(puzzleEngranajesController.checking)
        {
            transform.RotateAround(transform.position, Vector3.right, (speed* puzzleEngranajesController.combinacionesActuales[identificador]) * Time.deltaTime);
            //sManager.GirarEngranaje();
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(Ready)
            {
                puzzleEngranajesController.combinacionesActuales[identificador] = 1;
                FlechaSentidoReloj.SetActive(true);
                FlechaSentidoContraReloj.SetActive(false);
                //sManager.CambiarRotacion();
                if(puzzleEngranajesController.combinacionesActuales[identificador]==puzzleEngranajesController.combinacionesCorrectas[identificador])
                {
                    AumentarLuz();
                }
                else
                {
                    DisminuirLuz();
                }
            }
                
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(Ready)
            {
                puzzleEngranajesController.combinacionesActuales[identificador] = -1;
                FlechaSentidoReloj.SetActive(false);
                FlechaSentidoContraReloj.SetActive(true);
                //sManager.CambiarRotacion();
                if (puzzleEngranajesController.combinacionesActuales[identificador] == puzzleEngranajesController.combinacionesCorrectas[identificador])
                {
                    AumentarLuz();
                }
                else
                {
                    DisminuirLuz();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (Ready && previousGear!=null)
            {
                Vector3 currentPositionBeforeToChange = transform.position;
                //sManager.MoverEngranaje();
                StartCoroutine(slide(transform, previousGear.transform.position));
                StartCoroutine(slide(previousGear.transform, currentPositionBeforeToChange));
            }

        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (Ready && nextGear != null)
            {
                Vector3 currentPositionBeforeToChange = transform.position;
                //sManager.MoverEngranaje();
                StartCoroutine(slide(transform, nextGear.transform.position));
                StartCoroutine(slide(nextGear.transform, currentPositionBeforeToChange));
            }
        }
    }
    private IEnumerator slide(Transform currentGear,Vector3 nextGear)
    {
        keyHit = true;
        while (keyHit)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime >= lerpTime)
            {
                keyHit = false;
                currentLerpTime = 0;
            }
            float Perc = currentLerpTime / lerpTime;
            currentGear.position = Vector3.Lerp(currentGear.transform.position, nextGear, Perc);
            yield return null;
        }
    }
    public void EnableBool()
    {
        Ready = true;
    }
    public void DisableBool()
    {
        Ready = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(nameTagCorrectPosition))
        {
            puzzleEngranajesController.currentPositionData[identificador] = 1;
            AumentarLuz();
        }

    }
    public void AumentarLuz()
    {
        sManager.MoverEngranaje();
        if (myLight.intensity == 0)
        {
            myLight.intensity = 1;
        }
        else if (myLight.intensity == 1)
        {
            myLight.intensity = 2;
        }
        else
        {
            return;
        }
    }
    public void DisminuirLuz()
    {
        sManager.MoverEngranaje();
        if (myLight.intensity == 0)
        {
            return;
        }
        else if (myLight.intensity == 1)
        {
            myLight.intensity = 0;
        }
        else
        {
            myLight.intensity = 1;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(nameTagCorrectPosition))
        {
            puzzleEngranajesController.currentPositionData[identificador] = -1;
            DisminuirLuz();
        }
    }
}
