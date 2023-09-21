using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource bgm;
    [SerializeField] AudioSource uiSound;

    [SerializeField] AudioClip[] uiAudioClips; // 0 is button click, 1 is window open, 2 is success twinkle
    [SerializeField] AudioClip[] actionClips; // 0 is rake sound, 1 is water sound, 2 is placement sound
    [SerializeField] AudioClip[] enemyClips; // 0 is potato tumble sound, 1 is pea shooting sound

    // UI sounds
    public void PlayClickSound()
    {
        uiSound.PlayOneShot(uiAudioClips[0]); // play button click sound
    }

    public void PlayUIOpenSound()
    {
        uiSound.PlayOneShot(uiAudioClips[1]); // play window open sound
    }

    public void PlaySuccessSound()
    {
        uiSound.PlayOneShot(uiAudioClips[2]);
    }

    // Action sounds
    public void PlayTillSound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(actionClips[0]);
    }
    public void PlayWaterSound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(actionClips[1]);
    }
    public void PlayPlacedSound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(actionClips[2]);
    }

    // Enemy sounds
    public void PlayPotatoSound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(enemyClips[0]);
    }

    public void PlayPeaShootSound(AudioSource audioSource)
    {
        audioSource.PlayOneShot(enemyClips[1]);
    }
}
