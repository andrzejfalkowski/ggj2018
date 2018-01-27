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

    public bool IsAttackPossible()
    {
        return Time.time - lastAttackTime >= CooldownTime;
    }

    public void PerformAttack(HealthComponent target)
    {
        target.MakeDamage(Damage);
        lastAttackTime = Time.time;
    }
}