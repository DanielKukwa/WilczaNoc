using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    AudioSource audio;
    PauseMenu pauseMenu;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
        pauseMenu = GameObject.Find("Canvas").GetComponent<PauseMenu>();
    }

    public void LoadMenuAfterPause()
    {
      
        Time.timeScale = 1f;
        LoadStartMenu();
        
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }


    public void LoadGameScene()
    {
        Invoke("StartGame", 4f);
        PlayHowlingSound();
    }

    public void StartGame()
    {
        if (Application.isEditor)
        {
            SceneManager.LoadScene("LevelDesign");
            SceneManager.LoadScene("LevelDesignAnti", LoadSceneMode.Additive);
            SceneManager.LoadScene("Kamil_enviro", LoadSceneMode.Additive);
            SceneManager.LoadScene("Events", LoadSceneMode.Additive);
            SceneManager.LoadScene("DanInteracts", LoadSceneMode.Additive);
        }
        else
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene("LevelDesign");
        }
    }

    public void LoadCredits()
    {

        SceneManager.LoadScene("Credits");
    }

    public void LoadGameOver()
    {

        SceneManager.LoadScene("GameOver");
    }

    public void QuitGame()
    {

        Application.Quit();

    }

    void PlayHowlingSound()
    {
        audio.Play();
    }
}
