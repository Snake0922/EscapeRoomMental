using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LineController : MonoBehaviour
{
    public SoundManager sManager;
    public PasoPuzzles pPuzzle;
    [Space]
    public LineRenderer[] lRender;
    public RectTransform pointerOffset;
    public RectTransform pointerFlip;
    [Space]
    public Image puntoFinal;
    [Space]
    public Transform PadrePuntos;
    private Image[] puntos;
    [Space]
    public Transform PadrePuntosNecesarios;
    private BSelect[] puntosNecesarios;
    private int puntosNecesariosCount;
    private bool Finalizo = false;

    private List<Vector3> posiciones = new List<Vector3>();
    private List<Vector3> posicionesB = new List<Vector3>();
    private int posicionesCount = 1;
    private Vector3 _pos = new Vector3(0, 0, 0);
    [Space]
    public bool conectado = false;

    private void Awake()
    {
        puntos = PadrePuntos.GetComponentsInChildren<Image>();
        for (int i = 0; i < lRender.Length; i++)
        {
            lRender[i].positionCount = 1;
        }
        puntosNecesarios = PadrePuntosNecesarios.GetComponentsInChildren<BSelect>();
        puntosNecesariosCount = puntosNecesarios.Length;
        posicionesB.Add(new Vector3(0, 0, 0));

        for (int i = 0; i < puntos.Length; i++)
        {
            puntos[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < puntosNecesarios.Length; i++)
        {
            puntosNecesarios[i].gameObject.SetActive(false);
        }
        conectado = false;
    }

    public void EnablePuntos()
    {
        puntoFinal.enabled = true;
        for (int i = 0; i < puntos.Length; i++)
        {
            puntos[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < puntosNecesarios.Length; i++)
        {
            puntosNecesarios[i].gameObject.SetActive(true);
        }
        conectado = true;
    }

    public bool AgregarPunto(Vector3 pos, bool necesario)
    {
        if (posiciones.Contains(pos))
        {
            return false;
        }
        else
        {
            if (posicionesCount == 1)
            {
                posicionesB[0] = pos;
            }
            posiciones.Add(pos);
            posicionesB.Add(pos);
            posicionesCount += 1;
            for (int i = 0; i < lRender.Length; i++)
            {
                lRender[i].positionCount = posicionesCount;
            }
            for (int i = 0; i < posicionesCount - 1; i++)
            {
                posiciones[i] = posicionesB[i + 1];
            }
            for (int i = 0; i < lRender.Length; i++)
            {
                lRender[i].SetPositions(posiciones.ToArray());
            }

            if (!necesario)
            {
                for (int i = 0; i < puntos.Length; i++)
                {
                    puntos[i].enabled = false;
                }
            }

            return true;
        }
    }

    public bool ComprobarFinal(Vector3 pos)
    {
        int cuantosLleva = 0;
        for (int i = 0; i < puntosNecesariosCount; i++)
        {
            if (puntosNecesarios[i].activado)
            {
                cuantosLleva += 1;
            }
        }
        if (cuantosLleva == puntosNecesariosCount)
        {
            for (int i = 0; i < puntos.Length; i++)
            {
                puntos[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < lRender.Length; i++)
            {
                lRender[i].SetPosition(posicionesCount - 1, pos);
                pointerFlip.localPosition = pos;
            }
            Finalizo = true;
            sManager.LineasDeslizar(false);
            //Debug.Log("Finalizo");
            pPuzzle.TerminadoLineas();
            return true;
        }
        else
        {
            //Debug.Log("no papu");
            pPuzzle.ReponerLineas();
            return false;
        }
    }

    public bool QuitarPunto(Vector3 pos, bool necesario)
    {
        //posiciones.RemoveAt(posicionesCount);
        if (posiciones.Contains(pos) && posiciones.IndexOf(pos) == posicionesCount - 2)
        {
            //Debug.Log("szs2");
            //Debug.Log(posicionesCount - 2);
            //Debug.Log(posiciones.IndexOf(pos));
            posiciones.Remove(pos);
            posicionesB.Remove(pos);
            posicionesCount -= 1;
            for (int i = 0; i < lRender.Length; i++)
            {
                lRender[i].positionCount = posicionesCount;
                lRender[i].SetPositions(posiciones.ToArray());
            }

            if (!necesario)
            {
                for (int i = 0; i < puntos.Length; i++)
                {
                    puntos[i].enabled = false;
                }
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    private void LateUpdate()
    {
        if (!Finalizo)
        {
            if (posicionesCount > 1)
            {
                for (int i = 0; i < lRender.Length; i++)
                {
                    if (pointerOffset.localPosition.x < 558.5f && pointerOffset.localPosition.x > -552f
                     && pointerOffset.localPosition.y < 551f && pointerOffset.localPosition.y > -560f)
                    {
                        //vertical
                        if (pointerOffset.localPosition.x < 558.5f && pointerOffset.localPosition.x > 499f)
                        {
                            _pos.x = 522.5f;
                            _pos.y = pointerOffset.localPosition.y;
                        }
                        if (pointerOffset.localPosition.x < 239.5f && pointerOffset.localPosition.x > 172f)
                        {
                            _pos.x = 200.3f;
                            _pos.y = pointerOffset.localPosition.y;
                        }
                        if (pointerOffset.localPosition.x < -158.5f && pointerOffset.localPosition.x > -220.8f)
                        {
                            _pos.x = -194.7f;
                            _pos.y = pointerOffset.localPosition.y;
                        }
                        if (pointerOffset.localPosition.x < -496.5f && pointerOffset.localPosition.x > -552f)
                        {
                            _pos.x = -528f;
                            _pos.y = pointerOffset.localPosition.y;
                        }

                        //horizontal
                        if (pointerOffset.localPosition.y < 551f && pointerOffset.localPosition.y > 491f)
                        {
                            _pos.y = 523.9f;
                            _pos.x = pointerOffset.localPosition.x;
                        }
                        if (pointerOffset.localPosition.y < 231f && pointerOffset.localPosition.y > 165f)
                        {
                            _pos.y = 203f;
                            _pos.x = pointerOffset.localPosition.x;
                        }
                        if (pointerOffset.localPosition.y < -167f && pointerOffset.localPosition.y > -229f)
                        {
                            _pos.y = -194.6f;
                            _pos.x = pointerOffset.localPosition.x;
                        }
                        if (pointerOffset.localPosition.y < -503f && pointerOffset.localPosition.y > -560f)
                        {
                            _pos.y = -523.5f;
                            _pos.x = pointerOffset.localPosition.x;
                        }
                    }

                    lRender[i].SetPosition(posicionesCount - 1, _pos);
                    pointerFlip.localPosition = _pos;
                }
                sManager.LineasDeslizar(true);
                //Debug.Log("szs");
            }
        }
    }
}