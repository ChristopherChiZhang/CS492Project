using System;
using System.Collections.Generic;
using UnityEngine;

public class TaskApp : Timer
{
    public List<Tuple<int, string>> scoresAndReasons = new List<Tuple<int, string>>();
    float initialTimeScore = 1000; // The base score a player can get if a task is completed instantly
    int pointLoss = 25; // The amount of points a player loses per second from the initial amount
    public float timerXPos = 7.0f;
    public float timerYPos = 3.8f;

    public TaskApp()
    {
        duration = 0;
    }

    public void AddScoreAndReason(int score, string reason)
    {
        scoresAndReasons.Add(Tuple.Create(score, reason));
        Debug.Log("Added score: " + score + " and reason: " + reason + " to current task.");
    }

    public int GetTimeScore()
    {
        return Math.Max((int)Math.Round(initialTimeScore - duration * pointLoss), 0);
    }
}
