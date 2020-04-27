using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleEngranajesController : MonoBehaviour
{
    public int[] combinacionesCorrectas; //la  cantidad de enteros serán equivalentes al mismo numero de engranajes, y será 1 para los engranajes que deban girar con el reloj, y -1 para los engranajes que deban girar contrario al reloj
    public int[] combinacionesActuales; //estos valores seran los que vayan equivaliendo dependiendo de las acciones del usuario. Al final se validaran si estos valores coinciden con los correctos para validar o no el puzzle
    public bool checking = false;
    public GameObject[] engranajes;
    public static PuzzleEngranajesController instance;
    private void Awake()
    {
        if(instance!=null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        engranajes = GameObject.FindGameObjectsWithTag("Engranaje");
    }

    public void CheckPuzzle()
    {
        checking = true;

        if(combinacionesActuales[0]==combinacionesCorrectas[0] &&
            combinacionesActuales[1] == combinacionesCorrectas[1] &&
            combinacionesActuales[2] == combinacionesCorrectas[3])

            {
                Debug.Log("puzzle correcto");
            }
            else
            {
                Debug.Log("Puzzle incorrecto");
            }
    }
}
