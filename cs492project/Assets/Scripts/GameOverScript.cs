using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    
    public void RestartGame()
    {
        Debug.Log("Game restarted");
        SceneManager.LoadScene("StartMenuScene");
        
    }

    public void Quit() 
    {
        Debug.Log("Application closed");
        Application.Quit();
    }
}
