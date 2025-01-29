using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverableObject : MonoBehaviour
{
    [Tooltip("Texture used when hovered over")]
    [SerializeField] private Texture2D cursorTexture;

    [SerializeField] private CursorManager cursorManager;

    [SerializeField] private CutsceneController cutsceneController;
    [SerializeField] private Cutscene cutscene;

    private bool isFurniture = false;


    private void Start()
    {
        // Validate if it's furniture
        if (gameObject.GetComponent<Furniture>() != null) { isFurniture = true; }
        else { isFurniture = false; }
    }

    private void OnMouseOver()
    {
        if (cursorManager.GetCursorVisibility())
        {
            cursorManager.ChangeCursor(cursorTexture);

            if (isFurniture)
            {
                // UI coding here
            }
        }
    }

    private void OnMouseExit()
    {
        if (cursorManager.GetCursorVisibility())
        {
            cursorManager.ChangeBackCursor();

            if (isFurniture)
            {
                // UI coding here
            }
        }
    }

    private void OnMouseDown()
    {
        if (cursorManager.GetCursorVisibility() && !isFurniture)
        {
            cutsceneController.StartCutscene(cutscene.GetName());
        }
    }
}
