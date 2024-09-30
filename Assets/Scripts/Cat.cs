using System.Collections;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] private Path path;
    [SerializeField] private MyCamera cam;
    [SerializeField] private TypingInputController typingController;

    // currentPos is created because gameObject.transform is read only
    [SerializeField] private Transform currentPos;

    [SerializeField] private Transform[] pathPoints;
    [SerializeField] private int pathPointIndex = 0;


    public Transform GetCurrentPos() { return currentPos; }
    public void SetCurrentPos(Transform newPos) { currentPos = newPos; }

    private void Start()
    {
        path = FindFirstObjectByType<Path>();

        currentPos = transform;
        pathPoints = path.GetPathPoints();

        GetPastPoint();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MoveToNextPoint();
        }
    }
    
    private void GetPastPoint()
    {
        typingController.InitiatePlayerInput();
        MoveToNextPoint();
    }

    public void MoveToNextPoint()
    {
        currentPos.position = pathPoints[pathPointIndex].position;
        pathPointIndex++;

        cam.UpdateCameraPos(currentPos.transform);

        GetPastPoint();
    }
}
