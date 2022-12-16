/* 
------------------------------------------
PauseMenu.cs
Displays Pause Menu
Button Functions:
    [Main Menu]    Quits to Main Menu
    [Quit]         Quits to desktop
------------------------------------------
*/

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static bool GameIsPaused
    {
        set
        {
            if(value)
            {   // Pause
                Time.timeScale = 0.0f;
                Cursor.lockState = CursorLockMode.None; // locks cursor to center of screen so it doesnt leave
                Cursor.visible = true;
            }
            else
            {   // Unpause
                Time.timeScale = 1.0f;
                Cursor.lockState = CursorLockMode.Locked; // locks cursor to center of screen so it doesnt leave
                Cursor.visible = false;

                // Wait a frame before allowing pausing
                WasJustUnpaused = true;
            }
        }
        get => Time.timeScale == 0.0f;
    }
    private static bool WasJustUnpaused = false;

    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        // Prevent race conditions with closing other UIs
        if (WasJustUnpaused)
        {
            WasJustUnpaused = false;
            return;
        }

        // Close/open UI
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!GameIsPaused)
            {
                Pause();
            }
            else if(pauseMenuUI.activeSelf)
            {
                Resume();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        GameIsPaused = true;
    }

    public void OnMenuButtonPress()
    {
        SceneManager.LoadScene("Menu"); // main menu
        GameIsPaused = true;   // show cursor
        Time.timeScale = 1.0f; // unfreeze time
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
