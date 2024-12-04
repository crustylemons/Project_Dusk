using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    [SerializeField] private string[] dialogue;

    public string[] GetDialogue() { return dialogue; }
}
