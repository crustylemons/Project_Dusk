using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    //[Header("Script Connections")]
    [SerializeField] private TypingTestStatsManager statsManager;

    // Data
    private int[] items;
    private int couch;
    private int catBed;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("saveData");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        items = new int[2] { couch, catBed };
    }

    public void UpdateCollectedItem(string ItemName)
    {
       // Find the correct object name and increment it
       switch(ItemName.ToLower())
        {
            case "couch":
                couch++; break;
            case "cat bed":
                catBed++; break;
            default:
                Debug.Log("Couldn't find an item to update");
                break;
        }

        Debug.Log($"couch count = {couch}, cat bed count = {catBed}");
    }

    public int GetCollectedItemCount(string ItemName)
    {
        switch (ItemName.ToLower())
        {
            case "couch":
                return couch;
            case "cat bed":
                return catBed;
            default: 
                return 0;
        }
    }

}
