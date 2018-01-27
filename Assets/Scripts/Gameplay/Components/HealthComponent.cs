using UnityEngine;

public class HealthComponent
{
    public float MaxHealth;
    public float CurrentHealth;

    public HealthComponent(float _maxHealth)
    {
        MaxHealth = CurrentHealth = _maxHealth;
    }

    public void Cure()
    {
        CurrentHealth = MaxHealth;
    }

    public void MakeDamage(float damage)
    {
        CurrentHealth = Mathf.Max(0, CurrentHealth - damage);
    }

    public bool IsAlive()
    {
        return CurrentHealth > 0;
    }
}