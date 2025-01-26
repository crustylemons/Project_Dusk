using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DTTAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip denied;

    public void PlayDenied()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(denied);
        }
    }
}
