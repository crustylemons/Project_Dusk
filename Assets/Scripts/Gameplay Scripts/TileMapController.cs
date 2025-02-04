using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapController : MonoBehaviour
{

    [Header("Tile Maps")]
    [SerializeField] private GameObject longGrassRbOne;
    [SerializeField] private GameObject longGrassRbTwo;

    [SerializeField] private float tileMapSpeed = 5.0f;

    [Header("Tree Spawning")]
    [SerializeField] private Vector2 treeSpawnTop;
    [SerializeField] private Vector2 treeSpawnBottom;

    private bool catIsRunning = false;

    private void Update()
    {
        if (catIsRunning)
        {
            longGrassRbOne.transform.position = Vector2.MoveTowards(longGrassRbOne.transform.position, new Vector2(-16, 0), tileMapSpeed * Time.deltaTime);
            longGrassRbTwo.transform.position = Vector2.MoveTowards(longGrassRbTwo.transform.position, new Vector2(-16, 0), tileMapSpeed * Time.deltaTime);

            if (Mathf.RoundToInt(longGrassRbOne.transform.position.x) == -16)
            {
                longGrassRbOne.transform.position = new Vector2(16, 0);
            }

            if (Mathf.RoundToInt(longGrassRbTwo.transform.position.x) == -16)
            {
                longGrassRbTwo.transform.position = new Vector2(16, 0);
            }
        }
        else
        {
            // Reset positions if the cat has stopped moving
            longGrassRbOne.transform.position = new Vector2(0, 0);
            longGrassRbTwo.transform.position = new Vector2(16, 0);
        }
    }

    // Unfinished tile changing
    private void UpdateTileMap(Tilemap tilemap)
    {

        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    Debug.Log($"{tile.name} has the sprite ?");
                }
            }
        }
    }

    public void SetCatIsRunning(bool isCatRunning)
    {
        catIsRunning = isCatRunning;
    }

    public float GetTileMapSpeed() { return tileMapSpeed; }
}
