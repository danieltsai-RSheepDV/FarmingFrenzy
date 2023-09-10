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
        settingsPanel = GameObject.FindGameObjectWithTag(settingsName);
        ToggleSettings();

        // retrieve values from playerprefs across scenes
        musicVolume = PlayerPrefs.GetFloat(MIXER_MUSIC);
        sfxVolume = PlayerPrefs.GetFloat(MIXER_SFX);
        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;
        mixer.SetFloat(MIXER_MUSIC, musicVolume);
        mixer.SetFloat(MIXER_SFX, sfxVolume);

        //fullscreen on / off
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
        musicVolume = Mathf.Log10(volume) * 20;
        PlayerPrefs.SetFloat(MIXER_MUSIC, musicVolume);
        mixer.SetFloat(MIXER_MUSIC, musicVolume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Log10(volume) * 20;
        PlayerPrefs.SetFloat(MIXER_SFX, sfxVolume);
        mixer.SetFloat(MIXER_SFX, sfxVolume);
    }
}
