using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class CutsceneController : MonoBehaviour
{
    public IEnumerator WaitForInput()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
    }
    public void StartWaitingForInput()
    {
        StartCoroutine(WaitForInput());
    }
}

