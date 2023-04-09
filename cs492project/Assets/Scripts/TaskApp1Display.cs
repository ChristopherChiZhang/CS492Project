using UnityEngine;
using UnityEngine.UI;

public class TaskAppDisplay1 : MonoBehaviour
{
    public TaskApp task; // The corresponding task app to this display
    public Button reject; // The reject button
    public Button accept; // The accept button
    public Button nextPage; // The next page button
    public GameObject finish; // The finish task button
    public GameObject page1; // The first page
    public GameObject page2; // The second page
    public GameObject page3; // The third page
    public GameObject popup; // The pop up window
    private int currentPage = 1;

    void Start()
    {
        reject.onClick.AddListener(() =>
        {
            popup.SetActive(false);
            nextPage.interactable = true;
        });
        accept.onClick.AddListener(() =>
        {
            task.AddScoreAndReason(-400, "Task 1: Allowed third-party ad-tracking cookies.");
            popup.SetActive(false);
            nextPage.interactable = true;
        });
        nextPage.onClick.AddListener(() =>
        {
            FindObjectOfType<LoadingOverlay>().DelayedExecute(() =>
            {
                currentPage++;
                if (currentPage == 2)
                {
                    page1.SetActive(false);
                    page2.SetActive(true);

                    // Delay popup for a bit (and temporarily disable next button)
                    nextPage.interactable = false;
                    FindObjectOfType<LoadingOverlay>().DelayedExecute(() =>
                    {
                        popup.SetActive(true);
                    }, 0.5f, false);
                }
                else // third page
                {
                    page2.SetActive(false);
                    page3.SetActive(true);
                    finish.SetActive(true);
                    nextPage.gameObject.SetActive(false);
                    task.AddScoreAndReason(500, "Task 1: Read a pawesome story!");
                }
            });
        });
    }
}
