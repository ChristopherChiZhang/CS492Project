using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    // Current opened task
    string currentTask;

    // temporary ints for testing game flow
    int tasksCompleted = 0;
    int totalTasks = 6;


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
        tasksCompleted++;

        CheckGameOver();

    }

    public void CheckGameOver() 
    {
        // final code after tasks are implemented
        /*
        ForEach(bool completed in tasks.Values) {
            if (completed == false) return false;
        }
        return true;
        */

        // temp code for testing
        if (tasksCompleted >= totalTasks) GameOver();

    }

    public bool TaskIsComplete(string task)
    {
        return tasks[task];
    }

    public void GameOver() 
    {
        Debug.Log("Game over");
        SceneManager.LoadScene("GameOverScene");


    }
}
