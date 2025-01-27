using System.Collections;
using UnityEngine;

public class StrayTrailsObstacleGenerator : MonoBehaviour
{

    [SerializeField] private GameObject[] possibleObstacles;
    [SerializeField] private GameObject[] currentObstacles;

    [Header("Spawn Points")]
    [SerializeField] private Vector2 spawnOne;
    [SerializeField] private Vector2 spawnTwo;


    void Start()
    {
        // Validate there are possible obstacles
        if (possibleObstacles.Length == 0) { Debug.Log("There are no possible objects"); }        
    }

    void Update()
    {
        
    }

    public GameObject[] GetCurrentObstacles() { return currentObstacles; }
}
