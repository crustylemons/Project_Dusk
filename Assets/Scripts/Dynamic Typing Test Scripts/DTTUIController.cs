using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DTTUIController : MonoBehaviour
{
    [Header("Before Test UI")]
    [SerializeField] private GameObject beginningBox;

    [Header("While Typing UI")]
    [SerializeField] private GameObject wordBox;
    [SerializeField] private List<GameObject> typingText;
    [SerializeField] private GameObject timer;

    [Header("End Game UI")]
    [SerializeField] private GameObject endingBox;
    [SerializeField] private GameObject WPMBox;
    [SerializeField] private GameObject accuracyBox;
    [SerializeField] private GameObject leaderboard;
    [SerializeField] private GameObject playAgainBtn;
    [SerializeField] private GameObject continueBtn;

    [Header("Other")]
    [SerializeField] private GameObject blackTransition;

    private void Awake()
    {
        blackTransition.SetActive(true);
        InitializeTestQuestions();
    }

    public void SetWPM(int wpm)
    {
        Text text = WPMBox.GetComponentInChildren<Text>();
        text.text = $"WPM = {wpm}";
    }

    public void SetAccuracy(int accuracy)
    {
        Text text = accuracyBox.GetComponentInChildren<Text>();
        text.text = $"Accuracy = {accuracy}%";
    }

    public void SetTimer(int seconds)
    {
        timer.GetComponentInChildren<Text>().text = seconds.ToString();
    }

    public void InitializeTestQuestions()
    {
        // Inactive
        wordBox.SetActive(false);
        endingBox.SetActive(false);
        timer.SetActive(false);

        // Active
        beginningBox.SetActive(true);
    }

    public void StartTest()
    {
        // Inactive
        beginningBox.SetActive(false);
        endingBox.SetActive(false);

        // Active
        wordBox.SetActive(true);
        timer.SetActive(true);

    }

    public void EndTest()
    {
        wordBox.SetActive(false);
        timer.SetActive(false);

        endingBox.SetActive(true);
    }
}
