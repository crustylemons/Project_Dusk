using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TypingInputController : MonoBehaviour
{
    [Header("Path Data")]
    [SerializeField] private Path path;
    [SerializeField] private Vector2[] pathPoints;
    [SerializeField] private int pathPointIndex = 0;
    [SerializeField] private Vector2 targetPos = Vector2.zero;

    [Header("Word Data")]
    [SerializeField] private string[] words;
    [SerializeField] private Text wordDisplay;
    [SerializeField] private Text typedDisplay;

    [Header("Cutscenes")]
    [SerializeField] private CutsceneController cutsceneController;

    [Header("Audio")]
    [SerializeField] private AudioSource jumpSFXSource;


    private void Start()
    {
        // Get path points
        path = FindFirstObjectByType<Path>();
        pathPoints = path.GetPathPoints();
    }

    // Start the path progression gameplay
    public void StartPathCoroutine()
    {
        StartCoroutine(ProgressPathWaiter(words));   
    }

    public Vector2 GetTargetPos() { return targetPos; }

    private IEnumerator ProgressPathWaiter(string[] words)
    {
        // Iterate through every word
        for (int i = 0; pathPointIndex <= words.Length - 1; i++)
        {
            // UI management
            wordDisplay.text = words[i];
            string currentlyType = "";

            // Iterate through every letter in the current word
            char[] word = words[i].ToCharArray();
            foreach (char c in word)
            {
                KeyCode value = (KeyCode)c;
                yield return new WaitUntil(() => Input.GetKeyDown(value));
                typedDisplay.text = currentlyType += c;
                yield return null;
            }
            typedDisplay.text = "";

            // Continue to next word
            MoveToNextPoint();
            yield return null;
        }

        Debug.Log("No more path points left");
        targetPos = new Vector2(-1, 27);
        cutsceneController.StartCutscene(cutsceneController.GetNextUnplayedCutscene().GetName());
    }


    private void MoveToNextPoint()
    {
        jumpSFXSource.Play();
        targetPos = pathPoints[pathPointIndex];
        pathPointIndex++;
    }
}