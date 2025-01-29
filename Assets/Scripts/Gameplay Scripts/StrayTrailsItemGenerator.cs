using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrayTrailsItemGenerator : MonoBehaviour
{
    [Header("Script Connection")]
    [SerializeField] private StrayTrailsInputController inputController;
    [SerializeField] private TileMapController tileMapController;

    [Header("Items")]
    [SerializeField] private GameObject[] possibleItems;
    [SerializeField] private List<GameObject> currentItems;
    [SerializeField] private Vector3 itemDestroyPoint;
    [SerializeField] private int maxItems = 10;
    [SerializeField] private float timeBetweenInstancesMin = 1.0f;
    [SerializeField] private float timeBetweenInstancesMax = 1.0f;
    [SerializeField] private Vector2[] spawnPoints;


    private bool isPlaying = false;

    void Start()
    {
        if (possibleItems.Length == 0) { Debug.Log("There are no possible items for Stray Trails Mode collectables"); }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentItems.Count >  0 && inputController.IsPlaying())
        {
            // Move every item towards the destroy points
            foreach (GameObject item in currentItems)
            {
                // if the item was destroyed from before, remove it from the list
                if (!item) { currentItems.Remove(item); break; }

                // Validate if the item is at the destory point
                if (item.transform.position.x == itemDestroyPoint.x)
                {
                    Destroy(item);
                }
                else
                {
                    // Move Item
                    Vector2 target = new Vector2(itemDestroyPoint.x, item.transform.position.y);
                    float itemSpeed = tileMapController.GetTileMapSpeed(); // speed is based on tile map speed
                    item.transform.position = Vector2.MoveTowards(item.transform.position, target, itemSpeed * Time.deltaTime);
                }
            }
        }
        else if (!inputController.IsPlaying() && isPlaying == true)
        {
            StopSpawning();
        }
        else if (!inputController.IsPlaying() && isPlaying == false)
        {
            StartSpawning();
        }

    }

    private void StartSpawning()
    {
        isPlaying = true;
        StartCoroutine(StartSpawningCoroutine());
    }

    private IEnumerator StartSpawningCoroutine()
    {
        while (isPlaying)
        {
            // Validate there aren't too many items
            if (currentItems.Count < maxItems)
            {
                // Generate a random item at a random spawn 
                GameObject item = possibleItems[Random.Range(0, possibleItems.Length)];
                item.GetComponent<SpriteRenderer>().sortingOrder = 3;

                Vector3 spawn = spawnPoints[Random.Range(0, spawnPoints.Length)];

                // Instiate the item
                GameObject createdItem = Instantiate(item, spawn, Quaternion.identity);
                currentItems.Add(createdItem);

                // Let the input controller know there's another item
                inputController.AddItem(createdItem);
            }
            // Wait between items
            float secondsToWait = Random.Range(timeBetweenInstancesMin, timeBetweenInstancesMax);
            yield return new WaitForSeconds(secondsToWait);
        }
    }

    private void StopSpawning()
    {
        isPlaying = false;

        // Destroy all items
        foreach (GameObject item in currentItems)
        {
            Destroy(item);
        }
        currentItems.Clear();
    }

    public List<GameObject> GetCurrentItems() { return currentItems; }
}
