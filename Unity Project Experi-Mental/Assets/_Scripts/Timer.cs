using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float secondsTotals;
    public float minutesTotals;
    public Text TimerText;
    private void Start()
    {
        StartCoroutine(_Timer());
        TimerText.text = minutesTotals.ToString("00") + ":" + secondsTotals.ToString("00");
    }

    private IEnumerator _Timer()
    {
        yield return new WaitForSeconds(1);
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
        StartCoroutine("_Timer");
    }
}
