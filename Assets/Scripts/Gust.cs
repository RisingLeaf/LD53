using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gust : MonoBehaviour
{
    public float lifeTime = 1f;
    [SerializeField] private GameObject toSpawn;
    [SerializeField] private Vector3 offset;
    void Start()
    {
        
    }

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0f)
        {
            GameObject spawned = Instantiate(toSpawn, transform.position + offset, Quaternion.identity);
            spawned.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 10f);
            Destroy(gameObject);
        }
    }
}
