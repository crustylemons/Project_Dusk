using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StrayTrailsInputController : MonoBehaviour
{
    [SerializeField] private GameObject cat;
    private Animator catAnimator;

    [Header("Script Connections")]
    [SerializeField] private TileMapController tileMapController;
    [SerializeField] private StrayTrailsCatController catController;
    [SerializeField] private GameUIController UIController;

    [Header("Positions")]
    [SerializeField] private Vector3 positionOne;
    [SerializeField] private Vector3 positionTwo;

    private bool isPlaying = false;


    // Initiate Input Controlling
    public void StartStrayTrails()
    {
        // Cat Animation
        catAnimator = cat.GetComponent<Animator>();
        catAnimator.SetBool("IsMoving", true);
        cat.transform.position = positionOne;

        // Tell other scripts to start the game
        UIController.StartStrayTrails();
        tileMapController.SetCatIsRunning(true);

        // Inside script calls
        isPlaying = true;
        StartCoroutine(InputCoroutine());
    }

    private IEnumerator InputCoroutine()
    {
        while (isPlaying)
        {
            // Get player input to move the cat
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                cat.transform.position = positionOne;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                cat.transform.position = positionTwo;
            }
        
            yield return null;
        }
        
    }

    public void StopStrayTrails()
    {
        // Cat Animation
        cat.GetComponent<Animator>().SetBool("IsMoving", false);
        cat.transform.position = Vector3.zero;

        isPlaying = false;

        // Tell other scripts that the game is over
        tileMapController.SetCatIsRunning(false);
        UIController.StopStrayTrails();
        
    }

    public void AddItem(GameObject item)
    {
        StartCoroutine(ItemInputCoroutine(item.GetComponent<Item>()));
    }

    private IEnumerator ItemInputCoroutine(Item item)
    {

        KeyCode[] keyCodes = new KeyCode[4] { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };
        KeyCode chosenKey = keyCodes[Random.Range(0, keyCodes.Length)];

        // UI
        GameObject itemUI = UIController.CreateNewItemUI();
        itemUI.GetComponentInChildren<Text>().text = chosenKey.ToString();
        StartCoroutine(UpdateItemUIPos(item, itemUI));

        yield return new WaitUntil(() => Input.GetKeyDown(chosenKey) || item == null);

        // If it wasn't already destroyed
        if (item != null)
        {
            item.Collect();
        }
    }

    private IEnumerator UpdateItemUIPos(Item item, GameObject UIElement)
    {
        Vector2 velocity = Vector2.zero;
        UIElement.transform.position = Camera.main.WorldToScreenPoint(item.transform.position);
        while (!item.IsDestroyed())
        {
            Vector2 currentUIPos = UIElement.transform.position;
            Vector2 targetUIPos = Camera.main.WorldToScreenPoint(item.transform.position);

            
            UIElement.transform.position = Vector2.SmoothDamp(currentUIPos, targetUIPos, ref velocity, 0.1f);
            yield return null;
        }
        Destroy(UIElement);
    }

    public bool IsPlaying() { return isPlaying; }
}
