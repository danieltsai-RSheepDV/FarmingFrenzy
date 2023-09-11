using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menus : MonoBehaviour
{
    [SerializeField] string mainMenuName;
    [SerializeField] string gameName;
    [SerializeField] GameObject settingsPanel;
    bool settingsOpen = true; // settings
    bool fullScreenOff = false; // controls fullscreen toggle

    // audio mixer
    // https://www.youtube.com/watch?v=pbuJUaO-wpY&ab_channel=KapKoder
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    float musicVolume = 1f;
    float sfxVolume = 1f;

    const string MIXER_MUSIC = "MusicParam";
    const string MIXER_SFX = "SFXParam";

    // Start is called before the first frame update
    void Start()
    {
        // settings off
        if(settingsPanel == null) settingsPanel = GameObject.FindGameObjectWithTag("Settings");
        ToggleSettings();

        // retrieve values from playerprefs across scenes
        float value;
        mixer.GetFloat(MIXER_MUSIC, out value);
        musicVolume = Mathf.Pow(10, value / 20);

        mixer.GetFloat(MIXER_SFX, out value);
        sfxVolume = Mathf.Pow(10, value / 20);

        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;

        //fullscreen on / off
        Screen.fullScreen = fullScreenOff;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(gameName);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuName);
    }

    public void ToggleSettings()
    {
        settingsOpen = !settingsOpen;
        settingsPanel.SetActive(settingsOpen);
        Time.timeScale = settingsOpen ? 0f : 1f; // turn on or off time
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
        musicVolume = Mathf.Log10(volume) * 20;
        mixer.SetFloat(MIXER_MUSIC, musicVolume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Log10(volume) * 20;
        mixer.SetFloat(MIXER_SFX, sfxVolume);
    }
}
