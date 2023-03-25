using UnityEngine;
using UnityEngine.UI;

public class TaskAppIcons : MonoBehaviour
{
    public void SetActive(bool isActive)
    {
        // Enable self
        gameObject.SetActive(isActive);

        UpdateButtons();
    }

    void UpdateButtons()
    {
        // Make buttons interactable iff the associated task is not complete
        GameStateManager gameStateManager = FindObjectOfType<GameStateManager>();
        foreach (Transform child in transform)
        {
            TaskApp taskApp = child.GetComponent<TaskApp>();
            child.gameObject.GetComponent<Button>().interactable = !gameStateManager.TaskIsComplete(taskApp);
        }
    }
}
