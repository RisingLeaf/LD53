using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDependantUpdate : MonoBehaviour
{
    public Player player;
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-player.horizontalSpeed, GetComponent<Rigidbody2D>().velocity.y);
    }
}
