public interface IWithHealth //: ICanCombat
{
    public delegate void takenDamage(float amount);
    public event takenDamage onHealthChanged;

    public float getCurrentHealth();

    public void TakeDamage(float value);
}