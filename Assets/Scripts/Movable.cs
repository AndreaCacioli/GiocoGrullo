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
    public bool IsRunning { get; private set; }

    void Start()
    {
        destination = transform.position;
        fogOfWar = FindObjectOfType<FogOfWar>();
        fogOfWar.clearPosition(transform.position, vision);
        mapTilemap = FindObjectOfType<Map>().GetComponentInChildren<Tilemap>();
    }

    public static void LookAt(GameObject toRotate, Vector3 target)
    {
        var x = Mathf.Abs(toRotate.transform.localScale.x);
        if (target.x < toRotate.transform.position.x) toRotate.transform.localScale = new Vector3(-x, toRotate.transform.localScale.y, toRotate.transform.localScale.z);
        else toRotate.transform.localScale = new Vector3(x, toRotate.transform.localScale.y, toRotate.transform.localScale.z);

    }
    void Update()
    {
        if (Vector3.Distance(destination, transform.position) > .1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            IsRunning = true;
        }
        else
        {
            if (path != null)
            {
                if (path.Count > 0)
                {
                    destination = path[0];
                    Movable.LookAt(gameObject, destination);
                    path.RemoveAt(0);
                }
                else
                {
                    IsRunning = false;
                    path = null;
                }
                fogOfWar.clearPosition(transform.position, vision);
            }

        }

    }

    public float GetMovementPoints()
    {
        return movementPoints;
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
