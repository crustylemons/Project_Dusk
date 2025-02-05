using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    [Header("Before Playing UI")]
    [SerializeField] private GameObject beginningBox;
    [SerializeField] private GameObject bushes;
    [SerializeField] private GameObject grayOut;

    [Header("Typing Test UI")]
    [SerializeField] private GameObject typingTestBeginningBox;
    [SerializeField] private GameObject typingTestEndingBox;
    [SerializeField] private List<Button> buttonList;
    private string handMode;

    [Header("Stray Trails UI")]
    [SerializeField] private GameObject strayTrailsBeginningBox;
    [SerializeField] private GameObject strayTrailsEndingBox;
    [SerializeField] private GameObject itemUIPrefab;
    [SerializeField] private GameObject endScoreText;
    [SerializeField] private GameObject totalItemsText;
    [SerializeField] private GameObject scoreDisplay;
    [SerializeField] private GameObject catBedNumDisplay;
    [SerializeField] private GameObject couchNumDisplay;

    [Header("While In Typing Test UI")]
    [SerializeField] private GameObject wordBox;
    [SerializeField] private List<GameObject> typingText;
    [SerializeField] private GameObject timer;

    [Header("End Game UI")]
    [SerializeField] private GameObject WPMBox;
    [SerializeField] private GameObject accuracyBox;
    [SerializeField] private GameObject playAgainBtn;
    [SerializeField] private GameObject continueBtn;

    [Header("Other Connections")]
    [SerializeField] private GameObject blackTransition;
    [SerializeField] private GameObject esc;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip escOpen;
    [SerializeField] private AudioClip escClose;
    private TypingTestStatsManager playerStats;
    private SaveDataManager saveData;


    private void Awake()
    {
        blackTransition.SetActive(true);
        InitializePlayChoice();

        playerStats = FindAnyObjectByType<TypingTestStatsManager>();
        saveData = FindFirstObjectByType<SaveDataManager>();
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

    public void SetTimer(int seconds, bool isTypingTest)
    {
        if (isTypingTest)
        {
            timer.GetComponentInChildren<Text>().text = seconds.ToString();
        }
        else
        {
            scoreDisplay.GetComponentInChildren<Text>().text = seconds.ToString();
        }
    }

    public void InitializePlayChoice()
    {
        // Inactive
        wordBox.SetActive(false);
        typingTestBeginningBox.SetActive(false);
        typingTestEndingBox.SetActive(false);
        strayTrailsEndingBox.SetActive(false);
        timer.SetActive(false);

        // Active
        beginningBox.SetActive(true);
        grayOut.SetActive(true);
        bushes.SetActive(true);
    }

    public void StartStrayTrails()
    {
        // Inactive
        beginningBox.SetActive(false);
        strayTrailsEndingBox.SetActive(false);
        grayOut.SetActive(false);
        bushes.SetActive(false);

        // Active
        scoreDisplay.SetActive(true);
    }

    public void StopStrayTrails()
    {
        // Update Values
        int score = saveData.GetRecentScore();
        int highScore = saveData.GetHighScore();
        string scoreString;
        if (score > highScore) { scoreString = "NEW HIGH Score = "; }
        else { scoreString = "Score = "; }

        endScoreText.GetComponent<Text>().text = scoreString + score;
        totalItemsText.GetComponent<Text>().text = "Total Collected Items = " + saveData.GetTotalCollected().ToString();

        //couchNumDisplay.GetComponent<Text>().text = saveData.Get
        // Update Certain Numbers here

        // Active
        strayTrailsEndingBox.SetActive(true);
        grayOut.SetActive(true);
        bushes.SetActive(true);

        // Inactive
        scoreDisplay.SetActive(false);
    }

    // Score for Stray Trails mode
    public void UpdateScore(int scoreNum) { scoreDisplay.GetComponentInChildren<Text>().text = scoreNum.ToString(); }

    public void InitializeTypingTestOptions()
    {
        // Inactive
        wordBox.SetActive(false);
        typingTestEndingBox.SetActive(false);
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
        typingTestEndingBox.SetActive(false);
        grayOut.SetActive(false);
        bushes.SetActive(false);

        // Active
        wordBox.SetActive(true);
        timer.SetActive(true);
    }

    public void EndTypingTest()
    {
        // Set values of stats in the UI
        SetWPM(playerStats.GetWordsPerMinute());
        SetAccuracy(playerStats.GetAccuracy());

        // UI enabling/disabling
        wordBox.SetActive(false);
        timer.SetActive(false);

        typingTestEndingBox.SetActive(true);
        grayOut.SetActive(true);
        bushes.SetActive(true);
    }
    
    public string GetTypingTestHandMode() { return handMode; }

    // Sets the chosen hand mode to be used in the typing test
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
    
    public GameObject CreateNewItemUI()
    {
        GameObject itemUI = Instantiate(itemUIPrefab, canvas.transform);
        return itemUI;
    }
}
