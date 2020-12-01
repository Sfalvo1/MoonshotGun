using UnityEngine;
using System.Collections;
using System;

public class Spawnling : MonoBehaviour
{
    private enum Phase
    {
        Intro,
        Main
    }

    public Transform shootingPosition;
    public Transform bossTransform;

    private int firstPhaseIndex = 0;

    public float shootTimerMax = 2;
    private float shootTimer;

    public int scoreAmount = 5;

    // If on the right side, make -1
    public int side = 1;

    private Phase phase;

    // Use this for initialization
    void Start()
    {
        phase = Phase.Intro;
        shootTimer = shootTimerMax;

        // SetRotation();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        ProcessFiring();
    }

    public void SetRotation()
    {
        Vector3 newRotation = new Vector3(0, 0, 180);
        transform.eulerAngles = newRotation;
    }

    private void HandleMovement()
    {
        if(phase == Phase.Intro)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(-10 * side, 2), Time.deltaTime * 7f);

            if(Vector2.Distance((Vector2)transform.position, new Vector2(-10 * side, 2)) < .1f)
            {
                phase = Phase.Main;
            }
        }
        else
        {
            MainMovement();
        }
    }

    private void ProcessFiring()
    {
        shootTimer -= Time.deltaTime;

        if (shootTimer < 0)
        {
            // Should make it so the laser just shoots to the right and keeps going straight
            Vector2 targetPosition = new Vector2((transform.position.x + 2f) * side, transform.position.y);

            SquidLaserProjectile.Create(shootingPosition.position, targetPosition);

            shootTimer = shootTimerMax;
            SoundManager.Instance.PlaySound(SoundManager.Sounds.PlasmaShot);
        }
    }

    private void MainMovement()
    {
        if (firstPhaseIndex == 0)
        {
            if (Vector2.Distance(transform.position, new Vector2(-10 * side, 5)) > .1f)
            {
                transform.position = Vector2.MoveTowards((Vector2)transform.position, new Vector2(-10 * side, 5), 5f * Time.deltaTime);
            }
            else
            {
                firstPhaseIndex = 1;
            }
        }
        if (firstPhaseIndex == 1)
        {
            if (Vector2.Distance(transform.position, new Vector2(-10 * side, -5)) > .1f)
            {
                transform.position = Vector2.MoveTowards((Vector2)transform.position, new Vector2(-10 * side, -5), 5f * Time.deltaTime);
            }
            else
            {
                firstPhaseIndex = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerProjectile" && bossTransform != null)
        {
            bossTransform.GetComponent<SpawnerBoss>().spawnlingCurrentAmount--;
            Scoreboard.Instance.AddScore(scoreAmount);
        }
    }

}
