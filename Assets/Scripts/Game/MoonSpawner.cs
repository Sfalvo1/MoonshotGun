using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonSpawner : MonoBehaviour
{
    [SerializeField] Moon moon;
    [SerializeField] MoonChip moonChip;

    public float spawnTimerMax;
    private float spawnTimer;

    public float moonChunkSpawnTimerMax;
    private float moonChunkSpawnTimer;

    Vector2 spawnPosition;
    float randomX;

    void Start()
    {
    }

    void Update()
    {
        SpawnMoon();
        SpawnMoonChunk();
    }

    private void SpawnMoon()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer < 0)
        {
            Instantiate(moon, GenerateSpawnPosition(), Quaternion.identity);
            spawnTimer = spawnTimerMax;
        }
    }

    private void SpawnMoonChunk()
    {
        moonChunkSpawnTimer -= Time.deltaTime;
        if (moonChunkSpawnTimer < 0)
        {
            MoonChip moonChipSpawned = Instantiate(moonChip, GenerateSpawnPosition(), Quaternion.identity);
            moonChipSpawned.spawnerSpawned = true;
            moonChunkSpawnTimer = moonChunkSpawnTimerMax;
        }
    }

    private Vector2 GenerateSpawnPosition()
    {
        randomX = UnityEngine.Random.Range(-10, 10);
        return spawnPosition = new Vector2(randomX, 6.5f);
    }
}
