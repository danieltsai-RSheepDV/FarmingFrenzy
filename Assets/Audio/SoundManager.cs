using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource bgm;
    [SerializeField] AudioSource uiSound;

    [SerializeField] AudioClip[] uiAudioClips; // 0 is button click, 1 is window open
    [SerializeField] AudioClip[] actionClips; // 0 is rake sound, 1 is water sound, 2 is placement sound

    // UI sounds
    public void PlayClickSound()
    {
        uiSound.PlayOneShot(uiAudioClips[0]); // play button click sound
    }

    public void PlayUIOpenSound()
    {
        uiSound.PlayOneShot(uiAudioClips[1]); // play window open sound
    }

    // Action sounds

}
