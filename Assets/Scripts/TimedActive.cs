using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedActive : MonoBehaviour
{
    private float active = 0f;
    private ParticleSystem particles;
    
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }

    public void SetActiveFor(float time)
    {
        active = time;
    }

    void Update()
    {
        if(active > 0f)
        {
            active -= Time.deltaTime;
            particles.enableEmission = true;
        }
        else
        {
            particles.enableEmission = false;
        }
    }
}
