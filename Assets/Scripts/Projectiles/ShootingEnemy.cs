using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    [SerializeField] float shootTimerMax = 1f;
    [SerializeField] float movementSpeed = 1f;
    [SerializeField] SpriteRenderer spriteRenderer;

    public int scoreAmount;

    Vector2 startPosition;
    Vector2 endPoint;

    private Transform player;

    float shootTimer;

    void Start()
    {
        player = GetComponent<Enemy>().player;

        shootTimer = shootTimerMax;

        startPosition = gameObject.transform.position;
        endPoint = new Vector2(transform.position.x, -6);
    }

    void Update()
    {
        if (player != null)
        {
            HandleShooting();
            HandleRotation();
        }

        transform.position += -transform.up * movementSpeed * Time.deltaTime;

        //HandleMovement();
    }

    private void HandleShooting()
    {
        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0)
        {
            PlasmaProjectile.Create(transform.position, player.transform.position);
            shootTimer = shootTimerMax;
        }
    }

    private void HandleRotation()
    {
        Vector3 dir = transform.position - player.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        spriteRenderer.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private void HandleMovement()
    {
        //float step = movementSpeed * Time.deltaTime;

        //transform.position = Vector2.MoveTowards((Vector2)transform.position, endPoint, step);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Scoreboard.Instance.AddScore(scoreAmount);
    }
}
