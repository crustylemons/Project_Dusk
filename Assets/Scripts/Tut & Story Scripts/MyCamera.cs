using UnityEngine;
using UnityEngine.UI;

public class MyCamera : MonoBehaviour
{
    [SerializeField] private Cat cat;
    [SerializeField] private GameObject blackTransition;


    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        blackTransition.SetActive(true);
    }

    private void Update()
    {
        // Creates smooth transition with camera movement
        Vector3 targetPosition = new Vector3(cat.transform.position.x, cat.transform.position.y, -10);
        gameObject.transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
