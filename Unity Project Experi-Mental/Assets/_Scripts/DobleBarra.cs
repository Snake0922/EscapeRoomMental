using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DobleBarra : MonoBehaviour
{
    public Slider sliMaster;
    [Space]
    public Slider sli1;
    public Slider sli2;

    private void Update()
    {
        sli1.value = sliMaster.value;
        sli2.value = sliMaster.value;
    }
}