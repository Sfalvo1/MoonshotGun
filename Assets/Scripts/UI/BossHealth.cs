using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public static BossHealth Instance { get; private set; }

    public int healthBarScale = 20;

    private void Awake()
    {
        Instance = this;

        healthBarBackgroundTransform = transform.Find("healthBackground");
    }

    [SerializeField] Transform healthBarTransform;
    [SerializeField] Transform healthBarBackgroundTransform;

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
        healthBarTransform.localScale = new Vector3(bosshealth / healthBarScale, healthBarTransform.localScale.y, 1);
    }
}
