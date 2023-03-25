using UnityEngine;

public class TaskAppDisplays : MonoBehaviour
{
    public void EnableTask(GameObject taskApp)
    {
        gameObject.SetActive(true); // Enable the parent

        // Find the child GameObject with the matching "TaskAppxDisplay" name and enable it
        transform.Find(taskApp.name + "Display").gameObject.SetActive(true);

        // Update game state
        FindObjectOfType<GameStateManager>().StartTask(taskApp.GetComponent<TaskApp>());
    }

    public void DisableAllTasks()
    {
        // Disable all child GameObjects
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        gameObject.SetActive(false); // Disable the parent
    }
}
