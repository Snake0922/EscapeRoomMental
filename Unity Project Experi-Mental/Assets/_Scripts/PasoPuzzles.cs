using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

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
    public GameObject outlineLlave1;
    [Header("Tangram")]
    public GameObject Tan_obj;
    public GameObject outlineLlave2;
    [Header("Lineas")]
    public GameObject Lin_obj;
    public GameObject outlineLlave3;
    public Animator door;
    public Light endingLight;
    public SoundManager soundManager;

    [Space]
    public GameObject PuzzleLineas;
    public Camera cameraMain;
    public RectTransform Point;
    public Apuntar puntar;

    public Transform[] objetos;
    private ConstraintSource sour;
    private GameObject obLineas;

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

    private void Start()
    {
        obLineas = Instantiate(PuzzleLineas);
        objetos = obLineas.GetComponentsInChildren<Transform>(true);
        sour.weight = 1;
        sour.sourceTransform = Point;
        objetos[1].GetComponent<Canvas>().worldCamera = cameraMain;
        objetos[5].GetComponent<Canvas>().worldCamera = cameraMain;
        objetos[6].GetComponent<PositionConstraint>().SetSource(0, sour);
        obLineas.GetComponent<LineController>().sManager = GetComponent<SoundManager>();
        obLineas.GetComponent<LineController>().pPuzzle = this;
        objetos[44].GetComponent<currentPuzzleController>().apuntar = puntar;
        Lin_obj = obLineas;
    }

    public void ReponerLineas()
    {
        soundManager._Distortion2();
        Destroy(obLineas);
        obLineas = Instantiate(PuzzleLineas);
        obLineas.SetActive(true);
        objetos = obLineas.GetComponentsInChildren<Transform>(true);
        sour.weight = 1;
        sour.sourceTransform = Point;
        objetos[1].GetComponent<Canvas>().worldCamera = cameraMain;
        objetos[5].GetComponent<Canvas>().worldCamera = cameraMain;
        objetos[6].GetComponent<PositionConstraint>().SetSource(0, sour);
        obLineas.GetComponent<LineController>().sManager = GetComponent<SoundManager>();
        obLineas.GetComponent<LineController>().pPuzzle = this;
        objetos[44].GetComponent<currentPuzzleController>().apuntar = puntar;
        Lin_obj = obLineas;
    }

    private void Update()
    {
        if (barraEstres.value >= 25)
        {
            chromaticVal = ((barraEstres.value / 100) - 0.25f) * 2;
            chromatic.intensity.value = chromaticVal;
        }
        else if (chromatic.intensity.value != 0)
        {
            chromatic.intensity.value = 0;
        }
        if (barraEstres.value >= 25)
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
        Llaves.transform.position = outlineLlave2.transform.position;
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
        Llaves.transform.position = outlineLlave3.transform.position;
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
        StartCoroutine(FinishGame());
        //Llaves.SetActive(true);
        //Llaves.transform.position = outlineLlave1.transform.position;
        
    }
    IEnumerator FinishGame()
    {
        Engra_obj.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        door.SetTrigger("Win");
        soundManager.PlayWinSound();
        yield return new WaitForSeconds(0.5f);
        endingLight.enabled = true;
        yield return new WaitForSeconds(3.5f);
        SceneManager.LoadScene(0);
    }


}