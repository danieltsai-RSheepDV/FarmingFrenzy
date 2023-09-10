using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menus : MonoBehaviour
{
    [SerializeField] string gameName;
    [SerializeField] string settingsName = "Settings";
    GameObject settingsPanel;
    bool settingsOpen = true; // settings
    bool fullScreenOff = false; // controls fullscreen toggle

    // audio mixer
    // https://www.youtube.com/watch?v=pbuJUaO-wpY&ab_channel=KapKoder
    [SerializeField] AudioMixer mixer;
    const string MIXER_MUSIC = "MusicParam";
    const string MIXER_SFX = "SFXParam";

    // Start is called before the first frame update
    void Start()
    {
        settingsPanel = GameObject.FindGameObjectWithTag(settingsName);
        ToggleSettings();

        Screen.fullScreen = fullScreenOff;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(gameName);
    }

    public void ToggleSettings()
    {
        settingsOpen = !settingsOpen;
        settingsPanel.SetActive(settingsOpen);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToggleFullScreen(bool screenStatus)
    {
        fullScreenOff = screenStatus;
        Screen.fullScreen = screenStatus;
    }

    // audio
    public void SetMusicVolume(float volume)
    {
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(volume) * 20);
    }
}
