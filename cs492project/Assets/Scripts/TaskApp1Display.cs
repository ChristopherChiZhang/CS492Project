using UnityEngine;
using UnityEngine.UI;

public class TaskAppDisplay1 : MonoBehaviour
{
    public TaskApp task; // The corresponding task app to this display
    public Button reject; // The reject button
    public Button accept; // The accept button
    public Button nextPage; // The next page button
    public GameObject page1; // The first page
    public GameObject page2; // The second page
    public GameObject page3; // The third page
    public GameObject popup; // The pop up window
    private int currentPage = 1;

    void Start()
    {
        reject.onClick.AddListener(() =>
        {
            task.AddScoreAndReason(500, "Read a pawesome story!");
            popup.SetActive(false);
        });
        accept.onClick.AddListener(() =>
        {
            task.AddScoreAndReason(-300, "Allowed third-party ad-tracking cookies.");
            popup.SetActive(false);
        });
        nextPage.onClick.AddListener(() =>
        {
            currentPage++;
            if (currentPage == 2)
            {
                page1.SetActive(false);
                page2.SetActive(true);
                popup.SetActive(true);
            }
            else // third page
            {
                page2.SetActive(false);
                page3.SetActive(true);
                nextPage.gameObject.SetActive(false);
            }
        });
    }
}
