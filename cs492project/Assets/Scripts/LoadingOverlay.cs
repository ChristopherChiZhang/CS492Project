using System;
using System.Collections;
using UnityEngine;

public class LoadingOverlay : MonoBehaviour
{
    public GameObject loadingScreen;
    public GameObject loadingIcon;

    private IEnumerator LoadCoroutine(Action onFinish, float seconds)
    {
        Show();
        yield return new WaitForSeconds(seconds);
        Hide();
        if (onFinish != null)
        {
            onFinish();
        }
    }

    // Show loading screen for a set amount of seconds
    public void Show(Action onFinish = null, float seconds = 1f)
    {
        StartCoroutine(LoadCoroutine(onFinish, seconds));
    }

    // Show loading screen until manually hidden
    public void Show()
    {
        loadingScreen.gameObject.SetActive(true);
    }

    // Hide loading screen
    public void Hide()
    {
        loadingScreen.gameObject.SetActive(false);
    }

    void Update()
    {
        loadingIcon.gameObject.transform.Rotate(0.0f, 0.0f, -Time.deltaTime * 200, Space.Self);
    }
}
