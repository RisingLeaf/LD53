using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Director : MonoBehaviour
{
    [SerializeField] private GameObject text;
    [SerializeField] private GameObject textBox;

    private float visible = 0f;
    void Start()
    {
        GetComponent<Renderer>().enabled = false;
        text.GetComponent<TextMeshPro>().text = "";
        textBox.GetComponent<Renderer>().enabled = false;
    }

    public void ShowText(string textToShow, float time)
    {
        text.GetComponent<TextMeshPro>().text = textToShow;
        visible = time;
    }

    void Update()
    {
        if(visible > 0f)
        {
            GetComponent<Renderer>().enabled = true;
            textBox.GetComponent<Renderer>().enabled = true;
            visible -= Time.deltaTime;
        }
        else
        {
            GetComponent<Renderer>().enabled = false;
            text.GetComponent<TextMeshPro>().text = "";
            textBox.GetComponent<Renderer>().enabled = false;
        }
    }
}
