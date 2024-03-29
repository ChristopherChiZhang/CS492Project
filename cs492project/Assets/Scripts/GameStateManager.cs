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
    float countdownStarting = 120f;
    public TextMeshProUGUI countdownText;
    Color32 lowTime = new Color32(255, 114, 118, 255);

    // If timers (main timer and timer for current task) are counting down
    bool timersActive = false;

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
        UpdateTimerLocation(currentTask.timerXPos, currentTask.timerYPos);
        FindObjectOfType<TaskAppIcons>().gameObject.SetActive(false);
        FindObjectOfType<TaskAppDisplays>(true).EnableTask(currentTask);
        FindObjectOfType<ToDoListWindow>().gameObject.SetActive(false);
        currentTask.TurnTimerOn();
    }

    public void BackToHomeScreen()
    {
        CompleteCurrentTask();
        FindObjectOfType<TaskAppIcons>(true).SetActiveAndUpdateButton(); // Disable completed tasks and display app icons
        FindObjectOfType<TaskAppDisplays>().DisableAllTasks(); // Disable task from being seen to the user
        FindObjectOfType<ToDoListWindow>(true).gameObject.SetActive(true); // Enable the to do list
        FindObjectOfType<ToDoListWindow>(true).UpdateToDoList(currentTask);
        UpdateTimerLocation();
        currentTask = null;
    }

    void CompleteCurrentTask()
    {
        tasks[currentTask] = true; // Mark completed
        currentTask.TurnTimerOff(); // Stop timer
        currentTask.AddScoreAndReason(currentTask.GetTimeScore(), "Task " + currentTask.name[7] + ": Completed in " + (int)System.Math.Round(currentTask.duration) + " seconds.");
        UpdateGameOver(); // Set gameOver if true
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
        }
        if (gameOver) GameOver();
    }

    public void GameOver()
    {
        List<KeyValuePair<int, string>> scores = new List<KeyValuePair<int, string>>();
        int totalScores = 0;


        for (int index = 0; index < tasks.Count; index++)
        {
            var item = tasks.ElementAt(index);
            var itemKey = item.Key.scoresAndReasons.ToList();

            for (int i = 0; i < itemKey.Count; i++)
            {
                var scoreItem = itemKey.ElementAt(i);
                scores.Add(new KeyValuePair<int, string>(scoreItem.Item1, scoreItem.Item2));
                totalScores++;
            }
        }

        scores.Sort((a, b) => a.Value.CompareTo(b.Value));

        PlayerPrefs.SetInt("totalScores", totalScores);
        for (int i = 0; i < totalScores; i++)
        {
            PlayerPrefs.SetInt("scoreNum" + i, scores.ElementAt(i).Key);
            PlayerPrefs.SetString("scoreString" + i, scores.ElementAt(i).Value);
        }
        SceneManager.LoadScene("GameOverScene");
    }

    private void UpdateTimerLocation(float x = -6.133334f, float y = -4.044445f)
    {
        countdownText.gameObject.transform.position = new Vector3(x, y, 100);
    }

    private void Update()
    {
        if (timersActive)
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

    public void ActivateTimers()
    {
        timersActive = true;
    }

    public void DeactivateTimers()
    {
        timersActive = false;
    }
}
