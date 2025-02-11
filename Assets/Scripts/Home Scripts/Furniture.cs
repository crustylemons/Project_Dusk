using System.Collections;
using UnityEngine;

public class Furniture : MonoBehaviour
{
    [SerializeField] private HomeUIController UIController;
    private SaveDataManager saveData;

    [Header("Data")]
    [SerializeField] private string furnitureName;
    [SerializeField] private int collectedCount;
    [SerializeField] private int needToCollect = 5;

    [Header("Audio")]
    [SerializeField] private AudioClip onHoverSoundFX;
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        // Ensure the object has a name
        furnitureName ??= gameObject.name;

        // Get saved data
        saveData = GameObject.FindWithTag("saveData").GetComponent<SaveDataManager>();
        if (saveData)
        {
            collectedCount = saveData.GetCollectedItemCount(furnitureName);
            if (collectedCount >= needToCollect)
            {

                // Visual Changes
                gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }
        }
    }

    public int GetCollectedCount() {  return collectedCount; }

    private void OnMouseEnter()
    {
        audioSource.PlayOneShot(onHoverSoundFX);
    }

    private void OnMouseOver()
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        UIController.OnMouseHoverFurniture(screenPos, collectedCount, needToCollect);
    }

    private void OnMouseExit()
    {
        UIController.OnMouseExitFurniture();   
    }
}
