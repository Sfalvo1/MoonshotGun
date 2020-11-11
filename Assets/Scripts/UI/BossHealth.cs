using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public static BossHealth Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] Transform healthBarTransform;
    [SerializeField] Transform healthBarBackgroundTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
