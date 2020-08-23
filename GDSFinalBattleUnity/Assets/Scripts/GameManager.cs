using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        if (!Application.isEditor)
        {         
            SceneManager.LoadScene("LevelDesignAnti", LoadSceneMode.Additive);
            SceneManager.LoadScene("Kamil_enviro", LoadSceneMode.Additive);
            SceneManager.LoadScene("Events", LoadSceneMode.Additive);
            SceneManager.LoadScene("DanInteracts", LoadSceneMode.Additive);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadScene("LevelDesign");
            
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            SceneManager.LoadScene("PitchScene");
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            SceneManager.LoadScene("MasterScene");
        }
    }
}
