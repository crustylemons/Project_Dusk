using System.Collections;
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

    private void Start()
    {
        // Get path points
        path = FindFirstObjectByType<Path>();
        pathPoints = path.GetPathPoints();

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
    }


    private void MoveToNextPoint()
    {
        targetPos = pathPoints[pathPointIndex];
        pathPointIndex++;
    }
}