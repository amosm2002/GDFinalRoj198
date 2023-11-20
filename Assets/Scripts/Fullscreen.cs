using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fullscreen : MonoBehaviour
{
    private bool isFullscreen = true; 
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
}
