using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    [SerializeField] private Player player;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = new Vector2(transform.position.x - (player.horizontalSpeed * Time.deltaTime), transform.position.y);
    }
}
