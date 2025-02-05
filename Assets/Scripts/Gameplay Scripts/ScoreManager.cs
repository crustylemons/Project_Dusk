using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private SaveDataManager saveData;
    [SerializeField] private GameUIController UIController;

    private int score;
    private bool isPlaying = false;

    private void Start()
    {
        saveData = GameObject.FindWithTag("saveData").GetComponent<SaveDataManager>();
    }

    public void InitializeScoring()
    {
        isPlaying = true;
        StartCoroutine(Scoring());
    }

    private IEnumerator Scoring()
    {

        while (isPlaying)
        {
            score++;
            UIController.UpdateScore(score);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void StopScoring()
    {
        isPlaying = false;
        saveData.UpdateScore(score);
    }
}
