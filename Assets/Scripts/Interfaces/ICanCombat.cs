using UnityEngine;

public interface ICanCombat
{
    public delegate void OnAttackHandler(GameObject enemy);
    public event OnAttackHandler onAttack;
    public void Attack(IWithHealth opponent);
}