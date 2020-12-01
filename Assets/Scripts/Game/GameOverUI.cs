using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public static GameOverUI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        transform.Find("restartBtn").GetComponent<Button>().onClick.AddListener(() => {
            GameSceneManager.Load("Level1");
            if (Scoreboard.Instance != null)
            {
                Scoreboard.Instance.scoreNumber = 0;
            }
            PlayerPrefs.SetInt("CurrentScore", 0);
            Scoreboard.Instance.SubtractScore();
        });
        transform.Find("quitBtn").GetComponent<Button>().onClick.AddListener(() => {

            GameSceneManager.Load("MainMenuScene");
            PlayerPrefs.SetInt("CurrentScore", 0);
        });

        Hide();
    }

    public void Show()
    {
        try
        {
            gameObject.SetActive(true);
        }
        catch { return; }

        transform.Find("finalScoreText").GetComponent<TextMeshProUGUI>().SetText("Final Score: " + Scoreboard.Instance.scoreNumber);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

}
