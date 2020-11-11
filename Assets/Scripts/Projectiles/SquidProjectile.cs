using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidProjectile : MonoBehaviour
{
    [Tooltip("Higher number, slower projectile")] [SerializeField] int projectileSpeed = 50;

    public float lifeTime = 3f;
    private float lifeTimeTimer;


    Vector2 spawnPosition;
    Vector2 targetPosition;
    Vector2 moveDir;

    public static SquidProjectile Create(Vector2 spawnPosition, Vector2 targetPosition)
    {
        Transform pfProjectile = Resources.Load<Transform>("pfSquidProjectile");
        Transform projectileTransform = Instantiate(pfProjectile, spawnPosition, Quaternion.identity);

        SquidProjectile projectile = projectileTransform.GetComponent<SquidProjectile>();
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
        if (collision.tag == "Shield" && collision.GetComponent<Shield>().GetShieldAmount() > 0)
        {
            collision.GetComponent<Shield>().ShieldHit();
            Destroy(gameObject);
        }
        if (collision.tag == "Player")
        {
            if (!collision.GetComponent<PlayerController>().godMode)
            {
                Destroy(collision.gameObject);
            }
            Destroy(gameObject);
        }
        if (collision.tag == "Moon" || collision.tag == "MoonChip")
        {
            Destroy(gameObject);
        }
    }
}
