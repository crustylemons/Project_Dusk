using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private Text dialogueText;

    [SerializeField] private bool isPrintingDialogue = false;
    [SerializeField] private bool skipToFullText = false;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isPrintingDialogue) { skipToFullText = true; }
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

        foreach (string d in dialogues)
        {
            // Start printing dialogue
            yield return StartCoroutine(PrintStringSlowly(d));

            
            if (!skipToFullText)
            {
                Debug.Log("Waiting for input...");
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
            skipToFullText = false;
        }

        // Make dialogue box inactive and let process know it's done printing
        isPrintingDialogue = false;
        dialogueBox.SetActive(false);
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
                Debug.Log("Waiting for input...");
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
                break;
            }
            dialogueText.text = currentPrint += c;
            yield return new WaitForSeconds(0.08f);
        }
    }

    public bool GetIsPrintingDialogue() { return isPrintingDialogue; }
}
