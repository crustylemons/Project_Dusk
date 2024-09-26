using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Events;
using System.Collections;

public class TimelineInputWaiter : MonoBehaviour
{
    public PlayableDirector director; // Reference to the Cutscene Controller
    public UnityEvent onInputReceived; // Event to resume the Timeline

    void Start()
    {
        // Add listener for when the signal is emitted
        onInputReceived.AddListener(ResumeTimeline);
    }

    // Method to be called by the Signal Receiver
    public void WaitForPlayerInput()
    {
        // Pause the Timeline
        director.Pause();
        Debug.Log("Timeline paused. Waiting for input...");

        // Start coroutine to wait for player input
        StartCoroutine(WaitForInput());
    }

    IEnumerator WaitForInput()
    {
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        // Trigger event to resume the timeline
        onInputReceived.Invoke();
    }

    // Method to resume the Timeline
    void ResumeTimeline()
    {
        Debug.Log("Input received. Resuming Timeline...");
        director.Play();
    }
}

