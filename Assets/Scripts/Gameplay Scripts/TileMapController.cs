using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapController : MonoBehaviour
{

    [Header("Tile Maps")]
    [SerializeField] private GameObject longGrassOne;
    [SerializeField] private GameObject longGrassTwo;

    [SerializeField] private float tileMapSpeed = 8.0f;

    [Header("Tree Spawning")]
    [SerializeField] private Vector2 treeSpawnTop;
    [SerializeField] private Vector2 treeSpawnBottom;

    private bool catIsRunning = false;

    private void LateUpdate()
    {
        if (catIsRunning)
        {
            Vector2 grassOnePos = longGrassOne.transform.position;
            Vector2 grassTwoPos = longGrassTwo.transform.position;

            // Move the tile maps
            longGrassOne.transform.position = Vector2.MoveTowards(grassOnePos, new Vector2(-20, 0), tileMapSpeed * Time.deltaTime);
            longGrassTwo.transform.position = Vector2.MoveTowards(grassTwoPos, new Vector2(-20, 0), tileMapSpeed * Time.deltaTime);

            // Round the position to nearest pixel-aligned position based on the PPU
            float ppu = 128f;  // Pixels per Unit
            longGrassOne.transform.position = new Vector2(
                Mathf.Round(longGrassOne.transform.position.x * ppu) / ppu,
                Mathf.Round(longGrassOne.transform.position.y * ppu) / ppu
            );

            longGrassTwo.transform.position = new Vector2(
                Mathf.Round(longGrassTwo.transform.position.x * ppu) / ppu,
                Mathf.Round(longGrassTwo.transform.position.y * ppu) / ppu
            );

            if (Mathf.Abs(grassOnePos.x - (-20.0f)) <= 0.1f)
            {
                longGrassOne.transform.position = new Vector2(20, 0);
                if (grassTwoPos.x > 0)
                {
                    longGrassTwo.transform.position = new Vector2(0, 0);
                }
            }

            if (Mathf.Abs(grassTwoPos.x - (-20.0f)) <= 0.1f)
            {
                longGrassTwo.transform.position = new Vector2(20, 0);
                if (grassOnePos.x > 0)
                {
                    //longGrassOne.transform.position = Vector2.MoveTowards(grassOnePos, new Vector2(-1, 0), tileMapSpeed * Time.deltaTime);
                    longGrassOne.transform.position = new Vector2(0, 0);
                }
            }
        }
        else
        {
            // Reset positions if the cat has stopped moving
            longGrassOne.transform.position = new Vector2(0, 0);
            longGrassTwo.transform.position = new Vector2(20, 0);

            // Reset Speed
            tileMapSpeed = 8.0f;
        }
    }

    public void SetCatIsRunning(bool isCatRunning)
    {
        catIsRunning = isCatRunning;
    }

    public float GetTileMapSpeed() { return tileMapSpeed; }

    public void SetSpeed(float speed) { tileMapSpeed = speed;}
}
