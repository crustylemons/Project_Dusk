using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapController : MonoBehaviour
{

    [Header("Tile Maps")]
    [SerializeField] private GameObject longGrassOne;
    [SerializeField] private GameObject longGrassTwo;

    [SerializeField] private float tileMapSpeed = 5.0f;

    [Header("Tree Spawning")]
    [SerializeField] private Vector2 treeSpawnTop;
    [SerializeField] private Vector2 treeSpawnBottom;

    private bool catIsRunning = false;

    private void Update()
    {
        if (catIsRunning)
        {
            Vector2 grassOnePos = longGrassOne.transform.position;
            Vector2 grassTwoPos = longGrassTwo.transform.position;

            longGrassOne.transform.position = Vector2.MoveTowards(grassOnePos, new Vector2(-16, 0), tileMapSpeed * Time.deltaTime);
            longGrassTwo.transform.position = Vector2.MoveTowards(grassTwoPos, new Vector2(-16, 0), tileMapSpeed * Time.deltaTime);

            if (Mathf.Abs(grassOnePos.x - (-16f)) <= 0.1f)
            {
                longGrassOne.transform.position = new Vector2(16, 0);
                if (grassTwoPos.x > 0)
                {
                    longGrassTwo.transform.position = Vector2.MoveTowards(grassTwoPos, new Vector2(0, 0), tileMapSpeed * Time.deltaTime);
                }
                //longGrassRbTwo.transform.position = new Vector2(0, 0);
            }

            if (Mathf.Abs(grassTwoPos.x - (-16f)) <= 0.1f)
            {
                longGrassTwo.transform.position = new Vector2(16, 0);
                if (grassOnePos.x > 0)
                {
                    longGrassOne.transform.position = Vector2.MoveTowards(grassOnePos, new Vector2(0, 0), tileMapSpeed * Time.deltaTime);
                }
                //longGrassRbOne.transform.position = new Vector2(0, 0);
            }

            //if (Mathf.RoundToInt(longGrassRbOne.transform.position.x) == -16)
            //{
            //    longGrassRbOne.transform.position = new Vector2(16, 0);
            //    longGrassRbTwo.transform.position = new Vector2(0, 0);
            //}

            //if (Mathf.RoundToInt(longGrassRbTwo.transform.position.x) == -16)
            //{
            //    longGrassRbTwo.transform.position = new Vector2(16, 0);
            //    longGrassRbOne.transform.position = new Vector2(0, 0);
            //}
        }
        else
        {
            // Reset positions if the cat has stopped moving
            longGrassOne.transform.position = new Vector2(0, 0);
            longGrassTwo.transform.position = new Vector2(16, 0);
        }
    }

    public void SetCatIsRunning(bool isCatRunning)
    {
        catIsRunning = isCatRunning;
    }

    public float GetTileMapSpeed() { return tileMapSpeed; }
}
