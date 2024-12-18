using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DynamicTypingInputController : MonoBehaviour
{
    [SerializeField] private string[] possibleWords;

    [SerializeField] private Animator catAnimator;

    [Header("UI")]
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
        while (true)
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
                        break; // Exit the loop and proceed to the next character
                    }
                    else
                    {
                        Debug.Log($"Incorrect letter: Expected {c}");
                    }
                }
            }

            // Reset for the next word
            typedDisplay.text = "";
            wordDisplay.text = "";
            GenerateUpcomingText();
        }
    }
}
