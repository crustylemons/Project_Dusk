using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class StrayTrailsObstacleGenerator : MonoBehaviour
{
    [Header("Script Connection")]
    [SerializeField] private StrayTrailsInputController inputController;
    [SerializeField] private TileMapController tileMapController;

    [Header("Obstacles")]
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private List<GameObject> currentObstacles;

    [Header("Obstacle Settings")]
    [SerializeField] private Vector3 obstacleDestoryPoint;
    [SerializeField] private int maxObstacles = 10;
    [SerializeField] private float timeBetweenInstancesMin = 1.0f;
    [SerializeField] private float timeBetweenInstancesMax = 1.0f;
    [SerializeField] private Vector3[] spawnPoints;


    private bool isPlaying = false;

    void Start()
    {
        // Validate there are possible obstacles
        if (!obstaclePrefab) { Debug.Log("There is possible object for Stray Trails Mode obstacles"); }        
    }

    void Update()
    {
        if (currentObstacles.Count > 0 && inputController.IsPlaying())
        {
            // Move every obstacle towards the destory points
            foreach (GameObject obs in currentObstacles)
            {
                // if the obstacle was destroyed from before, remove it from the List
                if (!obs) { currentObstacles.Remove(obs); break; }

                // Validate if the obstacle is at the destroy point
                if (obs.transform.position.x == obstacleDestoryPoint.x)
                {
                    Destroy(obs);
                }
                else
                {
                    // Move obstacle
                    Vector2 target = new Vector2(obstacleDestoryPoint.x, obs.transform.position.y);
                    float obstacleSpeed = tileMapController.GetTileMapSpeed(); // speed is based on tile map speed
                    obs.transform.position = Vector2.MoveTowards(obs.transform.position, target, obstacleSpeed * Time.deltaTime);
                }
            }
        }
        else if (!inputController.IsPlaying() && isPlaying == true)
        {
            StopSpawning();
        }
        else if (inputController.IsPlaying() && isPlaying == false)
        {
            StartSpawning();
        }
    }

    public void StartSpawning()
    {
        isPlaying = true;
        StartCoroutine(StartSpawningCoroutine());
    }

    private IEnumerator StartSpawningCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(2, 3)); // delay
        while(isPlaying)
        {
            // Validate there aren't too many obstacles
            if (currentObstacles.Count < maxObstacles)
            {
                // Generate a random obstacle at a random spawn 
                GameObject obstacle = obstaclePrefab;
                obstacle.GetComponent<SpriteRenderer>().sortingOrder = 3;

                Vector3 spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];

                // Instiate the obstacle
                GameObject createdObstacle = Instantiate(obstacle, spawn, Quaternion.identity);
                currentObstacles.Add(createdObstacle);
            }
            // Wait between obstacles
            float secondsToWait = Random.Range(timeBetweenInstancesMin, timeBetweenInstancesMax);
            yield return new WaitForSeconds(secondsToWait);
        }
    }

    public List<GameObject> GetCurrentObstacles() { return currentObstacles; }

    public void StopSpawning()
    {
        isPlaying = false;


        // Destroy all obstacles
        foreach (GameObject obs in currentObstacles)
        {
            Destroy(obs);
        }
        currentObstacles.Clear();
    }
}
