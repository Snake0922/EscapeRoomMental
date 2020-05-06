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

    private WaitForSecondsRealtime segundos;

    private void Awake()
    {
        segundos = new WaitForSecondsRealtime(seconds);
    }

    private void Start()
    {
        ansietySlider.value = ansietyValue;
        StartCoroutine(AumentarAnsiedad());
    }
    IEnumerator AumentarAnsiedad()
    {
        yield return segundos;
        ansietyValue += ansietyIncrement;
        ansietySlider.value = ansietyValue;
        if (ansietyValue <= 100)
        {
            StartCoroutine(AumentarAnsiedad());
        }
    }
    public void AnsietyLevel(int value)
    {
        ansietyValue += value;
        ansietySlider.value = ansietyValue;
    }
}
