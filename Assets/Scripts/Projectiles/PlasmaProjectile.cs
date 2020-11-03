using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaProjectile : MonoBehaviour
{
    [Tooltip("Higher number, slower projectile")] [SerializeField] int projectileSpeed = 20;

    public float moveSpeed = 20f;
    public float lifeTime = 3f;
    private float lifeTimeTimer;


    Vector2 spawnPosition;
    Vector2 targetPosition;
    Vector2 moveDir;

    public static PlasmaProjectile Create(Vector2 spawnPosition, Vector2 targetPosition)
    {
        Transform pfProjectile = Resources.Load<Transform>("pfPlasmaProjectile");
        Transform projectileTransform = Instantiate(pfProjectile, spawnPosition, Quaternion.identity);

        PlasmaProjectile projectile = projectileTransform.GetComponent<PlasmaProjectile>();
        projectile.SetTargetPosition(targetPosition);

        return projectile;
    }

    private void SetTargetPosition(Vector2 targetPosition)
    {
        this.targetPosition = targetPosition;
    }


    void Start()
    {
        moveDir = ((((Vector2)transform.position - targetPosition).normalized * -1) / projectileSpeed);
    }

    void Update()
    {
        transform.Translate(moveDir);

        lifeTimeTimer += Time.deltaTime;
        if (lifeTimeTimer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (!collision.GetComponent<PlayerController>().godMode)
            {
                Destroy(collision.gameObject);
            }
            Destroy(gameObject);
        }
    }

}
