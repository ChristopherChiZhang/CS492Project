using UnityEditor.UI;
using UnityEngine;

public class TaskAppDisplays : MonoBehaviour
{
    public void EnableTask(GameObject taskApp)
    {
        gameObject.SetActive(true); // Enable the parent

        // Find the child with the matching prefix "TaskAppx" and enable it
        //GameObject[] taskAppDisplays = GetComponentsInChildren<GameObject>(true);
        Debug.Log("Enabling " + taskApp.name + "Display");
        transform.Find(taskApp.name + "Display").gameObject.SetActive(true);
        //foreach (GameObject display in taskAppDisplays)
        //{
        //    if(display.name.StartsWith(taskApp.name))
        //    {
                
        //        display.SetActive(true);
        //    }
        //    break;
        //}
    }

    public void DisableAllTasks()
    {
        //GameObject[] taskAppDisplays = GetComponentsInChildren<GameObject>(true);
        //foreach(GameObject display in taskAppDisplays)
        //{
        //    Debug.Log("Disabling" + display.name);
        //    display.SetActive(false);
        //}
        foreach(Transform child in transform)
        {
            Debug.Log("Disabling " + child.gameObject.name);
            child.gameObject.SetActive(false);
        }
        gameObject.SetActive(false); // Disable the parent
    }
}
