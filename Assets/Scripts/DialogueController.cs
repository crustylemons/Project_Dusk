using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private Text dialogueText;
    [SerializeField] private Text spaceToContinue;

    [SerializeField] private bool isPrintingDialogue = false;
    [SerializeField] private bool skipToFullText = false;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isPrintingDialogue || Input.GetKeyDown(KeyCode.Mouse0) && isPrintingDialogue) { skipToFullText = true; }
    }

    // Firewall for scenarios when printing dialogue isn't permitted
    public void PrintDialogue(string[] givenDialogue)
    {
        if (!isPrintingDialogue)
        {
            dialogueBox.SetActive(true);
            dialogueText.gameObject.SetActive(true);

            // Start handling each string
            StartCoroutine(HandleDialogue(givenDialogue));
        }
        else { Debug.Log("Already printing dialogue"); }
    }

    // Print the string array of dialogue to the player, waiting for input to move on to each string
    private IEnumerator HandleDialogue(string[] dialogues)
    {
        isPrintingDialogue = true;

        // iterate through the each given dialogue
        foreach (string d in dialogues)
        {
            // Start printing dialogue
            yield return StartCoroutine(PrintStringSlowly(d));

            if (!skipToFullText)
            {
                spaceToContinue.gameObject.SetActive(true);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0));
                yield return null;
            }
            skipToFullText = false;
            spaceToContinue.gameObject.SetActive(false);
        }

        // Make dialogue box inactive and let process know it's done printing
        isPrintingDialogue = false;
        dialogueBox.gameObject.SetActive(false);
        spaceToContinue.gameObject.SetActive(false);
        dialogueText.gameObject.SetActive(false);
    }

    // For a single string, print out in a satisfying typewriter-style 
    private IEnumerator PrintStringSlowly(string givenDialogue)
    {
        string currentPrint = "";
        foreach (char c in givenDialogue)
        {
            // if the player skips to full text
            if (skipToFullText)
            {
                dialogueText.text = givenDialogue;
                spaceToContinue.gameObject.SetActive(true);
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0));
                yield return null;
                break;
            }
            dialogueText.text = currentPrint += c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public bool GetIsPrintingDialogue() { return isPrintingDialogue; }
}
