using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    [SerializeField] GameObject objectToSpawn;
    [SerializeField] float cooldown;
    [SerializeField] bool isUnique;
    private float lastSpawn;
    private GameObject spawnedObject;

    void Update()
    {
        if (isUnique)
        {
            if (Time.time > lastSpawn + cooldown && spawnedObject == null)
            {
                spawnUnique();
            }
        }
        else {
            if (Time.time > lastSpawn + cooldown)
            {
                spawn();
            }
        }
    }

    private void spawn()
    {
        Instantiate(objectToSpawn, transform.position, transform.rotation);
        lastSpawn = Time.time;
    }
    private void spawnUnique()
    {
        spawnedObject = Instantiate(objectToSpawn, transform.position, transform.rotation);
        lastSpawn = Time.time;
    }
}