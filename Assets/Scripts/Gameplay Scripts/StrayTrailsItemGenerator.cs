using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrayTrailsItemGenerator : MonoBehaviour
{
    [SerializeField] private Item[] possibleItems;
    [SerializeField] private Item[] currentItems;
    [SerializeField] private Vector2[] spawnPoints;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Item[] GetCurrentItems() { return possibleItems; }
}
