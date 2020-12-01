using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmHealth : MonoBehaviour
{
    [SerializeField] Transform healthBarTransform;
    [SerializeField] Transform healthBarBackgroundTransform;

    public ArmHealth healthBar;
    public int healthBarIndex;

    void Start()
    {
        healthBar = FindObjectOfType<ArmBossHealthBars>().GetHealthBar(healthBarIndex);
        Hide();
    }

    void Update()
    {
        
    }

    public void Show()
    {
        healthBarTransform.gameObject.SetActive(true);
        healthBarBackgroundTransform.gameObject.SetActive(true);
    }

    public void Hide()
    {
        healthBarTransform.gameObject.SetActive(false);
        healthBarBackgroundTransform.gameObject.SetActive(false);
    }

    public void UpdateHealth(float bosshealth)
    {
        healthBarTransform.localScale = new Vector3(bosshealth / 20, healthBarTransform.localScale.y, 1);
    }
}
