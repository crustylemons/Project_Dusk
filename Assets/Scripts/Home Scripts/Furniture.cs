using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furniture : MonoBehaviour
{
    [SerializeField] private string furnitureName;
    [SerializeField] private SaveDataManager saveData;

    [SerializeField] private int collectedCount;
    [SerializeField] private int needToCollect = 5;

    private void Start()
    {
        // Ensure the object has a name
        if (furnitureName == null)
        {
            furnitureName = gameObject.name;
        }

        // Get saved data
        saveData = FindFirstObjectByType<SaveDataManager>();
        if (saveData)
        {
            collectedCount = saveData.GetCollectedItemCount(furnitureName);
            if (collectedCount >= needToCollect)
            {
                SpriteRenderer r = gameObject.GetComponent<SpriteRenderer>();
                Color color = new Color(r.color.r, r.color.g, r.color.b, 1f);
                r.color = color;
            }
        }
    }

    public int GetCollectedCount() {  return collectedCount; }
}
