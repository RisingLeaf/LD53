using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDependantUpdate : MonoBehaviour
{
    public Player player;
    public float velModifier = 1.0f;
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-player.horizontalSpeed * velModifier, GetComponent<Rigidbody2D>().velocity.y);
    }
}
