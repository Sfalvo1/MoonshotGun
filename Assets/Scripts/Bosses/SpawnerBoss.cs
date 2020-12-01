using UnityEngine;
using System.Collections;
using System;

public class SpawnerBoss : MonoBehaviour
{
    public enum BossPhase
    {
        Intro,
        FirstPhase,
        SecondPhase,
    }

    public BossPhase bossPhase;

    public int bossHealth = 500;
    public int scoreAmount = 1000;

    private int firstPhaseIndex = 0;

    [Header("Spawning")]
    public Spawnling spawnling;

    public int spawnlingCurrentAmount;
    public int spawnlingMax = 8;

    public Transform[] spawnPositions;
    private int spawnPositionIndex = 1;

    public float spawnTimerMax = 1;
    private float spawnTimer;

    private Transform bossTransform;

    [Header("Shooting")]
    public float shootTimerMax;
    private float shootTimer;

    public Transform[] shootingPositions;
    public Transform[] shootingTarget;

    private bool playSound = true;

    [Header("Movement")]
    [SerializeField] Vector2 enterPosition;
    [SerializeField] Vector2[] secondPhasePositions;
    private int moveIndex = 0;
    public float moveSpeed = 3f;

    private void Start()
    {
        BossHealth.Instance.Show();
        BossHealth.Instance.UpdateHealth(bossHealth);

        bossTransform = GetComponent<Transform>();
        // spawnTimer = spawnTimerMax;
        bossPhase = BossPhase.Intro;
    }

    private void Update()
    {
        HandlePhaseMovement();
        ProcessFiring();
        HandleSpawning();
    }

    private void HandleSpawning()
    {
        spawnTimer -= Time.deltaTime;

        if(spawnTimer < 0 && spawnlingCurrentAmount < spawnlingMax)
        {
            Spawnling spawned = Instantiate(spawnling, spawnPositions[spawnPositionIndex % 2].position, Quaternion.identity);

            spawned.bossTransform = bossTransform;

            if(spawnPositionIndex % 2 == 1)
            {
                spawned.side = -1;
                spawned.SetRotation();
            }

            spawnlingCurrentAmount++;
            spawnPositionIndex++;
            spawnTimer = spawnTimerMax;
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
        //else
        //{
        //    SecondPhaseMovement();
        //}

    }

    private void ProcessFiring()
    {
        shootTimer -= Time.deltaTime;

        if (shootTimer < 0)
        {
            for (int i = 0; i < 4; i++)
            {
                SquidLaserProjectile.Create(shootingPositions[i].position, shootingTarget[i].position);
            }

            shootTimer = shootTimerMax;

            if (playSound)
            {
                SoundManager.Instance.PlaySound(SoundManager.Sounds.PlasmaShot);
            }
            else { playSound = !playSound; }
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
            if (Vector2.Distance(transform.position, new Vector2(6.5f, 3f)) > .1f)
            {
                transform.position = Vector2.MoveTowards((Vector2)transform.position, new Vector2(6.5f, 3f), moveSpeed * Time.deltaTime);
            }
            else
            {
                firstPhaseIndex = 1;
            }
        }
        if (firstPhaseIndex == 1)
        {
            if (Vector2.Distance(transform.position, new Vector2(-6.5f, 3f)) > .1f)
            {
                transform.position = Vector2.MoveTowards((Vector2)transform.position, new Vector2(-6.5f, 3f), moveSpeed * Time.deltaTime);
            }
            else
            {
                firstPhaseIndex = 0;
            }
        }
    }

    private void SecondPhaseMovement()
    {
        if (Vector2.Distance((Vector2)transform.position, secondPhasePositions[moveIndex]) > .1f)
        {
            transform.position = Vector2.MoveTowards((Vector2)transform.position, secondPhasePositions[moveIndex], 4f * Time.deltaTime);
        }
        if (Vector2.Distance((Vector2)transform.position, secondPhasePositions[moveIndex]) < .1f)
        {
            if (moveIndex == secondPhasePositions.Length - 1)
            {
                moveIndex = 0;
            }
            else
            {
                moveIndex++;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerProjectile")
        {
            bossHealth--;
            BossHealth.Instance.UpdateHealth(bossHealth);

            if (bossHealth <= 250)
            {
                // bossPhase = BossPhase.SecondPhase;
                spawnlingMax = 12;
                moveSpeed += 2f;
                spawnTimerMax = .75f;
            }
            if (bossHealth <= 0)
            {
                BossHealth.Instance.Hide();

                Scoreboard.Instance.AddScore(scoreAmount);

                WinScreenUI.Instance.Show();
            }
            Destroy(collision.gameObject);
        }
    }

    private void OnDestroy()
    {
        Scoreboard.Instance.AddScore(scoreAmount);
    }

}
