using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskAppDisplay4 : MonoBehaviour
{
    public TaskApp task;  // Corresponding task app to this display

    public Button cancelSubscription;

    public GameObject popup1;
    public Button popup1accept;
    public Button popup1deny;
    public GameObject popup2;
    public Button popup2accept;
    public Button popup2deny;
    public GameObject popup3;
    public Button popup3accept;
    public Button popup3deny;
    public GameObject popup4;
    public Button popup4accept;
    public Button popup4deny;
    public GameObject popup5;
    public Button popup5accept;
    public Button popup5deny;
    public GameObject popupEndSuccess;
    public GameObject popupEndFail;
    public Button endSuccessButton;
    public Button endFailButton;

    void Start()
    {

        // start point
        cancelSubscription.onClick.AddListener(() =>
        {
            popup1.SetActive(true);
        });

        popup1accept.onClick.AddListener(() =>
        {
            popup1.SetActive(false);
            popup2.SetActive(true);
        });
        popup1deny.onClick.AddListener(() =>
        {
            popup1.SetActive(false);
        });

        popup2accept.onClick.AddListener(() =>
        {
            popup2.SetActive(false);
            popup3.SetActive(true);
        });
        popup2deny.onClick.AddListener(() =>
        {
            popup2.SetActive(false);
        });

        popup3accept.onClick.AddListener(() =>
        {
            popup3.SetActive(false);
            popup4.SetActive(true);
        });
        popup3deny.onClick.AddListener(() =>
        {
            popup3.SetActive(false);
        });

        popup4accept.onClick.AddListener(() =>
        {
            popup4.SetActive(false);
            popup5.SetActive(true);
        });
        popup4deny.onClick.AddListener(() =>
        {
            popup4.SetActive(false);
        });

        popup5accept.onClick.AddListener(() =>
        {
            popup5.SetActive(false);
            popupEndSuccess.SetActive(true);
        });
        popup5deny.onClick.AddListener(() =>
        {
            popup5.SetActive(false);
            popupEndFail.SetActive(true);
        });

        endSuccessButton.onClick.AddListener(() =>
        {
            popupEndSuccess.SetActive(false);
            task.AddScoreAndReason(500, "Unsubscribed from that pesky newsletter!");

        });
        endFailButton.onClick.AddListener(() =>
        {
            popupEndFail.SetActive(false);
        });

    }

}
