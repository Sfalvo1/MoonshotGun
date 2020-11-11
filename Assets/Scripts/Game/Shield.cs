using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public int shieldHealth = 5;

    public float shieldTimerMax = 1f;
    private float shieldTimer = 0f;

    [SerializeField] Transform shieldUI;

    [SerializeField] SpriteRenderer shieldSprite;
    Color tmp;

    private void Start()
    {
        shieldTimer = shieldTimerMax;
        tmp = shieldSprite.color;
        UpdateShieldUI();
    }

    private void UpdateShieldUI()
    {
        shieldUI.localScale = new Vector2(shieldHealth, shieldUI.localScale.y);
    }

    private void Update()
    {
        UpdateShieldAlpha();
        UpdateShields();

    }

    private void UpdateShields()
    {
        if (shieldHealth < 5)
        {
            shieldTimer -= Time.deltaTime;
            if (shieldTimer <= 0)
            {
                shieldHealth++;
                shieldTimer = shieldTimerMax;
                UpdateShieldUI();
            }
        }
    }

    private void UpdateShieldAlpha()
    {
        if (tmp.a >= 0f)
        {
            ChangeShieldAlpha();
        }
    }

    public void ChangeShieldAlpha()
    {
        shieldSprite.color = tmp;
        tmp.a -= .01f;
    }

    public void ShieldHit()
    {
        tmp.a = .8f;
        shieldHealth--;
        UpdateShieldUI();
    }

    public int GetShieldAmount()
    {
        return shieldHealth;
    }
}
