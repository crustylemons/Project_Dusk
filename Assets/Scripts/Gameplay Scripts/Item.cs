using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private SaveDataManager saveData;

    [Header("Item Data")]
    [SerializeField] private string furnitureName;
    [SerializeField] private int commonMultiplier;

    void Start()
    {
        // Ensure there's always a furniture name
        if (furnitureName == null)
        {
            furnitureName = gameObject.name;
        }

        // Connections
        saveData = FindFirstObjectByType<SaveDataManager>();
    }

    public void Collect()
    {
        saveData.UpdateCollectedItem(furnitureName);

        Destroy(gameObject);
    }

    public int GetCommonMultiplier() { return commonMultiplier; }
}
