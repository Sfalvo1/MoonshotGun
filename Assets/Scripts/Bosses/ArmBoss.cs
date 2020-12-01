using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmBoss : MonoBehaviour
{
    public enum BossPhase
    {
        Intro,
        FirstPhase,
        SecondPhase,
    }

    public BossPhase bossPhase;

    private int armAmount = 2;
    public int scoreAmount = 1000;


    private int firstPhaseIndex = 0;
    
    [Header("Shooting")]
    public float shootTimerMax;
    private float shootTimer;

    [Header("Movement")]
    [SerializeField] Vector2 enterPosition;
    [SerializeField] Vector2[] secondPhasePositions;
    private int moveIndex = 0;

    private void Start()
    {
        bossPhase = BossPhase.Intro;
    }

    private void Update()
    {
        HandlePhaseMovement();
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
                transform.position = Vector2.MoveTowards((Vector2)transform.position, new Vector2(6.5f, 3.5f), 3f * Time.deltaTime);
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
                transform.position = Vector2.MoveTowards((Vector2)transform.position, new Vector2(-6.5f, 3.5f), 3f * Time.deltaTime);
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

    public void ArmLoss()
    {
        armAmount--;
        if(armAmount == 0)
        {
            GetComponent<LevelManager>().LoadLevel();
        }
    }

}
