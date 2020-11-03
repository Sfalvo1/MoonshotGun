using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameSceneManager
{
    public static void Load(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
