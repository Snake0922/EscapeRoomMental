using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class IntroVideo : MonoBehaviour
{
    public VideoPlayer vPlayer;
    public RenderTexture rTexture;
    public float startWait;

    private bool start = false;
    private bool end = false;

    private void Awake()
    {
        rTexture.Release();
    }

    private void Update()
    {
        if (start)
        {
            if (!end && vPlayer.time >= 17.5f)
            {
                end = true;
                Debug.Log("end");
                Initiate.Fade("Lineas", Color.black, 0.5f);
            }
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
}