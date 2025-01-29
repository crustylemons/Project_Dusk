using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private SaveDataManager saveData;

    [Header("Item Data")]
    [SerializeField] private string furnitureName;
    [SerializeField] private string rarity;

    void Start()
    {
        // Ensure there's always a furniture name
        if (furnitureName == null)
        {
            furnitureName = gameObject.name;
        }
    }

    void Update()
    {
        
    }

    public void Collect()
    {
        saveData.UpdateCollectedItem(furnitureName);
    }
}
