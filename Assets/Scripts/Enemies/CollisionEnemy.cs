using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionEnemy : MonoBehaviour
{
    private Transform player;

    [SerializeField] float movementSpeed = 1f;
    [SerializeField] SpriteRenderer spriteRenderer;

    float shootTimer;
    private Vector2 moveDir;

    void Start()
    {
        player = GetComponent<Enemy>().player;
        moveDir = ((((Vector2)transform.position - (Vector2)player.position).normalized * -1) / movementSpeed);
    }

    void Update()
    {
        HandleRotation();
        HandleMovement();
    }

    private void HandleRotation()
    {
        Vector3 dir = transform.position - player.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        spriteRenderer.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private void HandleMovement()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, movementSpeed * Time.deltaTime);
    }
}
