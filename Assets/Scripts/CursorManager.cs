using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [Tooltip("Turn off if the player is using typing gameplay")]
    [SerializeField] private bool isCursorVisible = false;

    [Header("Cursors")]

    [Tooltip("The main cursor used when no event is taking place")]
    [SerializeField] private Texture2D defaultCursorTexture;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(defaultCursorTexture, Vector2.zero, CursorMode.ForceSoftware);
        Cursor.visible = isCursorVisible;
    }

    public bool GetCursorVisibility() { return isCursorVisible; }

    public void ChangeCursor(Texture2D newCursorTexture)
    {
        Cursor.SetCursor(newCursorTexture, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void ChangeBackCursor()
    {
        Cursor.SetCursor(defaultCursorTexture, Vector2.zero, CursorMode.ForceSoftware);
    }
}
