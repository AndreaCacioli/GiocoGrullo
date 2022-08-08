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
        if (defender is IWithHealth && attacker is IWithHealth)
        {
            uint i = ((IWithOffensiveTools)attacker).getNumberOfAttacks();
            uint j = ((IWithOffensiveTools)defender).getNumberOfAttacks();
            while (i > 0 || j > 0)
            {
                if (i > 0)
                {
                    attacker.Attack((IWithHealth)defender);
                    i--;
                }
                if (j > 0)
                {
                    defender.Attack((IWithHealth)attacker);
                    j--;
                }
            }

        }
        else throw new Exception("ICanCombat foud without a health, cannot take damage!");
    }
}