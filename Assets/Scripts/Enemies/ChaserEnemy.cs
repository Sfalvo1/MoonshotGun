using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserEnemy : MonoBehaviour
{

    [SerializeField] float shootTimerMax = 1f;
    float shootTimer;

    [SerializeField] float movementSpeed = 1f;
    [SerializeField] float chaseDistance = 3f;

    [SerializeField] SpriteRenderer spriteRenderer;

    private Transform player;

    public int scoreAmount;

    void Start()
    {
        player = GetComponent<Enemy>().player;

        shootTimer = shootTimerMax;
    }

    void Update()
    {

        if (player != null)
        {
            HandleMovement();
            HandleShooting();
            HandleRotation();
        }

    }

    private void HandleMovement()
    {
        if (Vector2.Distance(transform.position, player.position) > chaseDistance)
        {
            transform.position = Vector2.MoveTowards((Vector2)transform.position, player.position, movementSpeed * Time.deltaTime);
        }
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

    private void OnDestroy()
    {
        Scoreboard.Instance.AddScore(scoreAmount);
    }

}
