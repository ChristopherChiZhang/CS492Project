using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{

    public TextMeshProUGUI scoreBreakdownPos;
    public TextMeshProUGUI scoreBreakdownNeg;
    public TextMeshProUGUI totalScoreText;

    public GameObject finalPopup;
    public Button finalPopupYes;
    public Button finalPopupNo;

    void Start()
    {
        UpdateScoreText();

        finalPopupYes.onClick.AddListener(() =>
        {
            PlayerPrefs.SetInt("scoreNum" + PlayerPrefs.GetInt("totalScores"), -1);
            PlayerPrefs.SetString("scoreString" + PlayerPrefs.GetInt("totalScores"), "Data Racer: Shared your data with us. (Not actually, don't worry.)");
            PlayerPrefs.SetInt("totalScores", PlayerPrefs.GetInt("totalScores") + 1);
            UpdateScoreText();
            finalPopup.SetActive(false);
        });

        finalPopupNo.onClick.AddListener(() =>
        {
            finalPopup.SetActive(false);
        });

        Delay(() => { finalPopup.SetActive(true); }, 1f);
    }

    private void UpdateScoreText()
    {
        string scoreTextPos = "";
        string scoreTextNeg = "";

        int totalScores = PlayerPrefs.GetInt("totalScores");
        int totalScore = 0;


        for (int i = 0; i < totalScores; i++)
        {
            int scoreNum = PlayerPrefs.GetInt("scoreNum" + i);
            string scoreString = PlayerPrefs.GetString("scoreString" + i);
            totalScore += scoreNum;

            if (scoreNum > 0)
            {
                scoreTextPos += "<color=green>" + scoreNum + "</color> " + scoreString + Environment.NewLine;
            }
            else
            {
                scoreTextNeg += "<color=red>" + scoreNum + "</color> " + scoreString + Environment.NewLine;
            }

        }

        if (scoreTextPos != "")
        {
            scoreBreakdownPos.text = scoreTextPos;
        }
        else
        {
            scoreBreakdownPos.text = "No positive scores :(";
        }
        if (scoreTextNeg != "")
        {
            scoreBreakdownNeg.text = scoreTextNeg;
        }
        else
        {
            scoreBreakdownNeg.text = "No negative scores :)";
        }

        totalScoreText.text = "Total Score: " + totalScore;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("StartMenuScene");

    }

    public void Quit()
    {
        Application.Quit();
    }

    private IEnumerator DelayCoroutine(Action callback, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        callback();
    }

    private void Delay(Action callback, float seconds)
    {
        StartCoroutine(DelayCoroutine(callback, seconds));
    }
}