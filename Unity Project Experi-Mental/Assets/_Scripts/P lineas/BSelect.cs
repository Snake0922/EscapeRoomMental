using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore;
using UnityEngine.UI;

public class BSelect : MonoBehaviour
{
    public bool inicial = false;
    public bool final = false;
    public bool necesarios = false;
    [Space]
    public bool activado = false;
    public bool activado2 = false;
    public LineController lController;
    public UnityEvent PosibilesSiguientes;
    //public UnityEvent PosiblesAnteriores;
    [Space]
    [ColorUsage(true, false)]
    public Color32 blanco;
    [ColorUsage(true, false)]
    public Color32 rojo;

    private Image imagen;

    private void Awake()
    {
        imagen = GetComponent<Image>();
        if (!inicial && !necesarios)
        {
            imagen.enabled = false;
        }
        imagen.color = blanco;
    }

    public void Undido()
    {
        if (!lController.conectado && inicial)
        {
            lController.EnablePuntos();
        }
        if (!activado2)
        {
            if (/*imagen.enabled == true && */!activado)
            {
                if (!final)
                {
                    if (lController.AgregarPunto(transform.localPosition, necesarios))
                    {
                        activado = true;
                        if (!necesarios)
                        {
                            imagen.enabled = false;
                        }
                        imagen.color = rojo;
                        PosibilesSiguientes.Invoke();
                    }
                }
                else
                {
                    if (lController.AgregarPunto(transform.localPosition, necesarios))
                    {
                        activado = true;
                        imagen.color = rojo;
                    }
                    lController.ComprobarFinal(transform.localPosition);
                }
            }
            else
            {
                if (!inicial)
                {
                    if (lController.QuitarPunto(transform.localPosition, necesarios))
                    {
                        PosibilesSiguientes.Invoke();
                        activado = false;
                        imagen.color = blanco;
                        if (!necesarios)
                        {
                            if (!final)
                            {
                                imagen.enabled = false;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            if (!inicial && imagen.color == rojo)
            {
                activado2 = false;
                if (lController.QuitarPunto(transform.localPosition, necesarios))
                {
                    PosibilesSiguientes.Invoke();
                    activado = false;
                    imagen.color = blanco;
                }
            }
            else
            {
                UnityEngine.Debug.Log("choque");
            }
        }
    }

    public void UndidoFlip()
    {
        if (!activado2)
        {
            activado2 = true;
        }
        else
        {
            activado2 = false;
        }
    }

    public void ActivarImage()
    {
        imagen.enabled = true;
    }
}