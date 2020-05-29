using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public Slider sAnsiedad;
    public AudioSource reloj;
    public AudioSource effectos;
    private bool relojPlay;
    [Header("corazon")]
    public AudioSource respiraciones;
    public AudioSource corazones;
    [Space]
    public AudioClip respiracion1;
    private bool respiracion1Play;
    public AudioClip respiracion2;
    private bool respiracion2Play;
    public AudioClip respiracion3;
    private bool respiracion3Play;
    public AudioClip respiracion4;
    private bool respiracion4Play;
    [Space]
    public AudioClip corazon1;
    public AudioClip corazon2;
    [Space]
    public AudioClip llaves;
    public AudioClip win;
    [Header("tangram")]
    public AudioClip tangram_agarrar;
    public AudioClip tangram_SiEncaja;
    public AudioClip tangram_NoEncaja;
    public AudioClip tangramTerminado;
    [Header("lineas")]
    public AudioSource lineas;
    public AudioClip lineas_pasarXpunto;
    public AudioClip lineas_deslizando;
    private bool lineasDeslizePlay;
    public AudioClip lineas_pasador;
    [Header("Engranajes")]
    public AudioClip moverEngranaje;
    public AudioClip engranajeCorrecto;
    public AudioClip engranajeIncorrecto;
    public AudioClip doorOpening;
    [Header("Voices")]
    public AudioSource[] voices;
    public AudioMixer Distortion;
    public AudioMixerSnapshot Phase1, Phase2;
    public int PhaseDistortion = 0;
    private void Update()
    {
        Debug.Log("revisar que paso con estos corazones y sonidos :v");

        if (!relojPlay && sAnsiedad.value >= 240)
        {
            reloj.Play();
            relojPlay = true;
        }

        if (!respiracion1Play && sAnsiedad.value >= 0)
        {
            respiraciones.clip = respiracion1;
            corazones.clip = corazon1;
            respiraciones.Play();
            corazones.Play();
            respiracion1Play = true;
        }
        if (!respiracion2Play && sAnsiedad.value >= 75)
        {
            respiraciones.clip = respiracion2;
            respiraciones.Play();
            respiracion2Play = true;
        }
        if (!respiracion3Play && sAnsiedad.value >= 150)
        {
            respiraciones.clip = respiracion3;
            corazones.clip = corazon2;
            respiraciones.Play();
            corazones.Play();
            respiracion3Play = true;
        }
        if (!respiracion4Play && sAnsiedad.value >= 225)
        {
            respiraciones.clip = respiracion4;
            respiraciones.Play();
            respiracion4Play = true;
        }
    }

    public void SostenerLlave()
    {
        effectos.PlayOneShot(llaves);
    }

    public void TangramAgarrar()
    {
        effectos.PlayOneShot(tangram_agarrar);
    }

    public void TangramEncaja()
    {
        effectos.PlayOneShot(tangram_SiEncaja);
    }
    public void TangramNoEncaja()
    {
        effectos.PlayOneShot(tangram_NoEncaja);
    }

    public void LineasXpunto()
    {
        lineas.PlayOneShot(lineas_pasarXpunto);
    }


    public void LineasDeslizar(bool on)
    {
        if (on)
        {
            if (!lineasDeslizePlay)
            {
                lineas.clip = lineas_deslizando;
                lineas.Play();
                lineasDeslizePlay = true;
            }
        }
        else
        {
            lineas.Stop();
            lineas.PlayOneShot(lineas_pasador);
        }
    }

    public void MoverEngranaje()
    {
        effectos.PlayOneShot(moverEngranaje);
    }
    public void EngranajeCorrecto()
    {
        effectos.PlayOneShot(engranajeCorrecto);
    }
    public void OpenDoor()
    {
        effectos.PlayOneShot(doorOpening);
    }
    public void PlayWinSound()
    {
        OpenDoor();
        StartCoroutine(WinGame(0.5f));
    }
    IEnumerator WinGame(float delay)
    {
        yield return new WaitForSeconds(delay);
        effectos.PlayOneShot(win);
    }
    public void EngranajeIncorrecto()
    {
        effectos.PlayOneShot(engranajeIncorrecto);
    }
    public void TangramTerminado()
    {
        effectos.PlayOneShot(tangramTerminado);
    }

    public void _Distortion(bool Good)
    {
        if(!Good)
        {
            int voicePlayed = Random.Range(2, voices.Length);
            if(!voices[voicePlayed].isPlaying)
            {
                voices[voicePlayed].Play();
            }
            
        }
        else
        {
            int voicePlayed = Random.Range(0, 2);
            if (!voices[voicePlayed].isPlaying)
                voices[voicePlayed].Play();
        }
        
        switch(PhaseDistortion)
        {
            case 1:
                {
                    Phase1.TransitionTo(0.25f);
                    break;
                }
            case 2:
                {
                    Phase2.TransitionTo(0.25f);
                    break;
                }
        }   
    }  
}