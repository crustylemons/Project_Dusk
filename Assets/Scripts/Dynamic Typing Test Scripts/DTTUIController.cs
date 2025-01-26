using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DTTUIController : MonoBehaviour
{
    [Header("Before Playing UI")]
    [SerializeField] private GameObject beginningBox;
    [SerializeField] private GameObject bushes;
    [SerializeField] private GameObject grayOut;

    [Header("Before Typing Test UI")]
    [SerializeField] private GameObject typingTestBeginningBox;
    [SerializeField] private List<Button> buttonList;
    private string handMode;

    // put before stray trails mode UI elements

    [Header("While Typing UI")]
    [SerializeField] private GameObject wordBox;
    [SerializeField] private List<GameObject> typingText;
    [SerializeField] private GameObject timer;

    [Header("End Game UI")]
    [SerializeField] private GameObject endingBox;
    [SerializeField] private GameObject WPMBox;
    [SerializeField] private GameObject accuracyBox;
    [SerializeField] private GameObject playAgainBtn;
    [SerializeField] private GameObject continueBtn;

    [Header("Other")]
    [SerializeField] private GameObject blackTransition;
    [SerializeField] private GameObject esc;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip escOpen;
    [SerializeField] private AudioClip escClose;
    private PlayerStatsManager playerStatsManager;


    private void Awake()
    {
        blackTransition.SetActive(true);
        InitializePlayChoice();

        playerStatsManager = FindFirstObjectByType<PlayerStatsManager>();
    }

    public void SetWPM(int wpm)
    {
        Text text = WPMBox.GetComponent<Text>();
        text.text = $"WPM = {wpm}";
    }

    public void SetAccuracy(int accuracy)
    {
        Text text = accuracyBox.GetComponent<Text>();
        text.text = $"Accuracy = {accuracy}%";
    }

    public void SetTimer(int seconds)
    {
        timer.GetComponentInChildren<Text>().text = seconds.ToString();
    }

    public void InitializePlayChoice()
    {
        // Inactive
        wordBox.SetActive(false);
        endingBox.SetActive(false);
        timer.SetActive(false);
        typingTestBeginningBox.SetActive(false);

        // Active
        beginningBox.SetActive(true);
        grayOut.SetActive(true);
        bushes.SetActive(true);
    }

    public void InitializeStrayTrailsOptions()
    {
        // do stuff
    }

    public void InitializeTypingTestOptions()
    {
        // Inactive
        wordBox.SetActive(false);
        endingBox.SetActive(false);
        timer.SetActive(false);
        beginningBox.SetActive(false);

        // Active
        typingTestBeginningBox.SetActive(true);
        grayOut.SetActive(true);
        bushes.SetActive(true);
    }

    public void StartTypingTest()
    {
        // Inactive
        typingTestBeginningBox.SetActive(false);
        endingBox.SetActive(false);
        grayOut.SetActive(false);
        bushes.SetActive(false);

        // Active
        wordBox.SetActive(true);
        timer.SetActive(true);
    }

    public void EndTypingTest()
    {
        // Set values of stats in the UI
        SetWPM(playerStatsManager.GetWordsPerMinute());
        SetAccuracy(playerStatsManager.GetAccuracy());

        // UI enabling/disabling
        wordBox.SetActive(false);
        timer.SetActive(false);

        endingBox.SetActive(true);
    }
    
    public string GetTypingTestHandMode() { return handMode; }


    /// <summary>
    /// Sets the chosen hand mode to be used in the typing test
    /// </summary>
    /// <param name="hand">what hand is being used to type</param>
    public void SetChosenHandMode(string hand)
    {
        handMode = hand.ToLower();
    }
    
    public void ToggleEsc()
    {
        if (esc.activeSelf == true)
        {
            audioSource.PlayOneShot(escClose);
            esc.SetActive(false);
        }
        else
        {
            audioSource.PlayOneShot(escOpen);
            esc.SetActive(true);
        }
    }
}
