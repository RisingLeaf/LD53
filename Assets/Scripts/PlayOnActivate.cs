using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOnActivate : MonoBehaviour
{
    public AudioClip clip;
    void Start()
    {
        GetComponent<AudioSource>().PlayOneShot(clip, 0.7F);
    }
}
