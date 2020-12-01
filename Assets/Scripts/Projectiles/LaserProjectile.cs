using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    [Tooltip("Higher number, slower projectile")] [SerializeField] int projectileSpeed = 20;

    public float moveSpeed = 20f;
    public float lifeTime = 3f;
    private float lifeTimeTimer = 0;
    private int laserLife = 2;

    Vector2 spawnPosition;
    Vector2 targetPosition;
    Vector2 moveDir;

    Vector3 dir;
    float angle;

    [SerializeField] SpriteRenderer laserSprite;

    public static LaserProjectile Create(Vector2 spawnPosition, Vector2 targetPosition)
    {
        Transform pfProjectile = Resources.Load<Transform>("pfLaserProjectile");
        Transform projectileTransform = Instantiate(pfProjectile, spawnPosition, Quaternion.identity);

        LaserProjectile projectile = projectileTransform.GetComponent<LaserProjectile>();
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
        if (collision.tag == "Enemy")
        {
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlaySound(SoundManager.Sounds.ShipExplosion);
            }
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if(collision.tag == "MoonPiece")
        {
            MoonPiece moonPiece = collision.GetComponent<MoonPiece>();
            
            if (moonPiece.moonChipAmount > 0)
            {
                moonPiece.SpawnMoonChip((Vector2)transform.position);
                moonPiece.moonChipAmount--;
                Destroy(gameObject);
            }
            else
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
        if(collision.tag == "MoonChip")
        {
            laserLife--;
            if(laserLife <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
