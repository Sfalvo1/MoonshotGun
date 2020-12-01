using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    // Change this so the boss Finds the level manager object in game and calls the level change

    public int levelToLoad;

    public void LoadLevel()
    {
        StartCoroutine("Transition");
    }

    private IEnumerator Transition()
    {
        if (levelToLoad < 0)
        {
            Debug.LogError("Scene to load is not set");
            yield break;
        }

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene(levelToLoad);
    }

}
