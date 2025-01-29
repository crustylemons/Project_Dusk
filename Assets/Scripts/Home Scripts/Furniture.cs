using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{
    [SerializeField] private string furnitureName;
    [SerializeField] private SaveDataManager saveData;

    private int collectedCount;

    private void Start()
    {
        if (furnitureName == null)
        {
            furnitureName = gameObject.name;
        }

        saveData = FindFirstObjectByType<SaveDataManager>();
        if (saveData)
        {
            collectedCount = saveData.GetCollectedItemCount(furnitureName);
        }
    }

    public int GetCollectedCount() {  return collectedCount; }
}
