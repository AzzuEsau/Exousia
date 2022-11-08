using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject SettingsScreen;
    private bool SettingsStatus = false;

    // Redirects to the scene of the game
    public void LoadGame()
    {
        // SceneManager.LoadScene("Game");
        SceneManager.LoadScene("Olympus");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadControls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void LoadSettings()
    {
        SettingsStatus = !SettingsStatus;
        SettingsScreen.SetActive(SettingsStatus);
    }


    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
