using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    // Current opened task
    TaskApp currentTask;

    // Dictionary of tasks and if they are complete
    Dictionary<TaskApp, bool> tasks;

    // Stores whether the game is over or not
    bool gameOver = false;

    void Start()
    {
        currentTask = null;
        tasks = new Dictionary<TaskApp, bool>();

        // Add tasks to dictionary
        FindObjectsOfType<TaskApp>().ToList().ForEach(taskApp =>
        {
            tasks.Add(taskApp, false);
        });
    }

    public void StartTask(TaskApp task)
    {
        currentTask = task;
        Debug.Log("Started task: " + task);
    }

    public TaskApp GetCurrentTask()
    {
        return currentTask;
    }

    public void CompleteCurrentTask()
    {
        // TODO: Updated score in gamestate manager
        tasks[currentTask] = true; // Mark completed
        currentTask.TurnTimerOff(); // Stop timer
        Debug.Log(currentTask.name + " completed in: " + currentTask.duration + " seconds");
        UpdateGameOver(); // Set gameOver if true
        Debug.Log("Task marked as completed: " + currentTask);
        currentTask = null;
    }

    public bool TaskIsComplete(TaskApp task)
    {
        return tasks[task];
    }

    public void UpdateGameOver()
    {
        if (tasks.Values.All(v => v == true))
        {
            gameOver = true;
            Debug.Log("ALL TASKS COMPLETED.");
        }
        // TODO: Check if global game timer is up
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public int ComputeTotalScore()
    {
        // TODO: Compute total score
        // and populate the scroll rect on the end page with Text objects?
        return 0;
    }

    private void Update()
    {
        if (currentTask != null && currentTask.isTimerOn)
        {
            currentTask.duration += Time.deltaTime;
        }
    }
}
