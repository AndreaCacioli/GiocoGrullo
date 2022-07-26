﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameRules
{
    Dictionary<TileBase, float> tileCosts;

    private static GameRules instance;
    private GameRules()
    {
        tileCosts = new Dictionary<TileBase, float>();
        TileCosts obj = Resources.Load<TileCosts>("Constants/Rules/TileCosts");
        foreach (var item in obj.costs)
        {
            foreach (TileBase tile in item.tileBase)
            {
                tileCosts.Add(tile, item.cost);
            }
        }
    }


    public static GameRules getInstance()
    {
        if (instance == null) instance = new GameRules();
        return instance;
    }
    //////////////////////////////////////////////////////////////
    //BUILDER SECTION
    //////////////////////////////////////////////////////////////
    public int buildersBaseCharges()
    {
        return 3;
    }

    //////////////////////////////////////////////////////////////
    //END OF BUILDER SECTION
    //////////////////////////////////////////////////////////////
    public float WarriorStartingHealth()
    {
        return 100f;
    }

    //TODO
    //This UI stuff should be moved to the UI Constants
    public float GetYellowValue()
    {
        return .39f;
    }
    public float GetGreenValue()
    {
        return .69f;
    }
    //////////////////////////////////////////////////

    public float Cost(TileBase t)
    {
        try
        {
            if (tileCosts.ContainsKey(t))
            {
                return tileCosts[t];
            }
            else return float.PositiveInfinity;
        }
        catch
        {
            return float.PositiveInfinity;
        }
    }

    //////////////////////////////////////////////////////////////
    //WARRIOR SECTION
    //////////////////////////////////////////////////////////////
    public float WarriorBaseCombatStrength()
    {
        return 10f;
    }

    public float WarriorAttackingProbability()
    {
        return .3f;
    }

    public float WarriorDefense()
    {
        return 5;
    }

    public uint WarriorBaseNumberOfAttacks()
    {
        return 3;
    }

    public bool battlesHaveCritDamage()
    {
        return true;
    }

    public double critRate()
    {
        return .2;
    }

    public float critMultiplier()
    {
        return 1.25f;
    }

    //////////////////////////////////////////////////////////////
    //END WARRIOR SECTION
    //////////////////////////////////////////////////////////////
}