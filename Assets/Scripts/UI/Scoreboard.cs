using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    public static Scoreboard Instance { get; private set; }

    [SerializeField] TextMeshProUGUI textUI;

    public int scoreNumber;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetScore(scoreNumber);
    }

    public void AddScore(int scoreAmount)
    {
        scoreNumber += scoreAmount;
        SetScore(scoreNumber);
    }

    private void SetScore(int newScore)
    {
        textUI.text = newScore.ToString();
    }
}
