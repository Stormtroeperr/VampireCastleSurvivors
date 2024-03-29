namespace Interfaces
{
    public interface IDamagable
    {
        // Returns the current health after taking damage
        float Damage(float damage);
    }
}