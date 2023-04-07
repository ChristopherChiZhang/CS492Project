using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TaskApp3Display : MonoBehaviour
{
    public TaskApp task; // Corresponding task app to this display

    public Button nextPage;
    public Button prevPage;
    public Button accept;
    public Button submit;

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

    Button selectedMed = null;

    void Start()
    {
        Sprite auraImage = Resources.Load<Sprite>("General/CheckboxUnchecked"); // TODO: Update with aura icon
        Sprite uiMask = page1.GetComponentInChildren<Button>().GetComponent<Image>().sprite; // Save the uiMask to restore later
        LoadingOverlay overLay = FindObjectOfType<LoadingOverlay>();
        nextPage.onClick.AddListener(() =>
        {
            overLay.DelayedExecute(() =>
            {
                GameObject oldPage = null;
                GameObject newPage = null;
                if (currentPage == 1)
                {
                    prevPage.interactable = true;
                    oldPage = page1;
                    newPage = page2;
                }
                else if (currentPage == 2)
                {
                    oldPage = page2;
                    newPage = page3;
                }
                else if (currentPage == 3)
                {
                    nextPage.interactable = false;
                    oldPage = page3;
                    newPage = page4;
                }
                // Update all buttons on page to not be selected
                oldPage.GetComponentsInChildren<Button>().ToList().ForEach(button =>
                {
                    button.GetComponent<Image>().sprite = uiMask;
                });
                selectedMed = null;
                submit.gameObject.SetActive(false);
                oldPage.SetActive(false);
                newPage.SetActive(true);
                currentPage++;
            });
        });
        prevPage.onClick.AddListener(() =>
        {
            overLay.DelayedExecute(() =>
            {
                GameObject oldPage = null;
                GameObject newPage = null;
                if (currentPage == 2)
                {
                    prevPage.interactable = false;
                    oldPage = page2;
                    newPage = page1;
                }
                else if (currentPage == 3)
                {
                    oldPage = page3;
                    newPage = page2;
                }
                else if (currentPage == 4)
                {
                    nextPage.interactable = true;
                    oldPage = page4;
                    newPage = page3;
                }
                // Update all buttons on page to not be selected
                oldPage.GetComponentsInChildren<Button>().ToList().ForEach(button =>
                {
                    button.GetComponent<Image>().sprite = uiMask;
                });
                selectedMed = null;
                submit.gameObject.SetActive(false);
                oldPage.SetActive(false);
                newPage.SetActive(true);
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
            task.AddScoreAndReason(-250, "Shared sensitive health data.");
        });
        submit.onClick.AddListener(() =>
        {
            if (selectedMed.name.StartsWith("CorrectButton"))
            {
                task.AddScoreAndReason(500, "Retrieved your prescription!");
                FindObjectOfType<GameStateManager>().BackToHomeScreen();
            }
            else
            {

            }
        });

        // Add listeners to each button on the pages
        GameObject[] pages = { page1, page2, page3, page4, quickPage };
        pages.ToList().ForEach(page =>
        {
            // Add selected sprite listeners
            page.GetComponentsInChildren<Button>().ToList().ForEach(button =>
            {
                button.onClick.AddListener(() =>
                {
                    // Update buttons on page to not be selected
                    page.GetComponentsInChildren<Button>().ToList().ForEach(notSelectedButton =>
                    {
                        if (notSelectedButton != button)
                        {
                            notSelectedButton.GetComponent<Image>().sprite = uiMask;
                        }
                    });
                    // Change sprite of current button to be selected
                    button.GetComponent<Image>().sprite = auraImage;
                    selectedMed = button;
                    submit.gameObject.SetActive(true);
                });

                string buttonIconName = "";
                if (button.name.StartsWith("CorrectButton"))
                {
                    buttonIconName = "CorrectButtonIcon" + button.name.Last();
                }
                else
                {
                    buttonIconName = "ButtonIcon" + button.gameObject.name.Last();
                }
                GameObject buttonIcon = page.transform.Find(buttonIconName).gameObject;
                EventTrigger trigger = button.GetComponent<EventTrigger>();
                EventTrigger.Entry enterEntry = new EventTrigger.Entry();
                enterEntry.eventID = EventTriggerType.PointerEnter;
                enterEntry.callback.AddListener((eventData) =>
                {
                    buttonIcon.transform.localScale = new Vector3(1.1f, 1.1f, 0f);
                });
                EventTrigger.Entry exitEntry = new EventTrigger.Entry();
                exitEntry.eventID = EventTriggerType.PointerExit;
                exitEntry.callback.AddListener((eventData) =>
                {
                    buttonIcon.transform.localScale = new Vector3(1f, 1f, 1f);
                });
                trigger.triggers.Add(enterEntry);
                trigger.triggers.Add(exitEntry);
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
