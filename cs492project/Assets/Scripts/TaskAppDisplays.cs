using UnityEngine;

public class TaskAppDisplays : MonoBehaviour
{
    public GameObject emptyBrowser;  // Empty browser window to show while the site 'loads'

    public void EnableTask(TaskApp taskApp)
    {
        gameObject.SetActive(true);  // Enable itself
        emptyBrowser.SetActive(true);  // Enable the temporary empty browser

        // Do nothing for a bit while the empty browser animation plays
        FindObjectOfType<LoadingOverlay>().DelayedExecute(() =>
        {
            // Show loading overlay for a bit before showing the page
            FindObjectOfType<LoadingOverlay>().DelayedExecute(() => {
                transform.Find(taskApp.name + "Display").gameObject.SetActive(true);  // Find the child GameObject with the matching "TaskAppxDisplay" name and enable it
                emptyBrowser.SetActive(false);  // Disable the temporary empty browser
            });
        }, showOverlay: false);
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
