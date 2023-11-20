using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public PlayerController player;
    public Text livesText;

    void Update()
    {
        if (player != null && livesText != null)
        {
            livesText.text = "Lives: " + player.GetLives().ToString();
        }
    }
}
