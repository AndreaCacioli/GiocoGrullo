using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pathfinder
{
    public static List<Vector3> findPath(Tilemap tilemap, GraphNode start, GraphNode destination)
    {
        List<Vector3> steps = new List<Vector3>();

        PriorityQueue<GraphNode> queue = new PriorityQueue<GraphNode>();

        queue.enqueue(start, h(start, destination), null, 0);
        Node<GraphNode> node_extracted = null;
        Debug.Log("Starting the pathfinder");

        while (!queue.isEmpty())
        {
            Debug.Log("The queue is " + queue);
            node_extracted = queue.dequeue();
            GraphNode node_current = node_extracted.getItem();
            Debug.Log("Woriking on " + node_current);

            if (node_current == destination) break;

            foreach (GraphNode node_successor in node_current.getNeighbours())
            {
                float newg = node_extracted.getG() + node_current.getTravellingCost();

                queue.enqueue(node_successor, newg + h(node_successor, destination), node_extracted, newg);
            }
        }

        Debug.Log("Found path!!!");

        while (node_extracted != null)
        {
            steps.Add(tilemap.CellToWorld(DataStructureManager.getInstance().getCoordinates(node_extracted.getItem())));
            node_extracted = node_extracted.getVisitedBy();

        }

        steps.Reverse();
        return steps;
    }

    private static float h(GraphNode a, GraphNode b)
    {
        Vector3Int a_coord = DataStructureManager.getInstance().getCoordinates(a);
        Vector3Int b_coord = DataStructureManager.getInstance().getCoordinates(b);

        return Vector3Int.Distance(a_coord, b_coord);
    }
}