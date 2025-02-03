using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutsceneController : MonoBehaviour
{
    [SerializeField] private Animator catAnimator;

    [SerializeField] private DialogueController dialogueController;
    [SerializeField] private SaveDataManager saveData;

    private HouseCat cat;

    [SerializeField] private Cutscene[] cutscenes;


    private void Start()
    {
        saveData = GameObject.FindWithTag("saveData").GetComponent<SaveDataManager>();

        // If the current is is Home
        if (SceneManager.GetActiveScene().name == "Home")
        {
            cat = FindAnyObjectByType<HouseCat>();

            // If the player has already been in the home before and played the beginning cutscene
            if (saveData && saveData.HasHouseCutscenePlayed())
            {
                
                foreach (Cutscene c in cutscenes)
                {
                    if (c.GetName() == "Enter House")
                    {
                        // Set "Enter House" to has played before
                        c.SetHasPlayed();

                        // Start position control -- what would otherwise be started at the end of the Timeline
                        if (cat) { cat.StartPositionControl(); }
                    }
                }
            }
        }
    }

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
        // Check if it's attempting to play the Enter House cutscene again
        if (name == "Enter House" && saveData.HasHouseCutscenePlayed()) { Debug.Log("Intro House has already played"); return; }
        
        // Iterate through given cutscenes
        foreach (Cutscene c in cutscenes)
        {
            if (c.GetHasPlayed() == false && c.GetName() == name)
            {
                c.gameObject.GetComponent<PlayableDirector>().Play();
                break;
            }
        }
    }

}

