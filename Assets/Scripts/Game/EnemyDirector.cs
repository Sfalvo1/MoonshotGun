using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDirector : MonoBehaviour
{
    public Enemy[] enemies; // Change to lists in the future
    public Transform player;

    public Enemy bossPrefab;
    public Vector2 bossSpawnPoint;

    public float bossTimerMax;
    private float bossTimer;
    private bool bossActivated = false;

    public float spawnTimerMax;
    private float spawnTimer;

    Vector2 spawnPosition;
    int randomX;

    // Start is called before the first frame update
    void Start()
    {
        GenerateSpawnPosition();
        spawnTimer = spawnTimerMax;
        bossTimer = bossTimerMax;
        player = FindObjectOfType<PlayerController>().GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        bossTimer -= Time.deltaTime;

        if(spawnTimer <= 0 && !bossActivated)
        {
            SpawnEnemy();
            GenerateSpawnPosition();
            spawnTimer = spawnTimerMax;
        }

        if(bossTimer <= 0 && !bossActivated)
        {
            Enemy boss = Instantiate(bossPrefab, bossSpawnPoint, Quaternion.identity);
            boss.player = player;
            bossActivated = true;
        }
    }

    private void SpawnEnemy()
    {
        int enemyNumber = UnityEngine.Random.Range(0, enemies.Length);
        Enemy spawnedEnemy = Instantiate(enemies[enemyNumber], spawnPosition, Quaternion.identity);
        if (player != null)
        {
            spawnedEnemy.player = player;
        }
    }

    private void GenerateSpawnPosition()
    {
        randomX = UnityEngine.Random.Range(-10, 10);
        spawnPosition = new Vector2(randomX, 6.5f);
    }
}
