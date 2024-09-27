using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private GameObject[] childObjects;
    [SerializeField] private Transform[] pathPoints;

    private void Awake()
    {
        // Gets all tranforms of children + parent
        Transform[] pointsWithParent = gameObject.GetComponentsInChildren<Transform>();
        pathPoints = new Transform[pointsWithParent.Length - 1];

        // Sets pathPoints without the parent's transform
        int i = 0;
        foreach (Transform t in pointsWithParent)
        {
            if (t != this.gameObject.transform) 
            {
                pathPoints[i] = t; 
                i++;
            }
        }
        childObjects = new GameObject[pathPoints.Length];

        // Gets all child objects from the Transform array
        int value = 0;
        foreach (Transform t in pathPoints)
        {
            value++;
            childObjects[value - 1] = t.gameObject;
        }

        // Makes points invisible while playing
        foreach(GameObject point in childObjects)
        {
            point.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public Transform[] GetPathPoints()
    {
        return pathPoints;
    }
}
