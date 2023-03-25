using UnityEngine;
using UnityEngine.UI;

public class TaskAppDisplay1 : MonoBehaviour
{
    public TaskApp task; // The corresponding task app to this display
    public Button reject; // The reject button
    public Button accept; // The accept button

    void Start()
    {
        reject.onClick.AddListener(() =>
        {
            task.AddScoreAndReason(500, "Read a pawesome story!");
            // TODO: Disable popup window
        });
        accept.onClick.AddListener(() =>
        {
            task.AddScoreAndReason(-300, "Allowed third-party ad-tracking cookies.");
            // TODO: Disable popup window
        });
    }
}
