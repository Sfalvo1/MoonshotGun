using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinScreenUI : MonoBehaviour
{
    public static WinScreenUI Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        transform.Find("restartBtn").GetComponent<Button>().onClick.AddListener(() => {
            GameSceneManager.Load("Level1");
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
