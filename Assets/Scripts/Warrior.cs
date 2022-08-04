using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour, IWithHealth, IWithCombatStrength, IWithDefense, IWithDefensiveTools, IWithOffensiveTools, ICanCombat
{
    [SerializeField][Min(0)] private float health;
    [SerializeField][Min(0)] private float baseCombatStrength;
    [SerializeField][Range(0, 1)] private float attackingProbability;
    [SerializeField][Min(0)] private float defense;
    [SerializeField] List<IDefenseTool> defensiveTools;
    [SerializeField] List<IOffenseTool> offensiveTools;

    private IOffenseTool selectedTool = null;

    public float getAttackingProbability()
    {
        return attackingProbability;
    }

    public float getFinalAttackingProbability()
    {
        if (selectedTool == null) return getAttackingProbability();
        else return selectedTool.getHittingProbability();
    }

    public float getBaseCombatStrength()
    {
        return baseCombatStrength;
    }
    public float getFinalCombatStrength()
    {
        return selectedTool == null ? baseCombatStrength : selectedTool.getStrength();
    }

    public float getCurrentHealth()
    {
        return health;
    }

    // Start is called before the first frame update
    void Start()
    {
        health = GameRules.WarriorStartingHealth();
        baseCombatStrength = GameRules.WarriorBaseCombatStrength();
        attackingProbability = GameRules.WarriorAttackingProbability();
        defense = GameRules.WarriorDefense();
    }

    public float getDefenseValue()
    {
        return defense;
    }

    public List<IDefenseTool> GetDefensiveTools()
    {
        return new List<IDefenseTool>(defensiveTools);
    }

    public List<IOffenseTool> GetOffensiveTools()
    {
        return new List<IOffenseTool>(offensiveTools);
    }

    public void TakeDamage(float value)
    {
        defensiveTools.Sort(Comparer<IDefenseTool>.Create((x, y) => (int)((x.getDefenseValue() - y.getDefenseValue()) * 1000)));
        health -= (value - defensiveTools[0].getDefenseValue());
    }

    public void Attack(IWithHealth opponent)
    {
        bool outcome = Random.Range(0, 1) >= this.getFinalAttackingProbability();
        if (outcome)
        {
            opponent.TakeDamage(getFinalCombatStrength());
        }
        else
        {
            opponent.TakeDamage(0);
        }
    }

    public void SelectTool(IOffenseTool tool)
    {
        selectedTool = tool;
    }
}
