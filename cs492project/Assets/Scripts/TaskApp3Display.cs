using UnityEngine;
using UnityEngine.UI;

public class TaskApp3Display : MonoBehaviour
{
    public TaskApp task; // Corresponding task app to this display

    public Button nextPage;
    public Button prevPage;

    public GameObject page1;
    public GameObject page2;
    public GameObject page3;
    public GameObject page4;

    public GameObject quickPage;

    int currentPage = 1;

    void Start()
    {
        LoadingOverlay overLay = FindObjectOfType<LoadingOverlay>();
        nextPage.onClick.AddListener(() =>
        {
            overLay.DelayedExecute(() =>
            {
                if (currentPage == 1)
                {
                    prevPage.interactable = true;
                    page1.SetActive(false);
                    page2.SetActive(true);
                }
                else if (currentPage == 2)
                {
                    page2.SetActive(false);
                    page3.SetActive(true);
                }
                else if (currentPage == 3)
                {
                    nextPage.interactable = false;
                    page3.SetActive(false);
                    page4.SetActive(true);
                }
                currentPage++;
            });
        });
        prevPage.onClick.AddListener(() =>
        {
            overLay.DelayedExecute(() =>
            {
                if (currentPage == 2)
                {
                    prevPage.interactable = false;
                    page2.SetActive(false);
                    page1.SetActive(true);
                }
                else if (currentPage == 3)
                {
                    page3.SetActive(false);
                    page2.SetActive(true);
                }
                else if (currentPage == 4)
                {
                    nextPage.interactable = true;
                    page4.SetActive(false);
                    page3.SetActive(true);
                }
                currentPage--;
            });
        });
    }
}
