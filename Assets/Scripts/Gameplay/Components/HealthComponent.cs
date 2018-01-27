using UnityEngine;

public class HealthComponent
{
    private HealthBar barView;
    private BloodParticles particles;

    public float MaxHealth;
    public float CurrentHealth;

    public BaseBeing Owner;

    public HealthComponent(BaseBeing _owner, float _maxHealth, BloodParticles _particles, HealthBar _barView = null)
    {
        Owner = _owner;
        MaxHealth = CurrentHealth = _maxHealth;
        particles = _particles;
        barView = _barView;
    }

    public void Cure()
    {
        CurrentHealth = MaxHealth;
        RefreshView();
    }

    public void MakeDamage(float damage)
    {
        CurrentHealth = Mathf.Max(0, CurrentHealth - damage);
        RefreshView();
        particles.Play(CurrentHealth == 0);
        Owner.Speed.Stagger();
    }

    public bool IsAlive()
    {
        return CurrentHealth > 0;
    }

    private void RefreshView()
    {
        if(barView != null)
        {
            barView.SetFill(CurrentHealth / MaxHealth);
        }
    }
}