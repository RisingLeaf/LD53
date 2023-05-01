using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindSpawner : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private GameObject toSpawn;
    [SerializeField] private float rExtent = 5f;
    [SerializeField] private float lExtent = 5f;
    [SerializeField] private float chance = 0.1f;
    [SerializeField] private float cooldown = 1f;

    [SerializeField] private int activeOnDifficulty = 10;

    private float currentCooldown = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentCooldown >= cooldown && player.difficulty >= activeOnDifficulty && !player.timeInverse && !player.bossFight)
        {
            float rnd = Random.Range(0f, 1f);
            if(rnd <= chance)
            {
                float xOff = Random.Range(-lExtent, rExtent);
                GameObject spawned = Instantiate(toSpawn, new Vector3(transform.position.x + xOff, transform.position.y, Random.Range(-1, 1)), Quaternion.identity);
                currentCooldown = 0f;
            }
        }
        else
        {
            currentCooldown += Time.deltaTime;
        }
    }
}
