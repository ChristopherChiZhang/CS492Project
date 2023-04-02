using System;
using System.Collections;
using UnityEngine;

public class LoadingOverlay : MonoBehaviour
{
    public GameObject loadingScreen;
    public GameObject loadingIcon;

    private IEnumerator DelayedRunCoroutine(Action callback, float seconds, bool showOverlay)
    {
        if (showOverlay) Show();
        yield return new WaitForSeconds(seconds);
        if (showOverlay) Hide();
        if (callback != null) callback();
    }

    // Wait for a set delay before executing callback()
    public void DelayedExecute(Action callback = null, float seconds = 1f, bool showOverlay = true)
    {
        StartCoroutine(DelayedRunCoroutine(callback, seconds, showOverlay));
    }

    // Show loading screen
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
