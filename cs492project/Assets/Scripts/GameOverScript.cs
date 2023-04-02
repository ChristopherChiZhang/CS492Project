using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScript : MonoBehaviour
{

    public TextMeshProUGUI scoreBreakdownPos;
    public TextMeshProUGUI scoreBreakdownNeg;
    public TextMeshProUGUI totalScoreText;

    void Start() {

        string scoreTextPos = "";
        string scoreTextNeg = "";

        int totalScores = PlayerPrefs.GetInt("totalScores");
        int totalScore = 0;
        
        
        for (int i = 0; i < totalScores; i++) {
            string line = "";
            int scoreNum = PlayerPrefs.GetInt("scoreNum" + i);
            string scoreString = PlayerPrefs.GetString("scoreString" + i);
            line += scoreNum + ": " + scoreString;
            totalScore += scoreNum;

            if (scoreNum > 0) {
                scoreTextPos += line + System.Environment.NewLine;
            } else {
                scoreTextNeg += line + System.Environment.NewLine;
            }
            
        }

        if (scoreTextPos != "") {
            scoreBreakdownPos.text = scoreTextPos;
        } else {
            scoreBreakdownPos.text = "No positive scores :(";
        }
        if (scoreTextNeg != "") {
            scoreBreakdownNeg.text = scoreTextNeg;
        } else {
            scoreBreakdownNeg.text = "No negative scores :)";
        }
        
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