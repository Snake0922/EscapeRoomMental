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
    private void Start()
    {
        puzzleEngranajesController = GetComponentInParent<PuzzleEngranajesController>();
    }
    private void Update()
    {
        if(puzzleEngranajesController.checking)
        {
            transform.RotateAround(transform.position, Vector3.back, (speed* puzzleEngranajesController.combinacionesActuales[identificador]) * Time.deltaTime);
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
