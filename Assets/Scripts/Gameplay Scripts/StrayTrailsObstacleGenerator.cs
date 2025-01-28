using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class StrayTrailsObstacleGenerator : MonoBehaviour
{
    [SerializeField] private StrayTrailsInputController inputController;
    [SerializeField] private TileMapController tileMapController;

    [SerializeField] private GameObject[] possibleObstacles;
    [SerializeField] private List<GameObject> currentObstacles;

    [Header("Spawn Points")]
    [SerializeField] private Vector3[] spawnPoints;


    [Header("Obstacle Settings")]
    [SerializeField] private Vector3 obstacleDestoryPoint;
    [SerializeField] private int maxObstacles = 10;
    [SerializeField] private float timeBetweenInstancesMin = 1.0f;
    [SerializeField] private float timeBetweenInstancesMax = 1.0f;

    void Start()
    {
        // Validate there are possible obstacles
        if (possibleObstacles.Length == 0) { Debug.Log("There are no possible objects"); }        
    }

    void Update()
    {
        if (currentObstacles.Count > 0)
        {
            // Move every obstacle towards the destory points
            foreach (GameObject obs in currentObstacles)
            {
                // if the obstacle was destroyed from before, remove it from the List
                if (!obs) { currentObstacles.Remove(obs); break; }

                // Validate if the obstacle is at the destroy point
                if (obs.transform.position.x == obstacleDestoryPoint.x)
                {
                    Debug.Log($"{obs.name} should be destroyed");
                    Destroy(obs);
                }
                else
                {
                    // Move obstacle
                    Vector2 target = new Vector2(obstacleDestoryPoint.x, obs.transform.position.y);
                    float obstacleSpeed = tileMapController.GetTileMapSpeed();
                    obs.transform.position = Vector2.MoveTowards(obs.transform.position, target, obstacleSpeed * Time.deltaTime);
                }
            }
        }
    }

    // Gets Initiated from external sources
    public void StartSpawning()
    {
        StartCoroutine(StartSpawningCoroutine());
    }

    private IEnumerator StartSpawningCoroutine()
    {
        while(true)
        {
            // Validate there aren't too many obstacles
            if (currentObstacles.Count < maxObstacles)
            {
                // Generate a random obstacle at a random spawn 
                GameObject obstacle = possibleObstacles[Random.Range(0, possibleObstacles.Length)];
                obstacle.GetComponent<SpriteRenderer>().sortingOrder = 5;

                Vector3 spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];

                // Instiate the obstacle
                GameObject createObstacle = Instantiate(obstacle, spawn, Quaternion.identity);
                currentObstacles.Add(createObstacle);

                Debug.Log("Obstacle Created: " + createObstacle.name);

                
            }
            // Wait between obstacles
            float secondsToWait = Random.Range(timeBetweenInstancesMin, timeBetweenInstancesMax);
            yield return new WaitForSeconds(secondsToWait);
        }
    }

    public List<GameObject> GetCurrentObstacles() { return currentObstacles; }
}
