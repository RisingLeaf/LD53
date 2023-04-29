using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parralax : MonoBehaviour
{
    [SerializeField] private Player player;

    Material mat;
    float distance;

    [Range(0f, 0.5f)]
    public float speed = 0.2f;

    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        distance += Time.deltaTime * (speed * (1f + (player.horizontalSpeed / 20f))) * (player.timeInverse ? -1f : 1f);
        mat.SetTextureOffset("_MainTex", Vector2.right * distance);
    }
}
