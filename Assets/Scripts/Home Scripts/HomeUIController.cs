using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HomeUIController : MonoBehaviour
{
    [SerializeField] private GameObject esc;
    [SerializeField] private GameObject furnitureHover;

    void Update()
    {
        if (!esc.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            esc.SetActive(true);
        }
    }

    public void DisableEsc() { esc.SetActive(false); }

    public void OnMouseHoverFurniture(Vector2 mousePosition, int amountCollected, int amountNeeded)
    {
        // Set Text
        furnitureHover.GetComponentInChildren<Text>().text = $"Collected {amountCollected}/{amountNeeded}";

        furnitureHover.SetActive(true);
        RectTransform furnitureHoverTrans = furnitureHover.GetComponent<RectTransform>();

        // Mouse position with offset of the UI element's size
        Vector2 hoverUIPos = new Vector2(Input.mousePosition.x - 100, Input.mousePosition.y - 75);
        furnitureHover.transform.position = hoverUIPos;
    }

    public void OnMouseExitFurniture()
    {
        furnitureHover.SetActive(false);
    }
}
