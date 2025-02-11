
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    private SaveDataManager saveData;

    private void Start()
    {
        // Let saveDataManager know it's a new scene
        saveData = FindFirstObjectByType<SaveDataManager>();
        if (saveData) { saveData.FindStatsManager(); }
    }

    public void LoadScene(string sceneName)
    {
        // If the player presses play on the home screen
        if (sceneName == "Opening")
        {
            if (!saveData)
            {
                SceneManager.LoadScene(sceneName);
            }
            else
            {
               SceneManager.LoadScene("Home"); 
            }
            return;
        }
        
        // Other scene loads
        SceneManager.LoadScene(sceneName);
    }


    public void ExitGame()
    {
        Application.Quit();
    }
}
