using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject asteroidPrefab; 
    public float spawnRate = 4.0f; 
    public float spawnDistance = 10f; 
    public float spawnHeight = 5f; 

    private Transform playerTransform;
    private float timer;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; 
        timer = spawnRate;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnAsteroid();
            timer = spawnRate;
        }
    }

    void SpawnAsteroid()
    {
        Vector3 spawnPosition = playerTransform.position + (Vector3.up * spawnHeight);
        spawnPosition += Random.Range(-1f, 1f) > 0 ? Vector3.right * spawnDistance : Vector3.left * spawnDistance;

        Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
    }
}
