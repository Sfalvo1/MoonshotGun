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
        scoreNumber = PlayerPrefs.GetInt("CurrentScore");
        SetScore(scoreNumber);
    }

    private void Start()
    {
        scoreNumber = PlayerPrefs.GetInt("CurrentScore");
        SetScore(scoreNumber);
    }

    public void AddScore(int scoreAmount)
    {
        scoreNumber += scoreAmount;
        SetScore(scoreNumber);

        PlayerPrefs.SetInt("CurrentScore", scoreNumber);
    }

    private void SetScore(int newScore)
    {
        textUI.text = newScore.ToString();

    }

    public void SubtractScore()
    {
        ScoreAmount[] subtractMe = FindObjectsOfType<ScoreAmount>();
        foreach(ScoreAmount score in subtractMe)
        {
            scoreNumber -= score.scoreAmount;
        }
        if(scoreNumber < 0)
        {
            scoreNumber = 0;
        }
    }
}
