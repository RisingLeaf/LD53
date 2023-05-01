using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aura : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TimeVortex")
            Destroy(collision.gameObject);
        else if(collision.gameObject.tag == "Obstacle")
            Destroy(gameObject);
    }
}
