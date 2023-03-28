using System;
using System.Collections.Generic;
using UnityEngine;

public class TaskApp : Timer
{
    public List<Tuple<int, string>> scoresAndReasons = new List<Tuple<int, string>>();

    public TaskApp()
    {
        duration = 0;
    }

    public void AddScoreAndReason(int score, string reason)
    {
        scoresAndReasons.Add(Tuple.Create(score, reason));
        Debug.Log("Added score: " + score + " and reason: " + reason + " to current task.");
    }
}
