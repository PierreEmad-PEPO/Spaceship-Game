using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject rocket;
    [SerializeField] float totalDistance = 0.15f;
    private Vector2 lastPos;
    
    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(lastPos, transform.position) >= totalDistance)
        {
            Instantiate(rocket, transform.position, transform.rotation);
            lastPos = transform.position;
        }
    }
}
