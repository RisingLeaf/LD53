using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLifeTime : MonoBehaviour
{
    public float lifeTime = 1f;
    [SerializeField] private GameObject toSpawn;
    public Vector3 offset;
    void Start()
    {
        
    }

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0f)
        {
            GameObject spwnd = Instantiate(toSpawn, transform.position + offset, Quaternion.identity);
            spwnd.transform.parent = transform.parent;
            Destroy(gameObject);
        }
    }
}
