using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Movable : MonoBehaviour
{
    private Vector3 destination;
    [SerializeField] private float speed = 2;
    private List<Vector3> path;

    // Start is called before the first frame update
    void Start()
    {
        destination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(destination, transform.position) > .1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);

        }
        else
        {
            if (path != null && path.Count > 0)
            {
                destination = path[0];
                path.RemoveAt(0);
            }
        }

    }

    internal void moveTo(GraphNode selectedTile)
    {
        Tilemap tilemap = FindObjectOfType<Tilemap>();
        if (tilemap != null)
        {
            path = Pathfinder.findPathVector3(tilemap, DataStructureManager.getInstance().getNode(tilemap.WorldToCell(new Vector3(transform.position.x, transform.position.y, 0))), selectedTile);
        }
    }

    public void setDestination(Vector3 destination)
    {
        this.destination = destination;
    }

}
