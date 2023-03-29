using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{

    public TextMeshProUGUI scoreBreakdown;
    public TextMeshProUGUI totalScoreText;

    void Start() {

        string scoreText = "";

        int totalScores = PlayerPrefs.GetInt("totalScores");
        int totalScore = 0;
        
        
        for (int i = 0; i < totalScores; i++) {
            string line = "";
            int scoreNum = PlayerPrefs.GetInt("scoreNum" + i);
            string scoreString = PlayerPrefs.GetString("scoreString" + i);
            line += scoreNum + ": " + scoreString;
            totalScore += scoreNum;
            scoreText += line + System.Environment.NewLine;
        }

        scoreBreakdown.text = scoreText;
        totalScoreText.text = "Total Score: " + totalScore;
    }



    public void RestartGame()
    {
        Debug.Log("Game restarting");
        SceneManager.LoadScene("StartMenuScene");

    }

    public void Quit() 
    {
        Debug.Log("Application closed");
        Application.Quit();
    }
}