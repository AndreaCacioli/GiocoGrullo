using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    Tilemap tilemap;

    private void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();
    }

    // Start is called before the first frame update
    void Start()
    {

        DataStructureManager.getInstance().initializeWithTilemap(tilemap);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
