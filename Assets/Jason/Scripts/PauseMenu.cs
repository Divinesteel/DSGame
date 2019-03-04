using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public static bool gameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject SettingsMenuUI;


    // Update is called once per frame
    void Update () {
		
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }else
            {
                Pause();
            }
        }
	}

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        SettingsMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Settings()
    {
        if (SettingsMenuUI.activeSelf == false)
        {
            SettingsMenuUI.SetActive(true);
        }
        else
        {
            SettingsMenuUI.SetActive(false);
        }
    }

    public void QuitGame()
    {
        SettingsMenuUI.SetActive(false);
        Application.Quit();
    }
}
