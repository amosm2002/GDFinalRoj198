using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    
    private bool isFullscreen = false; 
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
        SceneManager.LoadScene("Tutorial");
    }

    public void ToggleFullscreen()
    {
        isFullscreen = !isFullscreen;
        if (isFullscreen)
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, FullScreenMode.FullScreenWindow);
            Debug.Log("Switched to Fullscreen Mode");
        }
        else
        {
            Screen.SetResolution(1280, 720, FullScreenMode.Windowed);
            Debug.Log("Switched to Windowed Mode");
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        Debug.Log(isPaused ? "Game Paused" : "Game Resumed");
    }
}
