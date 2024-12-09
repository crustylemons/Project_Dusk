using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    [SerializeField] private string[] dialogue;
    [SerializeField] private string cutsceneName;
    [SerializeField] private bool hasPlayed = false;

    public string[] GetDialogue() { return dialogue; }

    public string GetName() { return cutsceneName; }

    public void SetHasPlayed() { hasPlayed = true; }

    public bool GetHasPlayed() { return hasPlayed; }
}
