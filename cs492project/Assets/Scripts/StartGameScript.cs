using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScript : MonoBehaviour
{
    void Start() {
        PlayerPrefs.DeleteAll();
    }

    public void StartGame() 
    {
        SceneManager.LoadScene("Homepage");
    }
}