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
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private GameAudioController audioController;

    [Header("Positions")]
    [SerializeField] private Vector3 positionOne;
    [SerializeField] private Vector3 positionTwo;

    private bool isPlaying = false;


    // Initiate Input Controlling
    public void StartStrayTrails()
    {
        cat.transform.position = positionOne;
        UIController.StartStrayTrails();

        StartCoroutine(TakeInput());
    }

    private IEnumerator TakeInput()
    {
        // Countdown
        audioController.StartCountDown();
        yield return StartCoroutine(CountDown(3));

        // Intiate other scripts
        tileMapController.SetCatIsRunning(true);
        scoreManager.InitializeScoring();
        isPlaying = true;

        // Cat Animation
        catAnimator = cat.GetComponent<Animator>();
        catAnimator.SetBool("IsMoving", true);

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
        scoreManager.StopScoring();
        tileMapController.SetCatIsRunning(false);
        UIController.StopStrayTrails();
        audioController.StopMusic();
    }

    public void AddItem(GameObject item)
    {
        StartCoroutine(ItemInputCoroutine(item.GetComponent<Item>()));
    }

    private IEnumerator ItemInputCoroutine(Item item)
    {

        KeyCode[] keyCodes = new KeyCode[26] {
            KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E,
            KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.I, KeyCode.J,
            KeyCode.K, KeyCode.L, KeyCode.M, KeyCode.N, KeyCode.O,
            KeyCode.P, KeyCode.Q, KeyCode.R, KeyCode.S, KeyCode.T,
            KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X, KeyCode.Y,
            KeyCode.Z
        };
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
        UIElement.transform.position = Camera.main.WorldToScreenPoint(item.transform.position); // Set initial position

        while (!item.IsDestroyed())
        {
            Vector2 currentUIPos = UIElement.transform.position;
            Vector2 targetUIPos = Camera.main.WorldToScreenPoint(item.transform.position);

            UIElement.transform.position = Vector2.SmoothDamp(currentUIPos, targetUIPos, ref velocity, 0.1f); // Move Target
            

            yield return null; // Wait for next frame
        }
        Destroy(UIElement);
    }

    public bool IsPlaying() { return isPlaying; }

    private IEnumerator CountDown(int seconds)
    {
        int secondsLeft = seconds;
        UIController.SetTimer(secondsLeft, false);

        for (int i = seconds; i > 0; i--)
        {
            yield return new WaitForSeconds(1);
            secondsLeft--;
            UIController.SetTimer(secondsLeft, false);
        }
    }
}
