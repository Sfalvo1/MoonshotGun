using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonChip : MonoBehaviour
{
    [SerializeField] int ammoAmount = 4;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("colliding");

        if (collision.collider.tag == "Player")
        {
            collision.collider.GetComponent<PlayerController>().moonChunkAmount += ammoAmount;
            AmmoUI.Instance.SetAmmoText(collision.collider.GetComponent<PlayerController>().moonChunkAmount);
            Destroy(gameObject);

        }

    }

    //private void 
    //{
    //    print("Colliding");

    //    if(collision.tag == "Enemy")
    //    {
    //        Destroy(collision.gameObject);
    //        Destroy(gameObject);
    //    }

    //    if(collision.tag == "Player")
    //    {
    //        collision.GetComponent<PlayerController>().moonChunkAmount += 4;
    //        Destroy(gameObject);
    //    }
    //}
}
