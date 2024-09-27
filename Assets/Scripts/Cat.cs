using System.Collections;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] private Path path;
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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MoveToNextPoint();
        }
    }


    public void MoveToNextPoint()
    {
        currentPos.position = pathPoints[pathPointIndex].position;
        pathPointIndex++;
    }
}
