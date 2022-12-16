/* 
``````````````````````````````````````````
MainMenu.cs

Displays main menu on start.
Button functions:
    [Play]      loads Game scene
    [Controls]  makes control text visible
    [Credits]   makes credits text visible
    [Quit]      quits application to desktop
Plays desired sound (sound[0]) on button press.

``````````````````````````````````````````
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject controlsText;
    public GameObject creditsText;

    AudioSource[] sounds;

    void Start()
    {
        sounds = GetComponents<AudioSource>();
        controlsText.SetActive(false);
        creditsText.SetActive(false);
    }

    public void OnPlayButtonPress()
    {
        sounds[0].Play();
        SceneManager.LoadScene("Game"); // start game
        Time.timeScale = 1f;
    }

    public void OnControlsButtonPress()
    {
        sounds[0].Play();
        controlsText.SetActive(true);
        creditsText.SetActive(false);
    }

    public void OnCreditsButtonPress()
    {
        sounds[0].Play();
        creditsText.SetActive(true);
        controlsText.SetActive(false);
    }

    public void QuitGame()
    {
        sounds[0].Play();
        Application.Quit();
    }
}
