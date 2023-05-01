using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject bossSparks;
    [SerializeField] private float sparksXOff;
    [SerializeField] private float sparksMaxUp;
    [SerializeField] private float sparksMaxDown;
    [SerializeField] private float sparksCooldown;
    private float currentSparksCooldownA;
    private float currentSparksCooldownB;
    private float currentSparksCooldownC;

    [SerializeField] private Vector3 minP;
    [SerializeField] private Vector3 maxP;

    [SerializeField] private float speed;

    private Rigidbody2D rigidbody;

    private Vector3 currentDestination;

    public GameObject player;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        currentDestination = transform.position;
        currentSparksCooldownA = sparksCooldown;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime); 

        if(currentSparksCooldownA >= sparksCooldown)
        {
            float yOff = Random.Range(-sparksMaxDown, sparksMaxDown);
            GameObject spawned = Instantiate(
                bossSparks,
                new Vector3(
                    sparksXOff * Mathf.Sign(Random.Range(-1f, 1f)),
                    player.transform.position.y + yOff,
                    transform.position.z),
                Quaternion.identity);
            spawned.GetComponent<SpawnLifeTime>().offset = new Vector3(-spawned.transform.position.x, spawned.GetComponent<SpawnLifeTime>().offset.y, spawned.GetComponent<SpawnLifeTime>().offset.z);
            currentSparksCooldownA = 0f;
        }
        else
        {
            currentSparksCooldownA += Time.deltaTime;
        }
        if(currentSparksCooldownB >= sparksCooldown && player.GetComponent<Player>().difficulty >= 47)
        {
            float yOff = Random.Range(-sparksMaxDown, sparksMaxDown);
            GameObject spawned = Instantiate(
                bossSparks,
                new Vector3(
                    sparksXOff * Mathf.Sign(Random.Range(-1f, 1f)),
                    player.transform.position.y + yOff,
                    transform.position.z),
                Quaternion.identity);
            spawned.GetComponent<SpawnLifeTime>().offset = new Vector3(-spawned.transform.position.x, spawned.GetComponent<SpawnLifeTime>().offset.y, spawned.GetComponent<SpawnLifeTime>().offset.z);
            currentSparksCooldownB = 0f;
        }
        else
        {
            currentSparksCooldownB += Time.deltaTime;
        }
        if(currentSparksCooldownC >= sparksCooldown && player.GetComponent<Player>().difficulty >= 50)
        {
            float yOff = Random.Range(-sparksMaxDown, sparksMaxDown);
            GameObject spawned = Instantiate(
                bossSparks,
                new Vector3(
                    sparksXOff * Mathf.Sign(Random.Range(-1f, 1f)),
                    player.transform.position.y + yOff,
                    transform.position.z),
                Quaternion.identity);
            spawned.GetComponent<SpawnLifeTime>().offset = new Vector3(-spawned.transform.position.x, spawned.GetComponent<SpawnLifeTime>().offset.y, spawned.GetComponent<SpawnLifeTime>().offset.z);
            currentSparksCooldownC = 0f;
        }
        else
        {
            currentSparksCooldownC += Time.deltaTime;
        }
    }
}
