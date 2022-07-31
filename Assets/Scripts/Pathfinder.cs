using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pathfinder
{
    public static List<GraphNode> findPath(GraphNode start, GraphNode destination, Dictionary<GraphNode, bool> visibility)
    {
        if (destination.getTravellingCost() == float.PositiveInfinity) return null;

        HashSet<GraphNode> closedSet = new HashSet<GraphNode>();
        HashSet<GraphNode> openSet = new HashSet<GraphNode>();

        Dictionary<GraphNode, float> gScore = new Dictionary<GraphNode, float>();
        Dictionary<GraphNode, float> hScore = new Dictionary<GraphNode, float>();
        Dictionary<GraphNode, float> fScore = new Dictionary<GraphNode, float>();

        Dictionary<GraphNode, GraphNode> came_from = new Dictionary<GraphNode, GraphNode>();

        gScore[start] = 0;
        hScore[start] = h(start, destination);
        fScore[start] = hScore[start];
        came_from[start] = null;

        openSet.Add(start);

        while (openSet.Count != 0)
        {
            GraphNode x = smallest(openSet, fScore);
            if (x == destination)
            {
                //came_from[destination] = x;
                return reconstructPath(came_from, destination);
            }
            openSet.Remove(x);
            closedSet.Add(x);

            float tentative_g_score;
            bool tentative_is_better;
            foreach (GraphNode neighbour in x.getNeighbours())
            {
                if (closedSet.Contains(neighbour) || !visibility[neighbour]) continue;
                tentative_g_score = gScore[x] + x.getTravellingCost();

                if (!openSet.Contains(neighbour))
                {
                    openSet.Add(neighbour);
                    tentative_is_better = true;
                }
                else if (tentative_g_score < gScore[neighbour])
                {
                    tentative_is_better = true;
                }
                else
                {
                    tentative_is_better = false;
                }

                if (tentative_is_better)
                {
                    came_from[neighbour] = x;
                    gScore[neighbour] = tentative_g_score;
                    hScore[neighbour] = h(neighbour, destination);
                    fScore[neighbour] = gScore[neighbour] + hScore[neighbour];
                }
            }

            if (smallest(openSet, fScore) == null) //Every node in the open set has infinite f value;
            {
                return reconstructPath(came_from, came_from[smallest(closedSet, hScore)]);
            }

        }
        return null;
    }

    internal static List<Vector3> findPathVector3(Tilemap tilemap, FogOfWar fogOfWar, GraphNode graphNode, GraphNode selectedTile, ref float movementPoints)
    {
        List<Vector3> ret = new List<Vector3>();
        Dictionary<GraphNode, bool> visibility = new Dictionary<GraphNode, bool>();
        foreach (GraphNode node in DataStructureManager.getInstance().getAllNodes())
        {
            visibility.Add(node, fogOfWar.isVisible(tilemap.CellToWorld(DataStructureManager.getInstance().getCoordinates(node))));
        }
        var steps = findPath(graphNode, selectedTile, visibility);
        if (steps == null) return null;
        steps.RemoveAt(0);
        foreach (GraphNode g in steps)
        {
            if (movementPoints <= 0) break;
            movementPoints -= g.getTravellingCost();
            var v = DataStructureManager.getInstance().getCoordinates(g);
            ret.Add(tilemap.CellToWorld(new Vector3Int(v.x, v.y, -1)));
        }
        return ret;
    }

    private static List<GraphNode> reconstructPath(Dictionary<GraphNode, GraphNode> came_from, GraphNode destination)
    {
        List<GraphNode> steps = new List<GraphNode>();
        if (came_from[destination] != null)
        {
            GraphNode x = destination;
            while (x != null)
            {
                steps.Add(x);
                x = came_from[x];
            }
            steps.Reverse();
            return steps;
        }
        else return null;
    }

    private static GraphNode smallest(HashSet<GraphNode> set, Dictionary<GraphNode, float> score)
    {
        float min = float.MaxValue;
        GraphNode ret = null;
        foreach (GraphNode node in set)
        {
            if (score[node] < min)
            {
                ret = node;
                min = score[node];
            }
        }

        return ret;
    }

    private static float h(GraphNode a, GraphNode b)
    {
        Vector3Int a_coord = DataStructureManager.getInstance().getCoordinates(a);
        Vector3Int b_coord = DataStructureManager.getInstance().getCoordinates(b);

        return Vector3Int.Distance(a_coord, b_coord);
    }
}