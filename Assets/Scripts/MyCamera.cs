using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MyCamera : MonoBehaviour
{
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Cat cat;

    private void Update()
    {
        Vector3 targetPosition = new Vector3(cat.transform.position.x, cat.transform.position.y, -10);
        gameObject.transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
