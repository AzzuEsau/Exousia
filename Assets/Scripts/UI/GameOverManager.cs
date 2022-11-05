using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    private int waitSeconds = 5;

    public void Start()
    {
        LoadMainMenu();
    }

    // Redirects to the scene of the game
    public void LoadMainMenu()
    {
        StartCoroutine(WaitForLoading());
    }

    protected IEnumerator WaitForLoading()
    {
        yield return new WaitForSeconds(waitSeconds);
        SceneManager.LoadScene("MainMenu");
    }
}
