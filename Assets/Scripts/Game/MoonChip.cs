using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonChip : MonoBehaviour
{
    [SerializeField] int ammoAmount = 4;

    public bool spawnerSpawned = false;
    public float movementSpeed = 3f;

    private void Update()
    {
        if (spawnerSpawned)
        {
            transform.position += -transform.up * movementSpeed * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "Player")
        {
            collision.collider.GetComponent<PlayerController>().moonChunkAmount += ammoAmount;
            AmmoUI.Instance.SetAmmoText(collision.collider.GetComponent<PlayerController>().moonChunkAmount);
            Destroy(gameObject);
        }

    }
}
