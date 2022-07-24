using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DataStructureManager
{
    private static DataStructureManager instance = null;

    private static Dictionary<Vector3Int, GraphNode> dictionary;

    private DataStructureManager()
    { 
    }

    public static DataStructureManager getInstance()
    {
	    if(instance == null)
        {
            instance = new DataStructureManager();
            dictionary = new Dictionary<Vector3Int, GraphNode>();

	    }
        return instance;
    }

    internal GraphNode getNode(Vector3Int selectedCellCoord)
    {
        GraphNode ret;
        dictionary.TryGetValue(selectedCellCoord, out ret);
        return ret;
    }

    public void initializeWithTilemap(Tilemap tilemap)
    { 
	    if(tilemap != null)
        {
            for (int x = tilemap.cellBounds.min.x; x < tilemap.cellBounds.max.x; x++)
            {
                for (int y = tilemap.cellBounds.min.y; y < tilemap.cellBounds.max.y; y++)
                {
                    for (int z = tilemap.cellBounds.min.z; z < tilemap.cellBounds.max.z; z++)
                    {
                        if(tilemap.GetTile(new Vector3Int(x,y,z)) != null)
                        {   
			                Vector3Int key =  new Vector3Int(x, y, z);
						    TileBase t = tilemap.GetTile(key);
                            GraphNode graphNode = new GraphNode(t);
                            for(int i = y - 1; i <= y + 1; i++)
                            {
                                int x_start;
                                int x_end;
                                if(i == y) {
                                    x_start = x - 1;
                                    x_end = x + 1;
                                }
                                else {
				                    if(y % 2 == 0) { x_start = x - 1; x_end = x; }
                                    else { x_start = x; x_end = x + 1; } 
			                    }
			                    for(int j = x_start; j <= x_end; j++)
                                {
                                    GraphNode possibleNeighbour;
                                    Vector3Int coord = new Vector3Int(j, i, z);
							        bool outcome = dictionary.TryGetValue(coord , out possibleNeighbour);
                                    if(outcome && tilemap.GetTile(coord) != null)
                                    {
                                        graphNode.addNeighbour(possibleNeighbour);
                                        possibleNeighbour.addNeighbour(graphNode);
				                    }
			                    } 
			                }

                            dictionary.Add(key, graphNode);
			            }
                       
                    }
                }

            }

        }
    }

    internal Vector3Int getCoordinates(TileBase selectedTile)
    {
         foreach(KeyValuePair<Vector3Int, GraphNode> pair in dictionary)
         {
	        if (selectedTile != null && pair.Value.getTileBase().Equals(selectedTile))
	        {
                return pair.Key;
	        }

	     }
         return new Vector3Int();
    }

    public Vector3Int getCoordinates(GraphNode graphNode) {

		 foreach(KeyValuePair<Vector3Int, GraphNode> pair in dictionary)
         {
	        if (graphNode != null && pair.Value.Equals(graphNode))
	        {
                return pair.Key;
	        }

	     }

        return new Vector3Int();
    
    }

    internal List<Vector3Int> getNeighboursCoordinates(Vector3Int coordinates)
    {
        List<Vector3Int> ret = new List<Vector3Int>();
        if (!dictionary.ContainsKey(coordinates)) return ret;

        GraphNode node;
        dictionary.TryGetValue(coordinates, out node);
        foreach(GraphNode graphNode in node.getNeighbours())
        {
            foreach(KeyValuePair<Vector3Int, GraphNode> pair in dictionary)
            {
                if (pair.Value.Equals(graphNode))
                {
                    ret.Add(pair.Key);
                    break;
                }

	        }
        }
        return ret;

    }
}
