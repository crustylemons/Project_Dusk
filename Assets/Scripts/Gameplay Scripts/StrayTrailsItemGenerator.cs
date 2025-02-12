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
    [SerializeField] private List<GameObject> possibleItemsWithRarity; // duplicates items based on how common they are

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
        else
        {
            foreach (GameObject item in possibleItems)
            {
                int itemCommonMultiplier = item.GetComponent<Item>().GetCommonMultiplier();
                if (itemCommonMultiplier < 1)
                {
                    itemCommonMultiplier = 1;
                }
                for(int i = 1; i <= itemCommonMultiplier; i++)
                {
                    possibleItemsWithRarity.Add(item);
                }
            }
        }
        
    }

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
                if (Mathf.Abs(item.transform.position.x - itemDestroyPoint.x) < 0.1f)
                {
                    Destroy(item);
                }
                else
                {
                    // Move Item
                    Vector2 target = new Vector2(itemDestroyPoint.x, item.transform.position.y);

                    float itemSpeed = tileMapController.GetTileMapSpeed(); // speed is based on tilemap
                    item.transform.position = Vector2.MoveTowards(item.transform.position, target, itemSpeed * Time.deltaTime);

                    // Round the position to the nearest multiple of 1/128 to match the PPU
                    float ppu = 128f;  // Pixels Per Unit
                    item.transform.position = new Vector2(
                        Mathf.Round(item.transform.position.x * ppu) / ppu,
                        Mathf.Round(item.transform.position.y * ppu) / ppu
                    );
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

    private void StartSpawning()
    {
        isPlaying = true;
        StartCoroutine(StartSpawningCoroutine());
    }

    private IEnumerator StartSpawningCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(2,3)); // delay

        while (isPlaying)
        {
            // Validate there aren't too many items
            if (currentItems.Count < maxItems)
            {
                // Generate a random item at a random spawn 
                GameObject item = possibleItemsWithRarity[Random.Range(0, possibleItemsWithRarity.Count)];
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
