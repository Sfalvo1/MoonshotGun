using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidLaserProjectile : MonoBehaviour
{
    [Tooltip("Higher number, slower projectile")] [SerializeField] int projectileSpeed = 20;

    public float lifeTime = 3f;
    private float lifeTimeTimer = 0;
    private int laserLife = 2;

    Vector2 spawnPosition;
    Vector2 targetPosition;
    Vector2 moveDir;

    Vector3 dir;
    float angle;

    [SerializeField] SpriteRenderer laserSprite;

    public static SquidLaserProjectile Create(Vector2 spawnPosition, Vector2 targetPosition)
    {
        Transform pfProjectile = Resources.Load<Transform>("pfSquidLaserProjectile");
        Transform projectileTransform = Instantiate(pfProjectile, spawnPosition, Quaternion.identity);

        SquidLaserProjectile projectile = projectileTransform.GetComponent<SquidLaserProjectile>();
        projectile.SetTargetPosition(targetPosition);
        projectile.SetDirection(spawnPosition, targetPosition);

        return projectile;
    }

    private void SetDirection(Vector2 spawnPosition, Vector2 targetPosition)
    {
        dir = spawnPosition - targetPosition;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }

    private void SetTargetPosition(Vector2 targetPosition)
    {
        this.targetPosition = targetPosition;
    }


    void Start()
    {
        moveDir = ((((Vector2)transform.position - targetPosition).normalized * -1) / projectileSpeed);
        laserSprite.transform.rotation = Quaternion.AngleAxis(angle + 90f, Vector3.forward);
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
                GameOverUI.Instance.Show();
            }
            Destroy(gameObject);
        }
        if (collision.tag == "MoonChip")
        {
            laserLife--;
            if (laserLife <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
