using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpawner : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private GameObject toSpawn;
    [SerializeField] private float upExtent = 5f;
    [SerializeField] private float downExtent = 5f;
    [SerializeField] private float chance = 0.1f;
    [SerializeField] private float cooldown = 1f;

    [SerializeField] private int activeOnDifficulty = 1;

    private float currentCooldown = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentCooldown >= cooldown && player.difficulty >= activeOnDifficulty && !player.bossFight)
        {
            float rnd = Random.Range(0f, 1f);
            if(rnd <= chance)
            {
                float yOff = Random.Range(-downExtent, upExtent);
                GameObject spawned = Instantiate(toSpawn, new Vector3(transform.position.x * (player.timeInverse ? -1 : 1), transform.position.y + yOff, Random.Range(-1, 1)), Quaternion.identity);
                spawned.GetComponent<SpawnLifeTime>().offset = new Vector3(spawned.GetComponent<SpawnLifeTime>().offset.x * (player.timeInverse ? -1 : 1), spawned.GetComponent<SpawnLifeTime>().offset.y, spawned.GetComponent<SpawnLifeTime>().offset.z);
                currentCooldown = 0f;
            }
        }
        else
        {
            currentCooldown += Time.deltaTime;
        }
    }
}
