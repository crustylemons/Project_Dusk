using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyHomeCamera : MonoBehaviour
{
    // Bounds for camera movement
    [SerializeField] private Vector2 minBounds;
    [SerializeField] private Vector2 maxBounds;

    private Vector3 dragOrigin;
    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleDrag();
    }


    private void HandleDrag()
    {
        // Get the drag origin through mouse position
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        // Drag the camera while the mouse is being held down
        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);
            cam.transform.position = ClampCamera(cam.transform.position + difference);
        }
    }

    // Clamp the camera position to stay within the bounds of the map
    private Vector3 ClampCamera(Vector3 targetPos)
    {
        // Accounts for the camera's size in the game world
        float cameraHalfHeight = cam.orthographicSize;
        float cameraHalfWidth = cameraHalfHeight * cam.aspect;

        float clampedX = Mathf.Clamp(targetPos.x, minBounds.x + cameraHalfWidth, maxBounds.x - cameraHalfWidth);
        float clampedY = Mathf.Clamp(targetPos.y, minBounds.y + cameraHalfHeight, maxBounds.y - cameraHalfHeight);

        return new Vector3(clampedX, clampedY, targetPos.z);
    }

    private void OnDrawGizmos()
    {
        // Draw bounds in the editor for visualization
        Gizmos.color = Color.white;
        Gizmos.DrawLine(new Vector3(minBounds.x, minBounds.y, 0), new Vector3(maxBounds.x, minBounds.y, 0));
        Gizmos.DrawLine(new Vector3(maxBounds.x, minBounds.y, 0), new Vector3(maxBounds.x, maxBounds.y, 0));
        Gizmos.DrawLine(new Vector3(maxBounds.x, maxBounds.y, 0), new Vector3(minBounds.x, maxBounds.y, 0));
        Gizmos.DrawLine(new Vector3(minBounds.x, maxBounds.y, 0), new Vector3(minBounds.x, minBounds.y, 0));
    }

}
