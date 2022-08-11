using UnityEngine;

[CreateAssetMenu(fileName = "OffensiveTool", menuName = "ScriptableObjects/OffensiveTool", order = 1)]
public class Weapon : ScriptableObject, IOffenseTool
{
    public Sprite sprite;
    public int strength;
    public uint numberOfAttacks;
    public float hittingProbability;

    public float getHittingProbability()
    {
        return hittingProbability;
    }

    public uint getNumberOfAttacks()
    {
        return numberOfAttacks;
    }

    public float getStrength()
    {

        return strength;
    }
}
