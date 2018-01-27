using UnityEngine;

public class HealthComponent
{
    private HealthBar barView;

    public float MaxHealth;
    public float CurrentHealth;

    public HealthComponent(float _maxHealth, HealthBar _barView = null)
    {
        MaxHealth = CurrentHealth = _maxHealth;
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