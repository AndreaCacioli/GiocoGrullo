using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour, IWithHealth, IWithCombatStrength, IWithDefense, IWithDefensiveTools, IWithOffensiveTools, ICanCombat, IWithLeader
{
    public enum Team { A, B };

    [SerializeField] private Team team;
    [SerializeField][Min(0)] private float health;
    [SerializeField][Min(0)] private float baseCombatStrength;
    [SerializeField][Range(0, 1)] private float attackingProbability;
    [SerializeField][Min(0)] private float defense;
    [SerializeField] private uint baseNumberOfAttacks;
    List<IDefenseTool> defensiveTools;
    List<IOffenseTool> offensiveTools;

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
        health = GameRules.getInstance().WarriorStartingHealth();
        baseCombatStrength = GameRules.getInstance().WarriorBaseCombatStrength();
        attackingProbability = GameRules.getInstance().WarriorAttackingProbability();
        defense = GameRules.getInstance().WarriorDefense();
        baseNumberOfAttacks = GameRules.getInstance().WarriorBaseNumberOfAttacks();
        defensiveTools = new List<IDefenseTool>();
        offensiveTools = new List<IOffenseTool>();
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
        if (defensiveTools.Count > 1)
        {
            defensiveTools.Sort(Comparer<IDefenseTool>.Create((x, y) => (int)((x.getDefenseValue() - y.getDefenseValue()) * 1000)));
            value -= defensiveTools[0].getDefenseValue();
        }
        health -= value;
    }

    public void Attack(IWithHealth opponent)
    {
        double value = Random.Range(0f, 1f);
        bool outcome = value >= this.getFinalAttackingProbability();
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

    public Team getLeader()
    {
        return team;
    }

    public IOffenseTool getSelectedTool()
    {
        return selectedTool;
    }

    public uint getNumberOfAttacks()
    {
        if (selectedTool == null) return baseNumberOfAttacks;
        else return selectedTool.getNumberOfAttacks();
    }
}
