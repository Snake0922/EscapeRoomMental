using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class IntroVideo : MonoBehaviour
{
    private UniversalRenderPipelineAsset renderAsset;

    public VideoPlayer vPlayer;
    public RenderTexture rTexture;
    public float startWait;

    private bool start = false;

    public static float volumenMaster = 1;
    public static float graficasMaster = 0.9f;

    private void Awake()
    {
        rTexture.Release();

        renderAsset = GraphicsSettings.renderPipelineAsset as UniversalRenderPipelineAsset;
        PlayerPrefs.SetFloat("audioMaster", volumenMaster);
        AudioListener.volume = PlayerPrefs.GetFloat("audioMaster");
        PlayerPrefs.SetFloat("graficasMaster", graficasMaster);
        renderAsset.renderScale = PlayerPrefs.GetFloat("graficasMaster");
    }

    private void Start()
    {
        vPlayer.Pause();
    }

    private void Update()
    {
        if (start)
        {
            if (vPlayer.time >= 18)
            {
                vPlayer.time = 11;
            }
            //if (!end && vPlayer.time >= /*17.5f*/18.2f)
            //{
            //    end = true;
            //    Debug.Log("end");
            //    //Initiate.Fade("Lineas", Color.black, 0.5f);
            //    SceneManager.LoadScene("Lineas");

            //    Debug.Log("Corregir fade de escenas en vr");
            //}
        }
        else
        {
            startWait -= 0.1f * Time.deltaTime;
            if (startWait <= 0)
            {
                start = true;
                vPlayer.Play();
            }
        }
    }

    public void Iniciar()
    {
        SceneManager.LoadScene("Lineas");
    }

    public void CalidadGrafica(bool alto)
    {
        if (alto)
        {
            graficasMaster = 0.9f;
        }
        else
        {
            graficasMaster = 0.6f;
        }

        renderAsset.renderScale = graficasMaster;
        PlayerPrefs.SetFloat("graficasMaster", graficasMaster);
    }

    public void CambioVolumen(int val)
    {
        switch (val)
        {
            case 1:
                volumenMaster = 0;
                break;
            case 2:
                volumenMaster = 0.25f;
                break;
            case 3:
                volumenMaster = 0.5f;
                break;
            case 4:
                volumenMaster = 0.75f;
                break;
            case 5:
                volumenMaster = 1;
                break;
            default:
                volumenMaster = 1;
                break;
        }

        AudioListener.volume = volumenMaster;
        PlayerPrefs.SetFloat("audioMaster", volumenMaster);
    }
}