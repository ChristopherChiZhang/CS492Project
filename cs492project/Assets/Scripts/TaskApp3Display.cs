using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TaskApp3Display : MonoBehaviour
{
    public TaskApp task; // Corresponding task app to this display

    public Button nextPage;
    public Button prevPage;
    public Button accept;

    public GameObject page1;
    public GameObject page2;
    public GameObject page3;
    public GameObject page4;
    public GameObject quickPage;

    public GameObject smartwatchPopup;

    int currentPage = 1;
    float duration = 5f;
    float currentTime = 0f;
    Vector3 endPosition = new Vector3(-6.8f, 1.201778f, 100f);

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
        accept.onClick.AddListener(() =>
        {
            overLay.DelayedExecute(() =>
            {
                nextPage.interactable = false;
                prevPage.interactable = false;
                smartwatchPopup.SetActive(false);
                quickPage.SetActive(true);
            });
        });

        // Add listeners to each button on the pages
        //Sprite auraImage = Resources.Load<Sprite>("General/ButtonSelectedAura");
        GameObject[] pages = { page1, page2, page3, page4, quickPage };
        pages.ToList().ForEach(page =>
        {
            page.GetComponentsInChildren<Button>().ToList().ForEach(button =>
            {
                button.onClick.AddListener(() =>
                {
                    //button.GetComponent<Image>().sprite = auraImage;
                });
            });
        });
    }

    void Update()
    {
        if (currentTime != duration)
        {
            currentTime += Time.deltaTime;
            if (currentTime > duration)
            {
                currentTime = duration;
            }
            float percent = currentTime / duration;
            smartwatchPopup.transform.position = Vector3.Lerp(smartwatchPopup.transform.position, endPosition, percent);
        }
    }
}
