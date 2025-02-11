using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveDataManager : MonoBehaviour
{

    // Data
    [SerializeField] private int itemsCollectedCount;
    private int couch;
    private int catBed;
    private int kirby;
    private int strayTrailsRecentScore;
    private int strayTrailsHighScore;

    [SerializeField] private bool hasHomeCutscenePlayed = false;

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
        // If it's in the game's scene, find reset items collected count
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "Gameplay")
        {
            

            itemsCollectedCount = 0;
        }
    }

    public void ResetTotalCollected () { itemsCollectedCount = 0; }

    public void UpdateCollectedItem(string ItemName)
    {
       // Find the correct object name and increment it
       switch(ItemName.ToLower().Trim())
        {
            case "couch":
                couch++; break;
            case "cat bed":
                catBed++; break;
            case "kirby":
                kirby++;
                Debug.Log("kirby was collected");
                break;
            default:
                Debug.Log("Couldn't find an item to update");
                break;
        }

        // Update the amount collected that round
        itemsCollectedCount++;
    }

    public int GetCollectedItemCount(string ItemName)
    {
        return ItemName.ToLower().Trim() switch
        {
            "couch" => couch,
            "cat bed" => catBed,
            "kirby" => kirby,
            _ => 0,
        };
    }


    public int GetTotalCollected() {  return itemsCollectedCount; }

    public void HomeCutscenePlayed() { hasHomeCutscenePlayed = true; }

    public bool HasHouseCutscenePlayed()
    {
        if (hasHomeCutscenePlayed) return true;
        else return false;
    }

    public void UpdateScore(int recentScore)
    {
        strayTrailsRecentScore = recentScore;
        if (recentScore > strayTrailsHighScore)
        {
            strayTrailsHighScore = recentScore;
        }
    }

    public int GetHighScore() { return strayTrailsHighScore; }

    public int GetRecentScore() { return strayTrailsRecentScore; }
}
