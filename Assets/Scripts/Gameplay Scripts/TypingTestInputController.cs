using JetBrains.Annotations;
using System.Collections;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class TypingTestInputController : MonoBehaviour
{
    [Header("Word Pools")]
    [SerializeField] private string[] possibleNormalWords;
    [SerializeField] private string[] possibleLeftHandWords;
    [SerializeField] private string[] possibleRightHandWords;
    private string[] chosenPossibleWords;

    [Header("Needed Connections")]
    [SerializeField] private GameUIController UIController;
    [SerializeField] private GameAudioController audioController;
    [SerializeField] private SceneManagement sceneManagement;
    [SerializeField] private TileMapController tileMapController;
    [SerializeField] private Animator catAnimator;

    [Header("Statistics")]
    private TypingTestStatsManager playerStatsManager;
    private int correctCharactersTyped;
    private int charactersTyped;
    [SerializeField] private int seconds = 30;

    private bool timerIsGoing;

    [Header("UI")]
    [SerializeField] private Text upcomingWordDisplay;
    [SerializeField] private Text wordDisplay;
    [SerializeField] private Text typedDisplay;

    private void Start()
    {
        playerStatsManager = FindFirstObjectByType<TypingTestStatsManager>();

        // Animation
        catAnimator.SetBool("IsMoving", false);
        tileMapController.SetCatIsRunning(false); 
    }

    private void Update()
    {
        // Go back home
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIController.ToggleEsc();
        }
    }

    // Gets Initiated from external sources
    public void StartTypingTest()
    {
        TypingTestActions();

        // Generate words
        chosenPossibleWords = FindPoolOfWords();
        GenerateUpcomingText(chosenPossibleWords);
    }

    public void TypingTestActions()
    {
        // Animation
        catAnimator.SetBool("IsMoving", true);
        tileMapController.SetCatIsRunning(true);

        // Function calling
        UIController.StartTypingTest();
        StartCoroutine(GetPlayerInput());
    }

    // Find pool of words based on player's chosen game mode
    private string[] FindPoolOfWords()
    {
        switch (UIController.GetTypingTestHandMode())
        {
            case "left hand":
                Debug.Log("left hand chosen");
                return possibleLeftHandWords;
            case "right hand":
                Debug.Log("right hand chosen");
                return possibleRightHandWords;
            default:
                Debug.Log("normal chosen");
                return possibleNormalWords;
        }
    }

    private void GenerateUpcomingText(string[] wordPool)
    {
        if (wordPool == null || wordPool.Length < 2)
        {
            Debug.Log("Word pool is not the correct length or is null");
            return;
        }

        // Generate a line of text (into a single string) that isn't too long for the text box
        string lineOfWordsDisplay = "";
        string lastWord = "";
        while (lineOfWordsDisplay.Length < 30)
        {
            
            while (true)
            {
                string randomWord = wordPool[Random.Range(0, wordPool.Length)] + " ";

                // Ensure there isn't two words next to another
                if (randomWord != lastWord)
                {
                    lineOfWordsDisplay += randomWord;
                    lastWord = randomWord;
                    break;
                }
            }
        }

        // Apply the line of text to the appropriate display
        if (wordDisplay.text == "" && upcomingWordDisplay.text != "")
        {
            wordDisplay.text = upcomingWordDisplay.text;
            upcomingWordDisplay.text = "";
            GenerateUpcomingText(chosenPossibleWords);
        }
        else
        {
            upcomingWordDisplay.text = lineOfWordsDisplay;
        }
    }

    private IEnumerator GetPlayerInput()
    {
        // Clear previous data
        correctCharactersTyped = 0;
        charactersTyped = 0;

        // Countdown
        audioController.StartCountDown();
        yield return StartCoroutine(Timer(3));
        

        StartCoroutine(Timer(seconds));
        while (timerIsGoing)
        {
            // Each line of words (each a single string)
            foreach (char c in wordDisplay.text)
            {
                yield return null;
                KeyCode value = (KeyCode)c;
                while (timerIsGoing)
                {   
                    charactersTyped++;
                    yield return new WaitUntil(() => Input.anyKeyDown || !timerIsGoing);
                    if (Input.inputString.ToLower() == c.ToString().ToLower())
                    {
                        typedDisplay.text += c;
                        correctCharactersTyped++;
                        break; // Exit the loop and proceed to the next character
                    }
                    else if (!Input.GetKeyUp(KeyCode.Escape))
                    {
                        audioController.PlayDenied();
                    }
                }
            }

            // Reset for the next word
            typedDisplay.text = "";
            wordDisplay.text = "";
            GenerateUpcomingText(chosenPossibleWords);
        }

        // End Input intake
        EndTestActions();
    }

    private void EndTestActions()
    {
        // Give Data
        playerStatsManager.GetDataEndTest(correctCharactersTyped, charactersTyped, seconds / 60f);
        
        // UI
        UIController.EndTypingTest();

        // Animation
        catAnimator.SetBool("IsMoving", false);
        tileMapController.SetCatIsRunning(false);

        // Audio
        audioController.PlayTestFinished();
        audioController.StopMusic();
    }

    private IEnumerator Timer(int seconds)
    {
        timerIsGoing = true;
        int secondsLeft = seconds;
        UIController.SetTimer(secondsLeft, true);

        for (int i = seconds; i > 0; i--)
        {
            yield return new WaitForSeconds(1);
            secondsLeft--;
            UIController.SetTimer(secondsLeft, true);
        }
        timerIsGoing = false;
    }

}
