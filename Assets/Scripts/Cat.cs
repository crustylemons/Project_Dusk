using System.Collections;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] private Path path;
    [SerializeField] private MyCamera cam;
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
        MoveToNextPoint();
    }


    IEnumerator WaitForCorrectInput(KeyCode key)
    {
        yield return new WaitUntil(() => Input.GetKeyDown(key));
        MoveToNextPoint();
    }

    public void MoveToNextPoint()
    {
        currentPos.position = pathPoints[pathPointIndex];
        pathPointIndex++;

        cam.UpdateCameraPos(currentPos.transform);

        StartCoroutine(WaitForCorrectInput(KeyCode.Space));
    }
}
