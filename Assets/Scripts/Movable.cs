using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Movable : MonoBehaviour
{
    FogOfWar FogOfWar;
    [SerializeField] int vision = 2;

    private Vector3 destination;
    [SerializeField] private float speed = 2;
    private List<Vector3> path;
    public bool isRunning  { get; private set; } 

    // Start is called before the first frame update
    void Start()
    {
        destination = transform.position;
        FogOfWar = FindObjectOfType<FogOfWar>();
        FogOfWar.clearPosition(transform.position, vision);
    }

    // Update is called once per frame
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
                if(path.Count > 0)
                {
                    destination = path[0];
                    path.RemoveAt(0);
                }
                else 
		        {
                    isRunning = false;
		        }
                FogOfWar.clearPosition(transform.position, vision);
            }
           
        }

    }

    internal void moveTo(GraphNode selectedTile)
    {
        Tilemap mapTilemap;
        Map handleDataStructure = FindObjectOfType<Map>();
        mapTilemap = handleDataStructure.GetComponentInChildren<Tilemap>();
        if (mapTilemap != null)
        {
            path = Pathfinder.findPathVector3(mapTilemap, DataStructureManager.getInstance().getNode(mapTilemap.WorldToCell(new Vector3(transform.position.x, transform.position.y, 0))), selectedTile);
        }
    }

    public void setDestination(Vector3 destination)
    {
        this.destination = destination;
    }

}
