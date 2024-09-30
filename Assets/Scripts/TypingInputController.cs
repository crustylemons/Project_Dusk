using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypingInputController : MonoBehaviour
{
    [SerializeField] private Path path;

    [Tooltip("")]
    [SerializeField] private char[] letters;

    private void Start()
    {
        path = FindFirstObjectByType<Path>();
        letters = new char[path.GetPathPoints().Length];
    }

    public void InitiatePlayerInput()
    {
           
    }

    IEnumerator WaitForCorrectInput(KeyCode key)
    {
        yield return new WaitUntil(() => Input.GetKeyDown(key));
    }
}
