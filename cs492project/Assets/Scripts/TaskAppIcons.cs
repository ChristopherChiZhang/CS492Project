using UnityEngine;
using UnityEngine.UI;

public class TaskAppIcons : MonoBehaviour
{
    public void SetActive()
    {
        // Enable self
        gameObject.SetActive(true);

        UpdateButtons();
    }

    void UpdateButtons()
    {
        // Make buttons interactable iff the associated task is not complete
        GameStateManager gameStateManager = FindObjectOfType<GameStateManager>();
        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<Button>().interactable = !gameStateManager.TaskIsComplete(child.name);
        }
    }
}
