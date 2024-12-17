using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicTypingInputController : MonoBehaviour
{
    [SerializeField] List<string> possibleWords;


    [Header("UI")]
    [SerializeField] private GameObject wordBox;
    [SerializeField] private GameObject wordDisplay;
    [SerializeField] private GameObject typedDisplay;

    private IEnumerator WaitForInput()
    {
        return null;
    }
}
