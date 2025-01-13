using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    [Header("Statistics")]
    [SerializeField] private float wordsPerMinute;
    [SerializeField] private float accuracy;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("playerStats");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public int GetAccuracy() { return Mathf.RoundToInt(accuracy); }

    public int GetWordsPerMinute() { return Mathf.RoundToInt(wordsPerMinute); }

    public void SetWordsPerMinute(int correctCharactersTyped, float minutes)
    {
        int keyStokesPerMinute = correctCharactersTyped / 5;
        wordsPerMinute = Mathf.RoundToInt(Mathf.Clamp(keyStokesPerMinute / minutes, 0.0001f, 1000f));
    }

    public void SetAccuracy(float correctKeysPressed, float totalKeysPressed)
    {
        accuracy = (correctKeysPressed / totalKeysPressed) * 100;
    }

    /// <summary>
    /// Calculates WPM and Accuracy, then communicates to the UI to begin the ending sequence when the typing test is complete
    /// </summary>
    /// <param name="correctKeysPressed">The amount of correct characters typed</param>
    /// <param name="totalKeysPressed">The amount of characters typed</param>
    /// <param name="minutes">Minutes the player has been typing</param>
    public void GetDataEndTest(int correctKeysPressed, int totalKeysPressed, float minutes)
    {
        // Calculates & sets player statistics
        SetWordsPerMinute(correctKeysPressed, minutes);
        SetAccuracy(correctKeysPressed, totalKeysPressed);
    }
}
