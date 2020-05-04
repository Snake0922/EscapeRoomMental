using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngranajeController : MonoBehaviour
{
    public int identificador; //cada engranaje debe tener este script y un identificador diferente de acuerdo cada identificador correspondiente al index del array del engranaje
    public bool Ready = false;
    private PuzzleEngranajesController puzzleEngranajesController;
    public float speed;
    public MeshRenderer boton;
    public Material amarillo, azul;
    public GameObject previousGear, nextGear;
    
    private void Start()
    {
        puzzleEngranajesController = GetComponentInParent<PuzzleEngranajesController>();
    }
    private void Update()
    {
        if(puzzleEngranajesController.checking)
        {
            transform.RotateAround(transform.position, Vector3.right, (speed* puzzleEngranajesController.combinacionesActuales[identificador]) * Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(Ready)
            {
                puzzleEngranajesController.combinacionesActuales[identificador] = 1;
                boton.material = amarillo;
            }
                
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(Ready)
            {
                puzzleEngranajesController.combinacionesActuales[identificador] = -1;
                boton.material = azul;
            }
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (Ready && previousGear!=null)
            {
                Vector3 currentPositionBeforeToChange = transform.position;
                transform.position = previousGear.transform.position;
                previousGear.transform.position = currentPositionBeforeToChange;
            }

        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (Ready && nextGear != null)
            {
                Vector3 currentPositionBeforeToChange = transform.position;
                transform.position = nextGear.transform.position;
                nextGear.transform.position = currentPositionBeforeToChange;
            }
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
    
}
