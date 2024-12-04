using System.Collections;
using System.Collections.Generic;
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

    public IEnumerator WaitForInput()
    {
        
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        cutscenes[0].GetComponent<PlayableDirector>().Play();
    }

    public void StartDialogue()
    {
        dialogueController.PrintDialogue(cutscenes[0].GetDialogue());
        catAnimator.SetBool("IsSitting", true);
    }

    public void StartGameplay()
    {
        catAnimator.SetBool("IsSitting", false);
        cutscenes[0].GetComponent<PlayableDirector>().Stop();
    }
}

