using UnityEngine;

public class AttackComponent
{
    public float Damage;
    public float Range;
    public float CooldownTime;

    private float lastAttackTime = 0;

    public AttackComponent(float _damage, float _range, float _cooldownTime)
    {
        Damage = _damage;
        Range = _range;
        CooldownTime = _cooldownTime;
    }

    public bool IsAttackPossible(Vector2 position, Vector2 target)
    {
        return Time.time - lastAttackTime >= CooldownTime && 
               Vector2.Distance(position, target) <= Range;
    }

    public void PerformAttack(HealthComponent target)
    {
        target.MakeDamage(Damage);
        lastAttackTime = Time.time;
    }
}