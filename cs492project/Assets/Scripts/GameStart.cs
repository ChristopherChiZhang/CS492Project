using UnityEngine;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    public Button startButton;
    public GameObject bubble;

    void Start()
    {
        // Hide self and start timer
        startButton.onClick.AddListener(() => {
            gameObject.SetActive(false);
            FindObjectOfType<GameStateManager>().ActivateTimers();
        });

        // Show start button after a delay
        FindObjectOfType<LoadingOverlay>().DelayedExecute(() =>
        {
            startButton.interactable = true;
        }, 3f, showOverlay: false);
    }
}
