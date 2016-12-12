using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class AudioHelper {

    public static void PlayPitchAudio(AudioSource audioSource, AudioClip audioClip)
    {
        PlayPitchAudio(audioSource, audioClip, .7f, 1.3f);
    }

    public static void PlayPitchAudio(AudioSource audioSource, AudioClip audioClip, float minPitch, float maxPitch)
    {
        float randomPitch = Random.Range(minPitch, maxPitch);
        audioSource.pitch = randomPitch;

        audioSource.PlayOneShot(audioClip);
    }

}
