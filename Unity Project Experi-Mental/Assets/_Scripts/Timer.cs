using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public float secondsTotals;
    public float minutesTotals;
    public TextMeshProUGUI TimerText;

    private readonly WaitForSecondsRealtime unSegundo = new WaitForSecondsRealtime(1);

    private void Start()
    {
        StartCoroutine(_Timer());
        TimerText.text = minutesTotals.ToString("00") + ":" + secondsTotals.ToString("00");
    }

    private IEnumerator _Timer()
    {
        yield return unSegundo;
        if (secondsTotals > 0)
        {
            secondsTotals--;
        }
        else
        {
            if (minutesTotals > 0)
            {
                minutesTotals--;
                secondsTotals = 59;
            }
        }
        TimerText.text = minutesTotals.ToString("00") + ":" + secondsTotals.ToString("00");
        StartCoroutine(_Timer());
    }
}
