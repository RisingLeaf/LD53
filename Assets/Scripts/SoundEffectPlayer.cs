using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour
{
    
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip victorySound;
    [SerializeField] private AudioClip collide;
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Death()
    {
        audioSource.PlayOneShot(deathSound, 1.0F);
    }

    public void Victory()
    {
        audioSource.PlayOneShot(victorySound, 1.0F);
    }

    public void Collide()
    {
        audioSource.PlayOneShot(collide, 1.0F);
    }
}
