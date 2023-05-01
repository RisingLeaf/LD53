using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip intro;
    [SerializeField] private MusicPlayer musicNormal;
    [SerializeField] private MusicPlayer musicRift;
    [SerializeField] private Player player;
    private AudioSource audioSource;
    private bool started = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = intro;
        audioSource.loop = false;
        audioSource.Play();
    }

    void Update()
    {
        if(!audioSource.isPlaying)
        {
            if(!started)
            {
                musicNormal.StartPlaying();
                musicRift.StartPlaying();
            }
            if(player.timeInverse)
            {
                musicNormal.SetVolume(0f);
                musicRift.SetVolume(1f);
            }
            else
            {
                musicRift.SetVolume(0f);
                musicNormal.SetVolume(1f);
            }

        }
    }
}
