using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TaskApp5Display : MonoBehaviour
{
    public TaskApp task;  // Corresponding task app to this display

    public GameObject page1;
    public GameObject page2;
    public GameObject page3;
    public GameObject page4;
    public GameObject page5;
    public GameObject page6;

    public Button page1Next;
    public Button page2Next;
    public Button page3Next;
    public Button page4Next;
    public Button page5Next;

    public TextMeshProUGUI page3Username;
    public TextMeshProUGUI page3Password1;
    public TextMeshProUGUI page3Password2;

    public TextMeshProUGUI page4Input;
    public GameObject page4Error;

    public Toggle page5Toggle1;
    public Toggle page5Toggle2;
    public Toggle page5Toggle3;
    public Toggle page5Toggle4;
    public Toggle page5Toggle5;
    public Toggle page5Toggle6;
    public Toggle page5Toggle7;
    public Toggle page5Toggle8;

    private int currentPage = 1;

    private TextMeshProUGUI currInput = null;
    private static string username = "chadgpt1984";
    private static string password = "************";
    private static string captcha = "todo";
    private float t = 0f;
    private int x = 0;

    void Start()
    {
        LoadingOverlay overlay = FindObjectOfType<LoadingOverlay>();
        overlay.SetLoadingIconImage();

        // Page 1 -> 2
        page1Next.onClick.AddListener(() =>
        {
            overlay.DelayedExecute(() =>
            {
                page1.SetActive(false);
                page2.SetActive(true);
                currentPage++;
            });
        });

        // Page 2 -> 3
        page2Next.onClick.AddListener(() =>
        {
            overlay.DelayedExecute(() =>
            {
                page2.SetActive(false);
                page3.SetActive(true);
                currentPage++;
            });
        });

        // Page 3 -> 4
        page3Next.onClick.AddListener(() =>
        {
            overlay.DelayedExecute(() =>
            {
                page3.SetActive(false);
                page4.SetActive(true);
                currInput = null;  // Deselect page 3 input
                Keyboard.current.onTextInput += OnPage4TextInput;  // Enable page 4 input listener
                currentPage++;
            });
        });

        // Page 4 -> 5
        page4Next.onClick.AddListener(() =>
        {
            page4Error.SetActive(false);
            overlay.DelayedExecute(() =>
            {
                // Check captcha
                if (page4Input.text == captcha)
                {
                    page4.SetActive(false);
                    page5.SetActive(true);
                    Keyboard.current.onTextInput -= OnPage4TextInput;  // Disable page 4 input listener
                    currentPage++;
                } else
                {
                    page4Error.SetActive(true);
                    page4Input.text = "";
                    currInput = null;
                }
            });
        });

        // Page 5 -> 6
        page5Next.onClick.AddListener(() =>
        {
            overlay.DelayedExecute(() =>
            {
                page5.SetActive(false);
                page6.SetActive(true);
                currentPage++;

                // Read checkboxes
                if (page5Toggle1.isOn)
                {
                    task.AddScoreAndReason(-50, "Social Media: Shared app data.");
                }
                if (!page5Toggle2.isOn)
                {
                    task.AddScoreAndReason(-50, "Social Media: Shared location data.");
                }
                if (page5Toggle3.isOn)
                {
                    task.AddScoreAndReason(-50, "Social Media: Subscribed to monthly newsletter.");
                }

                if (!page5Toggle4.isOn)
                {
                    task.AddScoreAndReason(-50, "Social Media: Subscribed to weekly newsletter.");
                }
                if (!page5Toggle5.isOn)
                {
                    task.AddScoreAndReason(-50, "Social Media: Subscribed to daily newsletter.");
                }
                if (page5Toggle6.isOn || page5Toggle7.isOn)
                {
                    task.AddScoreAndReason(-50, "Social Media: Allowed camera access.");
                }
                if (page5Toggle8.isOn)
                {
                    task.AddScoreAndReason(-50, "Social Media: Shared data to personalize ads.");
                }

                task.AddScoreAndReason(500, "Signed up for Honkr!");
            });
        });

        // Page 5 checkbox that unchecks everything
        page5Toggle6.onValueChanged.AddListener((bool selected) =>
        {
            if (!selected)
            {
                page5Toggle1.isOn = false;
                page5Toggle2.isOn = false;
                page5Toggle3.isOn = false;
                page5Toggle4.isOn = false;
                page5Toggle5.isOn = false;
                page5Toggle7.isOn = false;
                page5Toggle8.isOn = false;
            }
        });
    }

    public void OnPage3UsernameClick()
    {
        currInput = page3Username;
        page3Password1.text = page3Password1.text.TrimEnd('|');
        page3Password2.text = page3Password2.text.TrimEnd('|');
    }

    public void OnPage3Password1Click()
    {
        currInput = page3Password1;
        page3Username.text = page3Username.text.TrimEnd('|');
        page3Password2.text = page3Password2.text.TrimEnd('|');
    }

    public void OnPage3Password2Click()
    {
        currInput = page3Password2;
        page3Username.text = page3Username.text.TrimEnd('|');
        page3Password1.text = page3Password1.text.TrimEnd('|');
    }

    public void OnPage4InputClick()
    {
        currInput = page4Input;
    }

    private void OnPage4TextInput(char c)
    {
        if (currInput != null)
        {
            currInput.text = currInput.text.TrimEnd('|');
            currInput.text += c;
        }
    }

    void Update()
    {
        t += Time.deltaTime;

        // Page 3 typing
        if (currInput != null)
        {
            if (currentPage == 3)
            {
                // Input cursor blinking effect
                if (t >= 0.5f)
                {
                    t = 0f;
                    if (currInput.text.EndsWith("|"))
                    {
                        currInput.text = currInput.text.TrimEnd('|');
                    }
                    else
                    {
                        currInput.text += '|';
                    }
                }

                // Keyboard input
                if (Input.anyKeyDown && !Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1))
                {
                    // For every other key press
                    x = (x + 1) % 2;
                    if (x == 0)
                    {
                        // Add character
                        string target = currInput == page3Username ? username : password;
                        if (currInput.text.TrimEnd('|').Length < target.Length)
                        {
                            currInput.text = currInput.text.TrimEnd('|');
                            currInput.text += target[currInput.text.Length];
                        }
                        page3Next.interactable = page3Username.text.TrimEnd('|').Length == username.Length && page3Password1.text.TrimEnd('|').Length == password.Length && page3Password2.text.TrimEnd('|').Length == password.Length;
                    }
                }
            }

            // Page 4 input
            else if (currentPage == 4)
            {
                // Input cursor blinking effect
                if (t >= 0.5f)
                {
                    t = 0f;
                    if (currInput.text.EndsWith("|"))
                    {
                        currInput.text = currInput.text.TrimEnd('|');
                    }
                    else
                    {
                        currInput.text += '|';
                    }
                }

                // Backspace input
                if (Input.GetKeyDown(KeyCode.Backspace))
                {
                    if (currInput != null)
                    {
                        currInput.text = currInput.text.TrimEnd('|');
                        currInput.text = currInput.text.Remove(currInput.text.Length - 1);
                    }
                }
            }
        }
    }
}
