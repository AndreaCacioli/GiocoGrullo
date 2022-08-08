using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class GraphNode
{
    private TileBase tileBase;
    private float travellingCost;
    private List<GraphNode> neighbours;

    public GraphNode(TileBase t)
    {
        this.tileBase = t;
        travellingCost = GameRules.getInstance().Cost(t);
        neighbours = new List<GraphNode>();
    }
    public void addNeighbour(GraphNode graphNode)
    {
        if (!neighbours.Contains(graphNode)) neighbours.Add(graphNode);
    }

    public List<GraphNode> getNeighbours() { return neighbours; }

    public float getTravellingCost() { return travellingCost; }

    public TileBase getTileBase() { return tileBase; }

    public override string ToString()
    {
        return "coords: " + DataStructureManager.getInstance().getCoordinates(this);
    }
}