using System.Linq;
using UnityEngine;

public class SubmitTask : MonoBehaviour
{
    public void CompleteTask()
    {
        // Get the current task and mark it completed
        // Updated score in gamestate manager
        // Handle anything else we need to do
        Debug.Log("Task completed!");

        // Mark current task as complete in game state
        FindObjectOfType<GameStateManager>().CompleteCurrentTask();
    }
}
