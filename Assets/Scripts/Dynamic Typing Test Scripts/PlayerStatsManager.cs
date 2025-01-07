using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    [SerializeField] private DTTUIController UIController;

    [Header("Statistics")]
    [SerializeField] private float wordsPerMinute;
    [SerializeField] private float currentSpeed;
    [SerializeField] private float accuracy;



    public float GetAccuracy() { return accuracy; }

    public float GetSpeed() { return currentSpeed; }

    public float GetWordsPerMinute() { return wordsPerMinute; }

    /// <summary>
    /// Calcluates the words per minute based on how many words the player typed divided by minutes given
    /// </summary>
    /// <param name="correctCharactersTyped">The amount of correct characters typed</param>
    /// <param name="minutes">Minutes the player has been typing</param>
    public void SetWordsPerMinute(int correctCharactersTyped, float minutes)
    {
        Debug.Log("minutes: " + minutes);
        int keyStokesPerMinute = correctCharactersTyped / 5;
        Debug.Log("KSPR: " + keyStokesPerMinute);

        wordsPerMinute = Mathf.Clamp(keyStokesPerMinute / minutes, 0.0001f, 1000f);

        // Displaying
        UIController.SetWPM(Mathf.RoundToInt(wordsPerMinute));
        Debug.Log($"WPM: {wordsPerMinute}");
    }

    public void SetAccuracy(int correctKeysPressed, int totalKeysPressed)
    {
        accuracy = (correctKeysPressed / totalKeysPressed) * 100;

        // Displaying
        UIController.SetAccuracy(Mathf.RoundToInt(accuracy));
        Debug.Log($"Accuracy: {accuracy}");
    }
}
