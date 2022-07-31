using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Movable : MonoBehaviour
{
    FogOfWar fogOfWar;
    Tilemap mapTilemap;
    [SerializeField] int vision = 2;
    [SerializeField] private float movementPoints = 4f;

    private Vector3 destination;
    [SerializeField] private float speed = 2;
    private List<Vector3> path;
    public bool isRunning { get; private set; }

    void Start()
    {
        destination = transform.position;
        fogOfWar = FindObjectOfType<FogOfWar>();
        fogOfWar.clearPosition(transform.position, vision);
        mapTilemap = FindObjectOfType<Map>().GetComponentInChildren<Tilemap>();
    }

    void Update()
    {
        if (Vector3.Distance(destination, transform.position) > .1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            isRunning = true;
        }
        else
        {
            if (path != null)
            {
                if (path.Count > 0)
                {
                    destination = path[0];
                    path.RemoveAt(0);
                }
                else
                {
                    isRunning = false;
                    path = null;
                }
                fogOfWar.clearPosition(transform.position, vision);
            }

        }

    }

    internal void moveTo(GraphNode selectedTile)
    {
        if (selectedTile == null) return;
        if (mapTilemap != null)
        {
            path = Pathfinder.findPathVector3(mapTilemap, fogOfWar, DataStructureManager.getInstance().getNode(mapTilemap.WorldToCell(new Vector3(transform.position.x, transform.position.y, 0))), selectedTile, ref movementPoints);
        }
    }

}
