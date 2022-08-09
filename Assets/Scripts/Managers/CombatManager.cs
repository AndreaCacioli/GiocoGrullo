using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager
{

    private static CombatManager instance = null;
    private List<ICanCombat> fighters;

    private CombatManager() { }

    public static CombatManager getInstance()
    {
        if (instance == null)
        {
            instance = new CombatManager();
            instance.fighters = new List<ICanCombat>();
        }
        return instance;
    }

    public IEnumerator StartFight(ICanCombat attacker, ICanCombat defender)
    {
        fighters.Add(attacker);
        fighters.Add(defender);
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
                    yield return new WaitForSeconds(2);
                }
                if (j > 0)
                {
                    defender.Attack((IWithHealth)attacker);
                    j--;
                    yield return new WaitForSeconds(2);
                }
            }

        }
        else throw new Exception("ICanCombat foud without a health, cannot take damage!");

        fighters.Remove(attacker);
        fighters.Remove(defender);
    }

    public bool isFighting(ICanCombat warrior)
    {
        if (warrior == null) return false;
        return fighters.Contains(warrior);
    }
}