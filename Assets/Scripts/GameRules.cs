using UnityEngine.Tilemaps;

//TODO decide if you want to keep this or move to ScriptableObjects
public class GameRules
{

    //TODO load values from a file
    private static GameRules instance;
    private GameRules() { }
    public static GameRules getInstance()
    {
        if (instance == null) instance = new GameRules();
        return instance;
    }

    public float WarriorStartingHealth()
    {
        return 100f;
    }

    public float GetYellowValue()
    {
        return .39f;
    }
    public float GetGreenValue()
    {
        return .69f;
    }

    public float Cost(TileBase t)
    {
        try
        {
            if (t.name.Substring(0, 7) == "hexDirt") return 1f;
            else if (t.name.Substring(0, 13) == "hexScrublands") return 5f;
            else return float.PositiveInfinity;
        }
        catch
        {
            return float.PositiveInfinity;
        }
    }

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
}