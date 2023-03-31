using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    // Current opened task
    TaskApp currentTask;

    // Dictionary of tasks and if they are complete
    Dictionary<TaskApp, bool> tasks;

    // Stores whether the game is over or not
    bool gameOver = false;

    // Game countdown timer
    float countdownCurrent = 0f;
    float countdownStarting = 300f;
    public TextMeshProUGUI countdownText;
    Color32 lowTime = new Color32(255, 114, 118, 255);

    void Start()
    {
        currentTask = null;
        tasks = new Dictionary<TaskApp, bool>();

        countdownCurrent = countdownStarting;

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

    public void BackToHomeScreen()
    {
        CompleteCurrentTask();
        FindObjectOfType<TaskWindow>().gameObject.SetActive(false); // Disable the task window (to be removed)
        FindObjectOfType<TaskAppIcons>(true).SetActiveAndUpdateButton(); // Disable completed tasks and display app icons
        FindObjectOfType<TaskAppDisplays>(true).DisableAllTasks(); // Disable task from being seen to the user
    }

    public void CompleteCurrentTask()
    {
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


        if (IsGameOver()) GameOver();
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

    public void GameOver()
    {
        Debug.Log("TASKSDONE");
        List<int> scoreNums = new List<int>();
        List<string> scoreStrings = new List<string>();
        int totalScores = 0;


        for (int index = 0; index < tasks.Count; index++)
        {
            var item = tasks.ElementAt(index);
            var itemKey = item.Key.scoresAndReasons.ToList();

            for (int i = 0; i < itemKey.Count; i++)
            {
                var scoreItem = itemKey.ElementAt(i);
                scoreNums.Add(scoreItem.Item1);
                scoreStrings.Add(scoreItem.Item2);
                totalScores++;
            }
        }

        PlayerPrefs.SetInt("totalScores", totalScores);
        for (int i = 0; i < totalScores; i++)
        {
            PlayerPrefs.SetInt("scoreNum" + i, scoreNums.ElementAt(i));
            PlayerPrefs.SetString("scoreString" + i, scoreStrings.ElementAt(i));
        }

        Debug.Log("Game over");
        SceneManager.LoadScene("GameOverScene");
    }

    private void Update()
    {
        if (currentTask != null && currentTask.isTimerOn)
        {
            currentTask.duration += Time.deltaTime;
        }

        if (countdownCurrent <= 1f)
        {
            gameOver = true;
            UpdateGameOver();
        }
        if (countdownCurrent <= 31f)
        {
            countdownText.color = lowTime;
        }

        countdownCurrent -= 1 * Time.deltaTime;
        countdownText.text = string.Format("{0:0} : {1:00}", Mathf.FloorToInt(countdownCurrent / 60), Mathf.FloorToInt(countdownCurrent % 60));


    }

}
