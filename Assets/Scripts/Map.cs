using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    Tilemap tilemap;

    private void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();
    }

    void Start()
    {
        DataStructureManager.getInstance().initializeWithTilemap(tilemap);
    }
}
