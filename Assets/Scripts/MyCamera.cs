using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCamera : MonoBehaviour
{

    public void UpdateCameraPos(Transform newPos)
    {
        gameObject.transform.position = new Vector3(newPos.position.x, newPos.position.y, -10);
    }
}
