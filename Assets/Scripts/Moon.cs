using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moon : MonoBehaviour
{
    public MoonChip moonChipPrefab;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SpawnMoonChip(Vector2 hitPosition)
    {
        MoonChip moonChip = Instantiate(moonChipPrefab, hitPosition, Quaternion.identity);
        moonChip.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10f);
    }
}
