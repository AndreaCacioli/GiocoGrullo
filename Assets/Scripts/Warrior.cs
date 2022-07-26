using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour, IWithHealth, IWithCombatStrength, IWithDefense, IWithDefensiveTools, IWithOffensiveTools, ICanCombat, IWithLeader
{
    public enum Team { A, B };

    [SerializeField] private Team team;
    [SerializeField][Min(0)] private float _health;
    public float Health
    {
        get
        {
            return _health;
        }
        private set
        {
            float diff = value - _health;
            _health = value;
            onHealthChanged?.Invoke(diff);
        }
    }
    [SerializeField][Min(0)] private float baseCombatStrength;
    [SerializeField][Range(0, 1)] private float attackingProbability;
    [SerializeField][Min(0)] private float defense;
    [SerializeField] private uint baseNumberOfAttacks;
    List<IDefenseTool> defensiveTools;
    [SerializeField] List<Weapon> offensiveTools;

    [SerializeField] private Weapon _selectedTool = null;

    public Weapon SelectedTool
    {
        get
        {
            return _selectedTool;
        }
        set
        {
            _selectedTool = value;
            onOffensiveToolChanged?.Invoke(value);
        }
    }


    public event IWithHealth.takenDamage onHealthChanged;

    public event IWithOffensiveTools.OffensiveToolChangedHandler onOffensiveToolChanged;
    public event ICanCombat.OnAttackHandler onAttack;

    public float getAttackingProbability()
    {
        return attackingProbability;
    }

    public float getFinalAttackingProbability()
    {
        if (SelectedTool == null) return getAttackingProbability();
        else return SelectedTool.getHittingProbability();
    }

    public float getBaseCombatStrength()
    {
        return baseCombatStrength;
    }
    public float getFinalCombatStrength()
    {
        return SelectedTool == null ? baseCombatStrength : SelectedTool.getStrength();
    }

    public float getCurrentHealth()
    {
        return Health;
    }

    // Start is called before the first frame update
    void Start()
    {
        Health = GameRules.getInstance().WarriorStartingHealth();
        baseCombatStrength = GameRules.getInstance().WarriorBaseCombatStrength();
        attackingProbability = GameRules.getInstance().WarriorAttackingProbability();
        defense = GameRules.getInstance().WarriorDefense();
        baseNumberOfAttacks = GameRules.getInstance().WarriorBaseNumberOfAttacks();
        defensiveTools = new List<IDefenseTool>();
        if (offensiveTools.Count >= 1) SelectedTool = offensiveTools[0];
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
        Health -= value;
        if (Health <= 0f) Health = 0f;
    }

    public void Attack(IWithHealth opponent)
    {
        double value = Random.Range(0f, 1f);
        bool outcome = value <= this.getFinalAttackingProbability();
        if (outcome)
        {
            float finalValue = getFinalCombatStrength();
            if (GameRules.getInstance().battlesHaveCritDamage())
            {
                double value2 = Random.Range(0f, 1f);
                bool crit = value2 <= GameRules.getInstance().critRate();
                if (crit)
                {
                    finalValue *= GameRules.getInstance().critMultiplier();
                }
            }
            opponent.TakeDamage(finalValue);
        }
        else
        {
            opponent.TakeDamage(0);
        }
        onAttack?.Invoke(((MonoBehaviour)opponent).gameObject);
    }

    public void SelectTool(IOffenseTool tool)
    {
        if (tool == null) SelectedTool = null;
        else if (tool is Weapon) SelectedTool = (Weapon)tool;
    }

    public Team getLeader()
    {
        return team;
    }

    public IOffenseTool getSelectedTool()
    {
        return SelectedTool;
    }

    public uint getNumberOfAttacks()
    {
        if (SelectedTool == null) return baseNumberOfAttacks;
        else return SelectedTool.getNumberOfAttacks();
    }
}
