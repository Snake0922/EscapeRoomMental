using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnsietyController : MonoBehaviour
{
    public Slider ansietySlider;
    public float ansietyValue;
    public float seconds;
    public float ansietyIncrement;
    [Space]
    public GameObject particlesBoom;
    public ActivarAccion botonEmergencia;
    public MeshRenderer botonMat;

    private WaitForSecondsRealtime segundos;

    private int toquesBoton = 0;

    private void Awake()
    {
        segundos = new WaitForSecondsRealtime(seconds);
        particlesBoom.SetActive(false);
    }

    private void Start()
    {
        ansietySlider.value = ansietyValue;
    }

    public void IniciarAnsiedad()
    {
        StartCoroutine(AumentarAnsiedad());
    }

    IEnumerator AumentarAnsiedad()
    {
        yield return segundos;
        ansietyValue += ansietyIncrement;
        ansietySlider.value = ansietyValue;
        if (ansietyValue <= ansietySlider.maxValue)
        {
            StartCoroutine(AumentarAnsiedad());
        }
    }
    public void AnsietyLevel(int value)
    {
        ansietyValue += value;
        ansietySlider.value = ansietyValue;
    }

    public void ToqueBotonAnsiedad()
    {
        toquesBoton += 1;
        if (toquesBoton == 1)
        {
            ansietyValue = Mathf.Clamp(ansietyValue - (ansietySlider.maxValue / 2), 0, ansietySlider.maxValue);
        }
        if (toquesBoton == 2)
        {
            particlesBoom.SetActive(true);
            botonEmergencia.enabled = false;
            botonMat.material.SetColor("_EmissionColor", Color.black);
        }
    }
}
