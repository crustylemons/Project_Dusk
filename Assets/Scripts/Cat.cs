using System.Collections;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] private Path path;
    [SerializeField] private TypingInputController typingController;

    // currentPos is created because gameObject.transform is read only
    [SerializeField] private Transform currentPos;

    [SerializeField] private Vector2[] pathPoints;
    [SerializeField] private int pathPointIndex = 0;


    public Transform GetCurrentPos() { return currentPos; }
    public void SetCurrentPos(Transform newPos) { currentPos = newPos; }

    private void Start()
    {
        path = FindFirstObjectByType<Path>();

        currentPos = transform;
        pathPoints = path.GetPathPoints();
        StartCoroutine(WaitForCorrectInput(KeyCode.Space));
    }


    private IEnumerator WaitForCorrectInput(KeyCode key)
    {
        while (pathPointIndex <= pathPoints.Length - 1)
        {
            yield return new WaitUntil(() => Input.GetKeyDown(key));
            MoveToNextPoint();
            yield return null;
        }
        Debug.Log("No more path points left");
    }

    public void MoveToNextPoint()
    {
        currentPos.position = pathPoints[pathPointIndex];
        pathPointIndex++;
    }

}
