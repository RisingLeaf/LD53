using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DisplayHighscore : MonoBehaviour
{
    [SerializeField] private string attribute;
    [SerializeField] private string prefix;
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = prefix + PlayerPrefs.GetInt(attribute);
    }
}
