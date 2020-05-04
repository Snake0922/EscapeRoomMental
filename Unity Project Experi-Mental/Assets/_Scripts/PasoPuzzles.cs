using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class PasoPuzzles : MonoBehaviour
{
    public GameObject Llaves;
    public Transform posSostenida;
    public UnityEngine.UI.Slider barraEstres;
    public float bajadaXpuzzle = 25;
    private float chromaticVal = 0;
    private float contrastVal = 0;
    public Volume postV;
    private ChromaticAberration chromatic;
    private ColorAdjustments colorAd;
    private Vignette vig;
    public AnsietyController ansiController;

    private bool sostenerLlave;

    [Header("Engranajes")]
    public GameObject Engra_obj;
    public Transform posLlaves1;
    public GameObject outlineLlave1;
    [Header("Tangram")]
    public GameObject Tan_obj;
    public Transform posLlaves2;
    public GameObject outlineLlave2;
    [Header("Lineas")]
    public GameObject Lin_obj;
    public Transform posLlaves3;
    public GameObject outlineLlave3;

    private void Awake()
    {
        Llaves.SetActive(false);
        Engra_obj.SetActive(false);
        Lin_obj.SetActive(false);

        postV.profile.TryGet<ChromaticAberration>(out chromatic);
        postV.profile.TryGet<ColorAdjustments>(out colorAd);
        postV.profile.TryGet<Vignette>(out vig);
        chromatic.intensity.value = chromaticVal;
        colorAd.contrast.value = contrastVal;
        vig.intensity.value = contrastVal;

        outlineLlave1.SetActive(false);
        outlineLlave2.SetActive(false);
        outlineLlave3.SetActive(false);
    }

    private void Update()
    {
        if (barraEstres.value >= 50)
        {
            chromaticVal = ((barraEstres.value / 100) - 0.5f) * 2;
            chromatic.intensity.value = chromaticVal;
        }
        else if (chromatic.intensity.value != 0)
        {
            chromatic.intensity.value = 0;
        }
        if (barraEstres.value >= 75)
        {
            contrastVal = ((barraEstres.value / 100) - 0.75f) * 2;
            vig.intensity.value = contrastVal;
        }
        else if (colorAd.contrast.value != 0)
        {
            colorAd.contrast.value = 0;
        }
        colorAd.contrast.value = barraEstres.value * 0.75f;

        if (sostenerLlave)
        {
            Llaves.transform.position = posSostenida.position;
        }
    }
    
    public void BajarAnsiedad(float val)
    {
        barraEstres.value = Mathf.Clamp(barraEstres.value - val, 0, 100);
        ansiController.ansietyValue -= val;
    }

    public void sostenLlave()
    {
        sostenerLlave = true;
    }

    public void ActivarTangram()
    {
        Tan_obj.SetActive(true);
        sostenerLlave = false;
    }
    public void TerminadoTangram()
    {
        Llaves.SetActive(true);
        Llaves.transform.position = posLlaves2.position;
        Tan_obj.SetActive(false);
        //barraEstres.value = Mathf.Clamp(barraEstres.value - bajadaXpuzzle, 0, 100);
        //ansiController.ansietyValue -= bajadaXpuzzle;
        outlineLlave3.SetActive(true);
    }

    public void ActivarLineas()
    {
        Lin_obj.SetActive(true);
        sostenerLlave = false;
    }
    public void TerminadoLineas()
    {
        Llaves.SetActive(true);
        Llaves.transform.position = posLlaves3.position;
        Lin_obj.SetActive(false);
        //barraEstres.value = Mathf.Clamp(barraEstres.value - bajadaXpuzzle, 0, 100);
        //ansiController.ansietyValue -= bajadaXpuzzle;
        outlineLlave1.SetActive(true);
    }

    public void ActivarEngranajes()
    {
        Engra_obj.SetActive(true);
        sostenerLlave = false;
    }
    public void TerminadoEngranajes()
    {
        Llaves.SetActive(true);
        Llaves.transform.position = posLlaves1.position;
    }
}