using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlyExistAboveLevel : MonoBehaviour
{
    [SerializeField] private int minLevel = 6;
    void Start()
    {
        if(PlayerPrefs.GetInt("level") < minLevel)
            Destroy(gameObject);
    }
}
