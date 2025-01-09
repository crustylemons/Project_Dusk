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


    private void OnMouseOver()
    {
        if (cursorManager.GetCursorVisibility())
        {
            cursorManager.ChangeCursor(cursorTexture);
        }
    }

    private void OnMouseExit()
    {
        if (cursorManager.GetCursorVisibility())
        {
            cursorManager.ChangeBackCursor();
        }
    }

    private void OnMouseDown()
    {
        if (cursorManager.GetCursorVisibility())
        {
            Debug.Log("mouse was pressed");
            cutsceneController.StartCutscene(cutscene.GetName());
        }
    }
}
