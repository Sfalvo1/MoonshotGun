using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonSpawner : MonoBehaviour
{
    [SerializeField] Moon moon;

    public float spawnTimerMax;
    private float spawnTimer;

    Vector2 spawnPosition;
    float randomX;

    // Start is called before the first frame update
    void Start()
    {
        spawnTimer = spawnTimerMax;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnMoon();

    }

    private void SpawnMoon()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            GenerateSpawnPosition();
            Instantiate(moon, spawnPosition, Quaternion.identity);
            spawnTimer = spawnTimerMax;
        }
    }

    private void GenerateSpawnPosition()
    {
        randomX = UnityEngine.Random.Range(-10, 10);
        spawnPosition = new Vector2(randomX, 6.5f);
    }
}
