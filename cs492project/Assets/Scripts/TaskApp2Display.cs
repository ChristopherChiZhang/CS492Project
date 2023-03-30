using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TaskApp2Display : MonoBehaviour
{
    public TaskApp task;  // Corresponding task app to this display

    public GameObject page1;  // First page
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

    public void FocusAddress()
    {
        addressSelected = true;

    }

    void Start()
    {
        page1Checkout.onClick.AddListener(() =>
        {
            page1.SetActive(false);
            page2.SetActive(true);
            currentPage++;
        });

        page2DenyLoc.onClick.AddListener(() =>
        {
            
            page2PopupLoc.SetActive(false);
        });

        page2AllowLoc.onClick.AddListener(() =>
        {
            task.AddScoreAndReason(-200, "Shared location data.");
            page2AddressInput.text = address;
            page2Confirm.interactable = true;
            page2PopupLoc.SetActive(false);
        });

        page2Confirm.onClick.AddListener(() =>
        {
            page2.SetActive(false);
            page3.SetActive(true);
            currentPage++;
            task.AddScoreAndReason(500, "Bought this week’s groceries!");
        });


    }

    void Update()
    {
        t += Time.deltaTime;

        if (currentPage == 2 && addressSelected)
        {
            // Input cursor effect
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
                    } else
                    {
                        page2Confirm.interactable = true;
                    }
                }
            }
        }
    }
}
