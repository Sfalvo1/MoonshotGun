using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    public static AmmoUI Instance { get; private set; }

    TextMeshProUGUI ammoText;

    private void Awake()
    {
        Instance = this;

        ammoText = transform.Find("ammoText").GetComponent<TextMeshProUGUI>();
    }

    public void SetAmmoText(int ammoAmount)
    {
        ammoText.SetText("Moon Chunks: " + ammoAmount);
    }
}
