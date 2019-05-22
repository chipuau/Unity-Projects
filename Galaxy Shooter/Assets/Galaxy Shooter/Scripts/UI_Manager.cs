using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class UI_Manager : MonoBehaviour
{
    public Sprite[] livesImage;
    public Image livesImageDisplay;
    public Text scoreText;
    public GameObject titleScreen; 
    public int score = 0; 
    
    public void UpdateLives(int currentLives)
    {
        livesImageDisplay.sprite = livesImage[currentLives]; 
    }

    public void UpdateScore()
    {
        score += 10;
        scoreText.text = "Score: " + score; 
    }

    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);
        score = 0;
        scoreText.text = "Score: ";
    }

    public void HideTitleScreen()
    {
        titleScreen.SetActive(false);
    }
}
