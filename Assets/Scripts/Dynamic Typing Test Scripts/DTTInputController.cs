using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class DTTInputController : MonoBehaviour
{
    [SerializeField] private string[] possibleWords;

    [SerializeField] private SceneManagement sceneManagement;
    [SerializeField] private Animator catAnimator;

    [Header("Statistics")]
    [SerializeField] private PlayerStatsManager playerStatsManager;
    [SerializeField] private float secondsTyped;
    private int correctCharactersTyped;
    private int charactersTyped;
    [SerializeField] private int seconds = 30;
    [SerializeField] private int secondsLeft = 30;

    private bool timerIsGoing;

    [Header("UI")]
    [SerializeField] private GameObject WordBox;
    [SerializeField] private Text upcomingWordDisplay;
    [SerializeField] private Text wordDisplay;
    [SerializeField] private Text typedDisplay;

    private void Start()
    {
        wordDisplay.text = "start typing to make the cat run";
        catAnimator.SetBool("IsMoving", true);
        GenerateUpcomingText();
        StartCoroutine(GetPlayerInput());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            sceneManagement.LoadScene("Home");
        }
    }

    private void GenerateUpcomingText()
    {
        // Generate a line of text that isn't too long for the text box
        string lineOfWordsDisplay = "";
        while (lineOfWordsDisplay.Length < 30)
        {
            string randomWord = possibleWords[Random.Range(0, possibleWords.Length)] + " ";
            lineOfWordsDisplay += randomWord;
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
        StartCoroutine(Timer());
        while (timerIsGoing)
        {
            foreach (char c in wordDisplay.text)
            {
                yield return null;
                KeyCode value = (KeyCode)c;
                while (true)
                {
                    yield return new WaitUntil(() => Input.anyKeyDown);
                    if (Input.inputString.ToLower() == c.ToString().ToLower())
                    {
                        typedDisplay.text += c;
                        correctCharactersTyped++;
                        break; // Exit the loop and proceed to the next character
                    }
                    else
                    {
                        //Debug.Log($"Incorrect letter: Expected {c}");
                    }

                    charactersTyped++;
                }
            }

            // Reset for the next word
            typedDisplay.text = "";
            wordDisplay.text = "";
            GenerateUpcomingText();
        }

        // Ending test actions
        WordBox.SetActive(false);
        playerStatsManager.SetWordsPerMinute(correctCharactersTyped, seconds/60f);
        playerStatsManager.SetAccuracy(correctCharactersTyped, charactersTyped);
    }

    private IEnumerator Timer()
    {
        timerIsGoing = true;
        for (int i = seconds; i > 0; i--)
        {
            yield return new WaitForSeconds(1);
            secondsLeft--;
        }
        timerIsGoing = false;
    }

}
