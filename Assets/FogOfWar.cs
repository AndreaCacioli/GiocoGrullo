using UnityEngine;
using UnityEngine.Tilemaps;

public class FogOfWar : MonoBehaviour
{

    Tilemap fowTilemap;
    Tilemap mapTilemap;
    [SerializeField] TileBase[] fogBasePool;
    [SerializeField] int cellOverflow;

    // Start is called before the first frame update
    void Awake()
    {
        fowTilemap = GetComponentInChildren<Tilemap>();
        var col = FindObjectsOfType<Tilemap>();
        if (col.Length != 2) throw new System.Exception("There is a wrong number of tilemaps in the scene, make sure there are only one Map and one Fog Of War!");
        foreach (Tilemap t in col)
        {
            if (t != fowTilemap) mapTilemap = t;
        }

        InitializeFOW();
    }

    private void InitializeFOW()
    {
        for (int i = mapTilemap.cellBounds.min.x - cellOverflow; i <= mapTilemap.cellBounds.max.x + cellOverflow; i++)
        {
            for (int j = mapTilemap.cellBounds.min.y - cellOverflow; j <= mapTilemap.cellBounds.max.y + cellOverflow; j++)
            {
                TileBase tile = fogBasePool[(int)(Random.value * 3)];
                fowTilemap.SetTile(new Vector3Int(i, j), tile);
            }
        }
    }

    internal bool isVisible(Vector3 WorldCoord)
    {
        WorldCoord.z = -1;
        TileBase tile = fowTilemap.GetTile(fowTilemap.WorldToCell(WorldCoord));
        return tile == null;
    }

    private void Start()
    {

    }

    // Update is called once per frame
    public void clearPosition(Vector3 worldCoordinates, int vision)
    {
        Vector3Int coordinates = fowTilemap.WorldToCell(worldCoordinates);
        for (int i = -vision; i <= vision; i++)
        {
            for (int j = -vision; j <= vision; j++)
            {
                Vector3Int tile = new Vector3Int(coordinates.x + i, coordinates.y + j);
                Vector3 worldTileCoordinates = fowTilemap.CellToWorld(tile);
                if (Vector3.Distance(worldCoordinates, worldTileCoordinates) < vision) fowTilemap.SetTile(tile, null);
            }
        }
    }
}
