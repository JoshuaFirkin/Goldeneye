using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    private AudioSource attachedAudioSrc;
    private float originalPitch;

    void Start()
    {
        attachedAudioSrc = transform.Find("Audio").GetComponent<AudioSource>();
        originalPitch = attachedAudioSrc.pitch;
    }

    public void PlayAttachedAudio(AudioClip _clip, float _pitch = 0)
    {
        if (attachedAudioSrc == null)
        {
            Debug.LogError("Could not access attached Audio Source object. Check that you've implemented it.");
            return;
        }

        if (_pitch == 0)
        {
            attachedAudioSrc.pitch = originalPitch;
        }
        else
        {
            attachedAudioSrc.pitch = _pitch;
        }

        attachedAudioSrc.clip = _clip;
        attachedAudioSrc.Play();
    }
}
