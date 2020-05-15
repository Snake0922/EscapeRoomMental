﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleEngranajesController : MonoBehaviour
{
    public int[] combinacionesCorrectas; //la  cantidad de enteros serán equivalentes al mismo numero de engranajes, y será 1 para los engranajes que deban girar con el reloj, y -1 para los engranajes que deban girar contrario al reloj
    public int[] combinacionesActuales; //estos valores seran los que vayan equivaliendo dependiendo de las acciones del usuario. Al final se validaran si estos valores coinciden con los correctos para validar o no el puzzle
    public int[] currentPositionData; //1 si el engranaje esta en su posicion correcta, 2 si esta incorrecto


    public bool checking = false;
    public static PuzzleEngranajesController instance;
    public PasoPuzzles pasoPuzzles;
    public GameObject GearWhite, GearBlue, GearRed;
    public GameObject[] arrows;
    public Vector3[] posicionesEngranajesCorrectas = new Vector3[3];
    public SoundManager sManager;
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
        posicionesEngranajesCorrectas[0] = GearWhite.transform.position;
        posicionesEngranajesCorrectas[1] = GearBlue.transform.position;
        posicionesEngranajesCorrectas[2] = GearRed.transform.position;
        DefaultValues();
        
    }
    public void DefaultValues()
    {
        for (int i = 0; i < combinacionesActuales.Length; i++)
        {
            combinacionesActuales[i] = 1;
        }
        for (int i = 0; i < 3; i++)
        {
            arrows[i].SetActive(true);
        }
    }

    IEnumerator Wait(float time)
    {
        foreach (GameObject flechas in arrows)
        {
            flechas.SetActive(false);
        }
        yield return new WaitForSeconds(time);
        GearWhite.GetComponent<EngranajeController>().ResetGearPositions();
        GearBlue.GetComponent<EngranajeController>().ResetGearPositions();
        GearRed.GetComponent<EngranajeController>().ResetGearPositions();
        checking = false;
        DefaultValues();
    }
    public void CheckPuzzle()
    {
        checking = true;

        if(combinacionesActuales[0]==combinacionesCorrectas[0] &&
        combinacionesActuales[1] == combinacionesCorrectas[1] &&
        combinacionesActuales[2] == combinacionesCorrectas[2])
        {
            if(currentPositionData[0]==1 && currentPositionData[1] == 1 && currentPositionData[2] == 1)
            {
                Debug.Log("puzzle correcto");
                sManager.EngranajeCorrecto();
                StartCoroutine(Wait(2.5f));
                pasoPuzzles.TerminadoEngranajes();
            }
            else
            {
                Debug.Log("Puzzle incorrecto");
                sManager.EngranajeIncorrecto();
                StartCoroutine(Wait(2.5f));
            }
        }
        else
        {
            Debug.Log("Rotaciones incorrectas");
            sManager.EngranajeIncorrecto();
            foreach (GameObject flechas in arrows)
            {
                flechas.SetActive(false);
            }
            StartCoroutine(Wait(2.5f));
        }
    }
}
