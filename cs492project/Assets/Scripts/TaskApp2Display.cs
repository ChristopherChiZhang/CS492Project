using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskApp2Display : MonoBehaviour
{
    public TaskApp task;  // Corresponding task app to this display

    public GameObject page1;  // First page
    public Toggle page1ShareData;
    public Button page1Checkout;

    public GameObject page2;  // Second page
    public TextMeshProUGUI page2AddressInput;
    public GameObject page2PopupLoc;
    public Button page2DenyLoc;
    public Button page2AllowLoc;
    public Button page2Confirm;

    public GameObject page3;  // Third page

    private int currentPage = 1;
    private static string address = "200 University Ave W, Waterloo, ON";  // FInal address
    private bool addressSelected = false;  // If address box has been selected
    private float t = 0f;  // Timer
    private int x = 0;  // Counter to make sure only every few key presses adds a letter to the address

    // Page 2: player clicks on address box
    public void FocusAddress()
    {
        addressSelected = true;
    }

    void Start()
    {
        LoadingOverlay overLay = FindObjectOfType<LoadingOverlay>();

        // Page 1 -> page 2
        page1Checkout.onClick.AddListener(() =>
        {
            Sprite loadingImage = Resources.Load<Sprite>("Task2/TurnipIcon");
            overLay.SetLoadingIconImage(loadingImage); // change loading icon to turnip
            overLay.DelayedExecute(() =>
            {
                // Check toggle
                if (page1ShareData.isOn)
                {
                    task.AddScoreAndReason(-150, "Shared shopping habits.");
                }

                page1.SetActive(false);
                page2.SetActive(true);
                currentPage++;

                // Delay popup for a bit
                overLay.DelayedExecute(() =>
                {
                    page2PopupLoc.SetActive(true);
                }, 0.5f, false);
            });
        });

        // Page 2: deny location
        page2DenyLoc.onClick.AddListener(() =>
        {
            page2PopupLoc.SetActive(false);
        });

        // Page 2: allow location
        page2AllowLoc.onClick.AddListener(() =>
        {
            page2PopupLoc.SetActive(false);
            overLay.DelayedExecute(() =>
            {
                task.AddScoreAndReason(-150, "Shared location data.");
                // Autofill address and enable going to page 3
                page2AddressInput.text = address;
                page2Confirm.interactable = true;
            });
        });

        // Page 2 -> page 3
        page2Confirm.onClick.AddListener(() =>
        {
            overLay.DelayedExecute(() =>
            {
                page2.SetActive(false);
                page3.SetActive(true);
                currentPage++;
                task.AddScoreAndReason(500, "Bought this week’s groceries!");
                overLay.SetLoadingIconImage(); // Change loading icon back to default
            });
        });


    }

    void Update()
    {
        t += Time.deltaTime;

        // Handle typing in page 2 address box
        if (currentPage == 2 && addressSelected)
        {
            // Input cursor blinking effect
            if (t >= 0.5f)
            {
                t = 0f;
                if (page2AddressInput.text.EndsWith("|"))
                {
                    page2AddressInput.text = page2AddressInput.text.TrimEnd('|');
                }
                else
                {
                    page2AddressInput.text += '|';
                }
            }

            // Keyboard input
            if (Input.anyKeyDown && !Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1))
            {
                x = (x + 1) % 2;
                if (x == 0)
                {
                    // Add letter to address
                    if (page2AddressInput.text.TrimEnd('|').Length < address.Length)
                    {
                        page2AddressInput.text = page2AddressInput.text.TrimEnd('|');
                        page2AddressInput.text += address[page2AddressInput.text.Length];
                        page2Confirm.interactable = page2AddressInput.text.Length == address.Length;
                    }
                    else
                    {
                        page2Confirm.interactable = true;
                    }
                }
            }
        }
    }
}
