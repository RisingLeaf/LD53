using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeTime : MonoBehaviour
{
    public float lifeTime = 1f;
    public float safeTime = 10f;
    void Start()
    {
        
    }

    void Update()
    {
        lifeTime -= Time.deltaTime;
        if(lifeTime <= 0f)
        {
            gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
            if(safeTime <= 0f)
            {
                Destroy(gameObject);
            }
            else
            {
                safeTime -= Time.deltaTime;
            }
        }
    }
}
