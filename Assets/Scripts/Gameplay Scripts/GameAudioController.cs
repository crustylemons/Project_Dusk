using System.Collections;
using UnityEngine;

public class GameAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource music;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip denied;
    [SerializeField] private AudioClip countDown;
    [SerializeField] private AudioClip finished;

    public void PlayDenied()
    {
        if (audioSource != null)
        {
            audioSource.PlayOneShot(denied);
        }
        else
        {
            Debug.Log("Audio Source is null");
        }
    }

    public void StartCountDown()
    {
        if (audioSource != null)
        {
            music.Stop();
            StartCoroutine(CountDownNoise());
        }
        else
        {
            Debug.Log("Audio Source is null");
        }
    }

    public IEnumerator CountDownNoise()
    {
        for (int i = 0; i <= 3; i++)
        {
            audioSource.PlayOneShot(countDown);
            yield return new WaitForSeconds(1);
        }

        music.Play();
    }

    public void StopMusic()
    {
        music.Stop();
    }

    public void PlayTestFinished()
    {
        if (finished)
        {
            audioSource.PlayOneShot(finished);
        }
    }
}
