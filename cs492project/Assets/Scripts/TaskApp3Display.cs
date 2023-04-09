using System.Collections;
using System.Collections.Generic;
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

    public CanvasGroup errorMessage;
    bool errorMessageOn = false;

    int currentPage = 1;
    float duration = 5f;
    float currentTime = 0f;
    Vector3 endPosition = new Vector3(-6.8f, 1.201778f, 100f);

    Button selectedMed = null;

    void Start()
    {
        Sprite auraImage = Resources.Load<Sprite>("General/GenericAura");
        Sprite uiMask = page1.GetComponentInChildren<Button>().GetComponent<Image>().sprite; // Save the uiMask to restore later
        LoadingOverlay overLay = FindObjectOfType<LoadingOverlay>();
        nextPage.onClick.AddListener(() =>
        {
            Sprite loadingImage = Resources.Load<Sprite>("Task3/BigPill");
            overLay.SetLoadingIconImage(loadingImage); // change loading icon to turnip
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
                ResetButtonIcons(oldPage.GetComponentsInChildren<Button>().ToList(), oldPage, uiMask);
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
                ResetButtonIcons(oldPage.GetComponentsInChildren<Button>().ToList(), oldPage, uiMask);
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
            task.AddScoreAndReason(-250, "Prescription: Shared sensitive health data.");
        });
        submit.onClick.AddListener(() =>
        {
            if (selectedMed.name.StartsWith("CorrectButton"))
            {
                task.AddScoreAndReason(500, "Retrieved your prescription!");
                overLay.SetLoadingIconImage(); // Change loading icon back to default
                FindObjectOfType<GameStateManager>().BackToHomeScreen();
            }
            else
            {
                if (!errorMessageOn)
                {
                    errorMessageOn = true;
                    errorMessage.gameObject.SetActive(true);
                    FadeErrorMessageOut();
                }
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
                    ResetButtonIcons(page.GetComponentsInChildren<Button>().ToList(), page, uiMask);
                    // Change sprite of current button to be selected
                    button.GetComponent<Image>().sprite = auraImage;
                    button.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                    selectedMed = button;
                    submit.gameObject.SetActive(true);
                });

                string buttonIconName = GetButtonIconName(button);
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
                    if (selectedMed == null)
                    {
                        buttonIcon.transform.localScale = new Vector3(1f, 1f, 1f);
                    }
                    else if (buttonIcon.name.Last() != selectedMed.name.Last())
                    {
                        buttonIcon.transform.localScale = new Vector3(1f, 1f, 1f);
                    }
                });
                trigger.triggers.Add(enterEntry);
                trigger.triggers.Add(exitEntry);
            });
        });
    }

    void ResetButtonIcons(List<Button> buttons, GameObject page, Sprite uiMask)
    {
        buttons.ForEach(button =>
        {
            button.GetComponent<Image>().sprite = uiMask;
            button.GetComponent<Image>().color = new Color32(0, 0, 0, 255);
            string buttonIconName = GetButtonIconName(button);
            GameObject buttonIcon = page.transform.Find(buttonIconName).gameObject;
            buttonIcon.transform.localScale = new Vector3(1f, 1f, 1f);
        });
    }

    string GetButtonIconName(Button button)
    {
        if (button.name.StartsWith("CorrectButton"))
        {
            return "CorrectButtonIcon" + button.name.Last();
        }
        else
        {
            return "ButtonIcon" + button.gameObject.name.Last();
        }
    }

    void FadeErrorMessageOut()
    {
        StartCoroutine(FadeCanvasGroup(errorMessage, errorMessage.alpha, 0));
    }

    public IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration = 1f)
    {
        yield return new WaitForSeconds(2); // Delay the routine by 1 second

        float startTime = Time.time;
        float timeSinceStart;
        float percentComplete;

        while (true)
        {
            timeSinceStart = Time.time - startTime;
            percentComplete = timeSinceStart / duration;

            cg.alpha = Mathf.Lerp(start, end, percentComplete);

            if (percentComplete >= 1)
            {
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        // Reset the error message for the next time
        errorMessage.gameObject.SetActive(false);
        errorMessage.alpha = 1;
        errorMessageOn = false;
    }

    // Controls the animation of the "smartwatch detected" popup
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
