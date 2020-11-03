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
            GameSceneManager.Load("SampleScene");
        });
        transform.Find("quitBtn").GetComponent<Button>().onClick.AddListener(() => {
            GameSceneManager.Load("MainMenuScene");
        });

        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);

        transform.Find("finalScoreText").GetComponent<TextMeshProUGUI>().SetText("Final Score: " + Scoreboard.Instance.scoreNumber);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
