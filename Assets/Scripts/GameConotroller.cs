using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    
    private bool isPaused = false;

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }
    
    public void LoadTutorial()
    {
        var settings = FindObjectOfType<SettingsManager>();
        if (settings != null) Destroy(settings.gameObject);
        SceneManager.LoadScene("Settings");
    }


    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        Debug.Log(isPaused ? "Game Paused" : "Game Resumed");
    }

    public void RestartGame()
    {
        var settings = FindObjectOfType<SettingsManager>();
        if (settings != null) Destroy(settings.gameObject);

        var player = FindObjectOfType<PlayerController>();
        if (player != null) Destroy(player.gameObject);

        var scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager != null) Destroy(scoreManager.gameObject);

        SceneManager.LoadScene("StartMenu");
    }
}
