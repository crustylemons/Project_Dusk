using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Cutscene : MonoBehaviour
{
    [SerializeField] private string[] dialogue;
    [SerializeField] private string cutsceneName;
    [SerializeField] private bool hasPlayed = false;
    [SerializeField] private bool playOnAwake = false;

    private void Start()
    {

        if (playOnAwake && !hasPlayed)
        {
            gameObject.GetComponent<PlayableDirector>().Play();
        }
        
    }

    public string[] GetDialogue() { return dialogue; }

    public string GetName() { return cutsceneName; }

    public void SetHasPlayed() { hasPlayed = true; }

    public bool GetHasPlayed() { return hasPlayed; }
}
