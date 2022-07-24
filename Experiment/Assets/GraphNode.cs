using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class GraphNode
{
    private TileBase tileBase;
    private List<GraphNode> neighbours; 

    public GraphNode(TileBase t)
    {
        this.tileBase = t;
        neighbours = new List<GraphNode>();
    }
    public void addNeighbour(GraphNode graphNode) {
        if(!neighbours.Contains(graphNode))neighbours.Add(graphNode);
    }

    public List<GraphNode> getNeighbours() { return neighbours; }


    public TileBase getTileBase() { return tileBase; }
}