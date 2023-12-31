using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    private Canvas canvas;
    [SerializeField] Toggle toggle;

    bool isFullScreen = false;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
        
        // 0 for windowed, 1 for full screen
        isFullScreen = Screen.fullScreen; // set screenstate to match current screen state
        toggle.isOn = isFullScreen;
    }

    public void ToggleSettings(bool b)
    {
        canvas.enabled = b;
        Time.timeScale = canvas.enabled ? 0f : 1f; // turn on or off time
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleFullScreen(bool screenStatus)
    {
        isFullScreen = screenStatus;
        Screen.fullScreen = isFullScreen;
    }
}
