using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private Vector2[] pathPoints;
    private SpriteRenderer[] childRenderers;

    private void Awake()
    {
        // Gets all child objects & intializes pathPoints' length
        childRenderers = GetComponentsInChildren<SpriteRenderer>();
        pathPoints = new Vector2[childRenderers.Length];

        // Populate pathPoints & set renders to invisible
        for (int i = 0; i < childRenderers.Length; i++)
        {
            pathPoints[i] = childRenderers[i].transform.position;
            childRenderers[i].enabled = false;
        }
    }

    public Vector2[] GetPathPoints()
    {
        return pathPoints;
    }
}
