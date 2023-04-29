using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyExistInOneDirection : MonoBehaviour
{
    public Player player;
    [SerializeField] private bool inverted = false;
    void Update()
    {
        if(player.timeInverse != inverted)
            Destroy(gameObject);
    }
}
