using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    // Current opened task
    string currentTask;

    // Dictionary of tasks and if they are complete
    Dictionary<string, bool> tasks;

    void Start()
    {
        currentTask = null;
        tasks = new Dictionary<string, bool>();

        // Add tasks to dictionary
        FindObjectsOfType<TaskApp>().ToList().ForEach(taskApp => {
            tasks.Add(taskApp.name, false);
        });
    }

    public void StartTask(string task)
    {
        currentTask = task;
        Debug.Log("Started task: " +  task);
    }

    public string GetCurrentTask()
    {
        return currentTask;
    }

    public void CompleteCurrentTask()
    {
        tasks[currentTask] = true;
        Debug.Log("Task marked as completed: " + currentTask);
        currentTask = null;
    }

    public bool TaskIsComplete(string task)
    {
        return tasks[task];
    }
}
