using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    const string MIXER_MUSIC = "MusicParam";
    const string MIXER_SFX = "SFXParam";
    
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    float musicVolume = 1f;
    float sfxVolume = 1f;

    // Start is called before the first frame update
    void Start()
    {
        float value;
        mixer.GetFloat(MIXER_MUSIC, out value);
        musicVolume = Mathf.Pow(10, value / 20);

        mixer.GetFloat(MIXER_SFX, out value);
        sfxVolume = Mathf.Pow(10, value / 20);

        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;
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
