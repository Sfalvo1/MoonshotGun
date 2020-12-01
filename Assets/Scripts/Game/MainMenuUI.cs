using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenuUI : MonoBehaviour
{

    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        transform.Find("playBtn").GetComponent<Button>().onClick.AddListener(() => {
            GameSceneManager.Load("Level1");
        });
        transform.Find("quitBtn").GetComponent<Button>().onClick.AddListener(() => {
            Application.Quit();
        });
    }

    private void Start()
    {
        scoreText.SetText("HighScore: " + PlayerPrefs.GetInt("HighScore").ToString());
    }
}
