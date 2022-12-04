using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TileCosts", menuName = "ScriptableObjects/TileCosts")]
public class TileCosts : ScriptableObject
{
    public List<TileCost> costs;

    [Serializable]
    public class TileCost
    {
        public List<TileBase> tileBase;
        public float cost;
    }
}
