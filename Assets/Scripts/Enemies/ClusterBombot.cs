using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterBombot : MonoBehaviour
{
    [SerializeField] Cluster clusterParent;

    public int scoreAmount;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        clusterParent.childCount--;

        if (collision.tag == "PlayerProjectile")
        {
            Destroy(collision.gameObject);

            if (clusterParent.childCount <= 0)
            {
                Destroy(clusterParent.transform.gameObject);
            }
            Destroy(gameObject);

        }
        else return;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && !collision.collider.GetComponent<PlayerController>().godMode)
        {
            Destroy(collision.gameObject);
        }
    }

}
