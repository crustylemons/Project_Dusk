using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveDataManager : MonoBehaviour
{
    //[Header("Script Connections")]
    [SerializeField] private TypingTestStatsManager statsManager;

    // Data
   [SerializeField] private int itemsCollectedCount;
    private int couch;
    private int catBed;
    private int plant;

    private bool hasHomeCutscenePlayed = false;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("saveData");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

    }

    public void FindStatsManager()
    {
        // If it's in the game's scene, find statsManager
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "Gameplay")
        {
            statsManager = FindAnyObjectByType<TypingTestStatsManager>();
            

            itemsCollectedCount = 0;
        }
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
            case "plant":
                plant++; break;
            default:
                Debug.Log("Couldn't find an item to update");
                break;
        }

        // Update the amount collected that round
        itemsCollectedCount++;
    }

    public int GetCollectedItemCount(string ItemName)
    {
        switch (ItemName.ToLower())
        {
            case "couch":
                return couch;
            case "cat bed":
                return catBed;
            case "plant":
                return plant;
            default: 
                return 0;
        }
    }


    public int GetTotalCollected() {  return itemsCollectedCount; }

    public void HomeCutscenePlayed() { hasHomeCutscenePlayed = true; }

    public bool HasHouseCutscenePlayed()
    {
        if (hasHomeCutscenePlayed) return true;
        else return false;
    }
}
