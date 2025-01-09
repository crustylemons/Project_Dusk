using System.Collections;
using UnityEngine;

public class TileMapController : MonoBehaviour
{

    [Header("Tile Maps")]
    [SerializeField] private Rigidbody2D longGrassRbOne;
    [SerializeField] private Rigidbody2D longGrassRbTwo;

    [SerializeField] private float forceAmount = -5f;

    [Header("Tree Spawning")]
    [SerializeField] private Vector2 treeSpawnTop;
    [SerializeField] private Vector2 treeSpawnBottom;

    private bool catIsRunning = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (catIsRunning)
        {
            longGrassRbOne.velocityX = forceAmount;
            longGrassRbTwo.velocityX = forceAmount;
        }
    }

    private void Update()
    {
        if (catIsRunning)
        {
            if (Mathf.RoundToInt(longGrassRbOne.gameObject.transform.position.x) == -16)
            {
                longGrassRbOne.gameObject.transform.position = new Vector2(16, 0);
            }

            if (Mathf.RoundToInt(longGrassRbTwo.gameObject.transform.position.x) == -16)
            {
                longGrassRbTwo.gameObject.transform.position = new Vector2(16, 0);
            }
        }
        else
        {
            // Reset positions if the cat has stopped moving
            longGrassRbOne.gameObject.transform.position = new Vector2(0, 0);
            longGrassRbTwo.gameObject.transform.position = new Vector2(16, 0);
        }
    }

    public void SetCatIsRunning(bool isCatRunning)
    {
        catIsRunning = isCatRunning;
    }
}
