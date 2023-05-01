using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrabSliderValue : MonoBehaviour
{
    void Start()
    {
        Slider slider = GetComponent<Slider>();
        slider.value = Mathf.Min(Mathf.Max(PlayerPrefs.GetFloat("speed"), 0.25f), 2f);
    }
}
