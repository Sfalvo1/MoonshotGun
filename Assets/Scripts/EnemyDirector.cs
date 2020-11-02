using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDirector : MonoBehaviour
{
    public Enemy[] enemies; // Change to lists in the future
    public Transform player;
    public float spawnTimerMax;
    private float spawnTimer;

    Vector2 spawnPosition;
    int randomX;

    // Start is called before the first frame update
    void Start()
    {
        GenerateSpawnPosition();
        spawnTimer = spawnTimerMax;
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;

        if(spawnTimer <= 0)
        {
            SpawnEnemy();
            GenerateSpawnPosition();
            spawnTimer = spawnTimerMax;
        }
    }

    private void SpawnEnemy()
    {
        int enemyNumber = UnityEngine.Random.Range(0, 3);
        Enemy spawnedEnemy = Instantiate(enemies[enemyNumber], spawnPosition, Quaternion.identity);
        spawnedEnemy.player = player;
    }

    private void GenerateSpawnPosition()
    {
        randomX = UnityEngine.Random.Range(-10, 10);
        spawnPosition = new Vector2(randomX, 6.5f);
    }
}
