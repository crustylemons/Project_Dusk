using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    private SaveDataManager saveDataManager;

    private void Start()
    {
        // Let saveDataManager know it's a new scene
        saveDataManager = FindFirstObjectByType<SaveDataManager>();
        if (saveDataManager) { saveDataManager.FindStatsManager(); }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
