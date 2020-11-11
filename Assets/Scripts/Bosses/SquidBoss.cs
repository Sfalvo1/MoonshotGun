using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidBoss : MonoBehaviour
{
    public enum BossPhase
    {
        Intro,
        FirstPhase,
        SecondPhase,
    }

    public int bossHealth = 500;

    [Header("Shooting")]
    [SerializeField] Transform[] shootingPositions;
    [SerializeField] Transform[] laserPositions;
    public float shootTimerMax;
    private float shootTimer;

    [Header("Movement")]
    [SerializeField] Vector2 enterPosition;
    [SerializeField] Vector2[] firstPhasePositions;
    [SerializeField] Vector2[] secondPhasePositions;

    public BossPhase bossPhase;

    public Transform player;

    private int firstPhaseIndex;
    private int moveIndex = 0;

    void Start()
    {
        BossHealth.Instance.Show();
        BossHealth.Instance.UpdateHealth(bossHealth);

        shootTimer = shootTimerMax;

        player = GetComponent<Enemy>().player;

        bossPhase = BossPhase.Intro;
        
    }

    void Update()
    { 
        HandleAttack();
    }

    private void FixedUpdate()
    {
        HandlePhaseMovement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerProjectile")
        {
            bossHealth--;
            BossHealth.Instance.UpdateHealth(bossHealth);
            if (bossHealth <= 250)
            {
                bossPhase = BossPhase.SecondPhase;
            }
            if (bossHealth <= 0)
            {
                Destroy(gameObject);
                BossHealth.Instance.Hide();
            }
            Destroy(collision.gameObject);
        }
    }

    private void HandleAttack()
    {
        shootTimer -= Time.deltaTime;

        if (bossPhase != BossPhase.Intro && shootTimer <= 0)
        {
            foreach(Transform shotTransform in shootingPositions)
            {
                SquidProjectile.Create(shotTransform.position, player.position);
            }

            if (bossPhase == BossPhase.SecondPhase)
            { //Need to make a sub-timer for separating the lasers after they spawn. Otherwise, they'll all be on top of each other
                SquidLaserProjectile.Create(laserPositions[0].position, player.position);
                SquidLaserProjectile.Create(laserPositions[1].position, player.position);
            }

            shootTimer = shootTimerMax;
        }
    }

    private void HandlePhaseMovement()
    {
        if (bossPhase == BossPhase.Intro)
        {
            IntroMovement();
        }
        else if (bossPhase == BossPhase.FirstPhase)
        {
            FirstPhaseMovement();
        }
        else
        {
            SecondPhaseMovement();
        }
    }

    private void IntroMovement()
    {
        if (Vector2.Distance((Vector2)transform.position, enterPosition) > .1f)
        {
            transform.position = Vector2.MoveTowards((Vector2)transform.position, enterPosition, 3f * Time.deltaTime);
        }
        else
        {
            bossPhase = BossPhase.FirstPhase;
        }
    }

    private void FirstPhaseMovement()
    {
        if (firstPhaseIndex == 0)
        {
            if (Vector2.Distance(transform.position, new Vector2(6.5f, 3.5f)) > .1f)
            {
                transform.position += transform.right * 3f * Time.deltaTime;
            }
            else
            {
                firstPhaseIndex = 1;
            }
        }
        if (firstPhaseIndex == 1)
        {
            if (Vector2.Distance(transform.position, new Vector2(-6.5f, 3.5f)) > .1f)
            {
                transform.position -= transform.right * 3f * Time.deltaTime;
            }
            else
            {
                firstPhaseIndex = 0;
            }
        }
    }

    private void SecondPhaseMovement()
    {
        if (Vector2.Distance((Vector2)transform.position, firstPhasePositions[moveIndex]) > .1f)
        {
            transform.position = Vector2.MoveTowards((Vector2)transform.position, firstPhasePositions[moveIndex], 3f * Time.deltaTime);
        }
        if (Vector2.Distance((Vector2)transform.position, firstPhasePositions[moveIndex]) < .1f)
        {
            if (moveIndex == firstPhasePositions.Length)
            {
                moveIndex++;
            }
            else
            {
                moveIndex = 0;
            }
        }
    }

}
