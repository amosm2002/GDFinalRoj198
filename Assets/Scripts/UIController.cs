using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Text livesText;
    public Text scoreText;

    void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        if (ScoreManager.instance != null && scoreText != null)
            scoreText.text = "Score: " + ScoreManager.instance.GetScore();

        PlayerController player = FindObjectOfType<PlayerController>();
        if (player != null && livesText != null)
            livesText.text = "Lives: " + player.GetLives();
    }
}
