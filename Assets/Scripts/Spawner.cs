using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Player player;

    [SerializeField] private GameObject toSpawn;
    [SerializeField] private float lifeTime = 10f;
    [SerializeField] private float upExtent = 5f;
    [SerializeField] private float downExtent = 5f;
    [SerializeField] private float chance = 0.1f;
    [SerializeField] private float cooldown = 1f;
    [SerializeField] private float velModifier = 1f;

    [SerializeField] private int activeOnDifficulty = 1;

    [SerializeField] private bool timeInverted = false;

    private float currentCooldown = 0f;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(currentCooldown >= cooldown && player.difficulty >= activeOnDifficulty && player.timeInverse == timeInverted && !player.bossFight)
        {
            float rnd = Random.Range(0f, 1f);
            if(rnd <= chance)
            {
                float yOff = Random.Range(-downExtent, upExtent);
                GameObject spawned = Instantiate(toSpawn, new Vector3(transform.position.x, transform.position.y + yOff, Random.Range(-1, 1)), Quaternion.identity);
                spawned.GetComponent<Rigidbody2D>().velocity = new Vector2(-player.horizontalSpeed * velModifier, 0.0f);
                spawned.GetComponent<LifeTime>().lifeTime = lifeTime;
                spawned.GetComponent<PlayerDependantUpdate>().player = player;
                spawned.GetComponent<PlayerDependantUpdate>().velModifier = velModifier;
                if(spawned.GetComponent<OnlyExistInOneDirection>())
                    spawned.GetComponent<OnlyExistInOneDirection>().player = player;
                currentCooldown = 0f;
            }
        }
        else
        {
            currentCooldown += Time.deltaTime + Time.deltaTime * (Mathf.Abs(player.horizontalSpeed) / 10f);
        }
    }
}
