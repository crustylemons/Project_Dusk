using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private SaveDataManager saveData;
    [SerializeField] private GameUIController UIController;
    [SerializeField] private TileMapController tileMapController;

    private int score;
    private bool isPlaying = false;

    private void Start()
    {
        saveData = GameObject.FindWithTag("saveData").GetComponent<SaveDataManager>();
    }

    public void InitializeScoring()
    {
        isPlaying = true;
        saveData.ResetTotalCollected();
        StartCoroutine(Scoring());
    }

    private IEnumerator Scoring()
    {
        while (isPlaying)
        {
            score+=2;
            if (score % 10 == 0)
            {
                UIController.UpdateScore(score); // update UI every 10
            }
            yield return new WaitForSeconds(0.5f);

            // Update speed
            if (score >= 100 && score < 200) { tileMapController.SetSpeed(8.5f); }
            else if (score >= 200 && score < 300) { tileMapController.SetSpeed(9.0f); }
            else if (score >= 300 && score < 400) { tileMapController.SetSpeed(9.5f);  }
            else if (score >= 400 && score < 500) { tileMapController.SetSpeed(10.0f);  }
            else if (score >= 500 && score < 600) { tileMapController.SetSpeed(11.0f); }
            else if (score >= 600 && score < 700) { tileMapController.SetSpeed(11.5f); }
            else if (score >= 700 && score < 800) { tileMapController.SetSpeed(12.0f); }
            else if (score >= 800) { tileMapController.SetSpeed(13.0f); }

        }
    }

    public void StopScoring()
    {
        isPlaying = false;
        saveData.UpdateScore(score);
        score = 0;
    }
}
