using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DTTUIController : MonoBehaviour
{
    [Header("While Typing UI")]
    [SerializeField] private GameObject wordBox;
    [SerializeField] private List<GameObject> typingText;

    [Header("End Game UI")]
    [SerializeField] private GameObject endingBox;
    [SerializeField] private GameObject WPMBox;
    [SerializeField] private GameObject accuracyBox;
    [SerializeField] private GameObject leaderboard;
    [SerializeField] private GameObject playAgainBtn;
    [SerializeField] private GameObject continueBtn;


    public void SetWPM(int wpm)
    {
        Text text = WPMBox.GetComponentInChildren<Text>();
        text.text = $"WPM = {wpm}";
    }

    public void SetAccuracy(int accuracy)
    {
        Text text = accuracyBox.GetComponentInChildren<Text>();
        text.text = $"Accuracy = {accuracy}";
    }
}
