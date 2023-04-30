using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Director : MonoBehaviour
{
    //[SerializeField] private AudioSource audioSource;
    //[SerializeField] private AudioClip clip;
    //[SerializeField] private float volume=0.5f;

    [SerializeField] private GameObject text;
    [SerializeField] private GameObject textBox;

    private float visible = 0f;
    void Start()
    {
        transform.localScale = new Vector3(0f, 0f, 0f);
        text.GetComponent<TextMeshProUGUI>().text = "";
        textBox.transform.localScale = new Vector3(0f, 0f, 0f);
    }

    public void ShowText(string textToShow, float time)
    {
        //audioSource.PlayOneShot(clip, volume);
        text.GetComponent<TextMeshProUGUI>().text = textToShow;
        visible = time;
    }

    void Update()
    {
        if(visible > 0f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            textBox.transform.localScale = new Vector3(1f, 1f, 1f);
            visible -= Time.deltaTime;
        }
        else
        {
            transform.localScale = new Vector3(0f, 0f, 0f);
            text.GetComponent<TextMeshProUGUI>().text = "";
            textBox.transform.localScale = new Vector3(0f, 0f, 0f);
        }
    }
}
