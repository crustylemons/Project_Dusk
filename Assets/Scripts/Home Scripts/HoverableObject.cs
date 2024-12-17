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
            Debug.Log("Mouse entered " + gameObject.name);
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
            cutsceneController.StartCutscene(cutscene.GetName());
        }
    }
}
