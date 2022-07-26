using UnityEngine.Tilemaps;

public class GameRules
{
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
}