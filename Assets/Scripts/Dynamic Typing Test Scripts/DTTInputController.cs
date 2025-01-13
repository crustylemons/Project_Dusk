using JetBrains.Annotations;
using System.Collections;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class DTTInputController : MonoBehaviour
{
    [SerializeField] private string[] possibleNormalWords;
    [SerializeField] private string[] possibleLeftHandWords;
    [SerializeField] private string[] possibleRightHandWords;

    [Header("Needed Connections")]
    [SerializeField] private DTTUIController UIController;
    [SerializeField] private SceneManagement sceneManagement;
    [SerializeField] private TileMapController tileMapController;
    [SerializeField] private Animator catAnimator;

    [Header("Statistics")]
    [SerializeField] private PlayerStatsManager playerStatsManager;
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
        playerStatsManager = FindFirstObjectByType<PlayerStatsManager>();

        // Animation
        catAnimator.SetBool("IsMoving", false);
        tileMapController.SetCatIsRunning(false);
    }

    private void Update()
    {
        // Go back home
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            sceneManagement.LoadScene("Home");
        }
    }

    public void StartTypingTest()
    {
        // UI
        wordDisplay.text = "start typing to make the cat run";

        // Animation
        catAnimator.SetBool("IsMoving", true);
        tileMapController.SetCatIsRunning(true);

        // Function calling
        GenerateUpcomingText();
        StartCoroutine(GetPlayerInput());
    }

    private void GenerateUpcomingText()
    {
        // Find the right pool of words
        string[] wordPool;
        switch(UIController.GetGameMode())
        {
            case "left hand":
                wordPool = possibleLeftHandWords;
                Debug.Log("left hand chosen");
                break;
            case "right hand":
                wordPool = possibleRightHandWords;
                Debug.Log("right hand chosen");
                break;
            default:
                wordPool = possibleNormalWords;
                Debug.Log("normal chosen");
                break;
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
            GenerateUpcomingText();
        }
        else
        {
            upcomingWordDisplay.text = lineOfWordsDisplay;
        }
    }

    private IEnumerator GetPlayerInput()
    {
        StartTestActions();

        int countdown = 3;
        while (countdown > 0)
        {
            StartCoroutine(Timer(countdown));
            yield return new WaitForSeconds(1);
            countdown--;
        }

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
                }
            }

            // Reset for the next word
            typedDisplay.text = "";
            wordDisplay.text = "";
            GenerateUpcomingText();
        }

        // End Input intake
        EndTestActions();
    }

    private void StartTestActions()
    {
        // Clear previous data
        correctCharactersTyped = 0;
        charactersTyped = 0;

    }

    private void EndTestActions()
    {
        // Give Data
        playerStatsManager.GetDataEndTest(correctCharactersTyped, charactersTyped, seconds / 60f);
        
        // UI
        UIController.EndTest();

        // Animation
        tileMapController.SetCatIsRunning(false);
        catAnimator.SetBool("isMoving", false);
    }

    private IEnumerator Timer(int seconds)
    {
        timerIsGoing = true;
        int secondsLeft = seconds;
        UIController.SetTimer(secondsLeft);

        for (int i = seconds; i > 0; i--)
        {
            yield return new WaitForSeconds(1);
            secondsLeft--;
            UIController.SetTimer(secondsLeft);
        }
        timerIsGoing = false;
    }

}
