using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmBoss_Arm : MonoBehaviour
{

    public int healthBarIndex = 0;
    public ArmHealth armHealthBar;

    [SerializeField] Transform[] shootingPositions;
    [SerializeField] Transform[] shootingTarget;

    Transform mainTransform;

    public int armHealth = 500;
    public int scoreAmount = 500;

    public float spinSpeed;
    public float spin = 0;

    public float shootTimeMax;
    private float shootTimer = 0;
    private bool playSound = true;

    private bool spunUp = false;

    private void Awake()
    {
        armHealthBar = FindObjectOfType<ArmBossHealthBars>().GetHealthBar(healthBarIndex);
        armHealthBar.Show();
    }

    void Start()
    {
        mainTransform = GetComponentInParent<Transform>();
        shootTimer = shootTimeMax;

        armHealthBar.UpdateHealth(armHealth);
    }

    void Update()
    {
        HandleRotation();
        ProcessFiring();
    }

    private void HandleRotation()
    {
        spin += spinSpeed / 5;

        transform.eulerAngles = new Vector3(
            transform.rotation.eulerAngles.x,
            transform.rotation.eulerAngles.y,
            spin);
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
            shootTimer = shootTimeMax;

            if (playSound)
            {
                SoundManager.Instance.PlaySound(SoundManager.Sounds.PlasmaShot);
            }
            else { playSound = !playSound; }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerProjectile")
        {
            armHealth--;

            armHealthBar.UpdateHealth(armHealth);

            if (armHealth <= 250)
            {
                GetComponentInParent<ArmBoss>().bossPhase = ArmBoss.BossPhase.SecondPhase;

                if (!spunUp)
                {
                    if (spinSpeed < 0) { spinSpeed -= 2; }
                    else { spinSpeed += 2; }

                    shootTimeMax -= 0.08f;
                    spunUp = true;
                }
            }

            if (armHealth <= 0)
            {
                armHealthBar.Hide();

                Scoreboard.Instance.AddScore(scoreAmount);

                Destroy(gameObject);

                GetComponentInParent<ArmBoss>().ArmLoss();

            }
            Destroy(collision.gameObject);
        }
    }
}
