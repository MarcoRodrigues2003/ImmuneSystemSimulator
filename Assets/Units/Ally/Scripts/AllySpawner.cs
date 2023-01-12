using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllySpawner : MonoBehaviour
{
    public GameObject Macrophage;

    public float spawnCooldown;
    public float spawnTime;

    // Update is called once per frame
    void Update()
    {
        if(Time.time > spawnTime)
        {
            Instantiate(Macrophage, transform.position, Quaternion.identity);
            spawnTime = Time.time + spawnCooldown;
        }
    }
}
