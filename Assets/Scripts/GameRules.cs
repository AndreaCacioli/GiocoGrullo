using UnityEngine.Tilemaps;

public class GameRules
{
    public static float WarriorStartingHealth()
    {
        return 100f;
    }

    public static float GetYellowValue()
    {
        return .39f;
    }
    public static float GetGreenValue()
    {
        return .69f;
    }

    public static float Cost(TileBase t)
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

    internal static float WarriorBaseCombatStrength()
    {
        return 10f;
    }

    internal static float WarriorAttackingProbability()
    {
        return .3f;
    }

    internal static float WarriorDefense()
    {
        return 5;
    }
}