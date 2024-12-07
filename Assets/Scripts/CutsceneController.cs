using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CutsceneController : MonoBehaviour
{
    [SerializeField] private Cat cat;
    [SerializeField] private Animator catAnimator;

    [SerializeField] private DialogueController dialogueController;

    [SerializeField] private Cutscene[] cutscenes;

    private void Start()
    {
        cat = FindFirstObjectByType<Cat>();
        catAnimator = cat.GetAnimator();
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
        catAnimator.SetBool("IsSitting", true);
    }

    public void StartGameplay()
    {
        catAnimator.SetBool("IsSitting", false);
        cutscenes[0].gameObject.GetComponent<PlayableDirector>().Stop();
    }
}

