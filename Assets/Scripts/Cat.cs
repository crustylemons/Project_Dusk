using System.Collections;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] private TypingInputController typingController;

    // Variables for smooth walking
    private float smoothTime = 0.3f;
    private Vector2 velocity = Vector3.zero;


    private void Update()
    {
        // Creates smooth transition with cat movement
        gameObject.transform.position = Vector2.SmoothDamp(gameObject.transform.position, typingController.GetTargetPos(), ref velocity, smoothTime);

    }

}
