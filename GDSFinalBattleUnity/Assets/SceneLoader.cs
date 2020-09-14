using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }


    public void LoadGameScene()
    {
        if (Application.isEditor)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("LevelDesignAnti", LoadSceneMode.Additive);
            SceneManager.LoadScene("Kamil_enviro", LoadSceneMode.Additive);
            SceneManager.LoadScene("Events", LoadSceneMode.Additive);
            SceneManager.LoadScene("DanInteracts", LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void LoadCredits()
    {

        SceneManager.LoadScene("Credits");
    }

    public void LoadGameOver()
    {

        SceneManager.LoadScene("Game Over");
    }

    public void QuitGame()
    {

        Application.Quit();

    }
}
