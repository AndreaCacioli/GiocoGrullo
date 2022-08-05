public interface IWithHealth //: ICanCombat
{
    public float getCurrentHealth();

    public void TakeDamage(float value);
}