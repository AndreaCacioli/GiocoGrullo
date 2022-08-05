using System;

public class CombatManager
{

    private static CombatManager instance = null;

    private CombatManager() { }

    public static CombatManager getInstance()
    {
        if (instance == null)
        {
            instance = new CombatManager();
        }
        return instance;
    }

    public void StartFight(ICanCombat attacker, ICanCombat defender)
    {
        if (defender is IWithHealth) attacker.Attack((IWithHealth)defender);
        else throw new Exception("ICanCombat foud without a health, cannot take damage!");
    }
}