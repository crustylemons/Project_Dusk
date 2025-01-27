using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneController : MonoBehaviour
{
    [SerializeField] private Animator catAnimator;

    [SerializeField] private DialogueController dialogueController;

    [SerializeField] private Cutscene[] cutscenes;
    

    private IEnumerator WaitForDialogue()
    {
        yield return new WaitUntil(() => dialogueController.GetIsPrintingDialogue() == false);
        cutscenes[0].gameObject.GetComponent<PlayableDirector>().Play();
    }

    public void StartDialogue()
    {
        // Initiates dialogue
        dialogueController.PrintDialogue(cutscenes[0].GetDialogue());
        StartCoroutine(WaitForDialogue());
    }

    public void EndCutscene(string cutsceneName)
    {
        // Stop cutscene
        cutscenes[0].gameObject.GetComponent<PlayableDirector>().Stop();

        // Iterature through cutscenes to set the correct one's bool "hasPlayed" to true
        foreach (Cutscene c in cutscenes)
        {
            if (c.GetName() == cutsceneName)
            {
                c.SetHasPlayed();
            }
        }
    }

    public void StartCutscene(string name)
    {
        // Iterate through given cutscenes
        foreach (Cutscene c in cutscenes)
        {
            if (c.GetHasPlayed() == false && c.GetName() == name || c.GetCanPlayMultiple() && c.GetName() == name)
            {
                c.gameObject.GetComponent<PlayableDirector>().Play();
                break;
            }
        }
    }

    public Cutscene GetNextUnplayedCutscene()
    {
        foreach (Cutscene c in cutscenes)
        {
            if (c.GetHasPlayed() == false || c.GetCanPlayMultiple())
            {
                return c;
            }
        }
        return null;
    }
}

