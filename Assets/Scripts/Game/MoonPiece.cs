using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonPiece : MonoBehaviour
{
    [SerializeField] MoonChip moonChipPrefab;

    [SerializeField] public int moonChipAmount = 10;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnMoonChip(Vector2 hitPosition)
    {
        MoonChip moonChip = Instantiate(moonChipPrefab, hitPosition, Quaternion.identity);
        moonChip.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 10f);
    }
}
