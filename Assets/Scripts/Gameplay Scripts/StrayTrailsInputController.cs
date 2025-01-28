using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class StrayTrailsInputController : MonoBehaviour
{
    [SerializeField] private GameObject cat;
    private Animator catAnimator;
    private BoxCollider2D catCollider;

    [Header("Controllers")]
    [SerializeField] private TileMapController tileMapController;
    [SerializeField] private StrayTrailsCatController catController;
    [SerializeField] private GameUIController UIController;

    [Header("Positions")]
    [SerializeField] private Vector3 positionOne;
    [SerializeField] private Vector3 positionTwo;


    // Initiate Input Controlling
    public void StartStrayTrails()
    {
        // Animation
        catAnimator = cat.GetComponent<Animator>();
        catAnimator.SetBool("IsMoving", true);
        tileMapController.SetCatIsRunning(true);

        cat.transform.position = positionOne;
        catCollider = cat.GetComponent<BoxCollider2D>();

        StartCoroutine(InputCoroutine());
    }

    private IEnumerator InputCoroutine()
    {
        // Get player input to move the cat
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            gameObject.transform.position = positionOne;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            gameObject.transform.position = positionTwo;
        }

        

        yield return null;
    }

    public void StopStrayTrails()
    {
        cat.GetComponent<Animator>().SetBool("IsMoving", false);
        tileMapController.SetCatIsRunning(false);
        cat.transform.position = Vector3.zero;
    }

}
