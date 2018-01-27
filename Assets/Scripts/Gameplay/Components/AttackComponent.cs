using UnityEngine;

public class AttackComponent
{
    public float Damage;
    public float Range;
    public float CooldownTime;

    private float lastAttackTime = 0;
    private AnimatedCharacter animation;

    public AttackComponent(float _damage, float _range, float _cooldownTime, AnimatedCharacter _animation)
    {
        Damage = _damage;
        Range = _range;
        CooldownTime = _cooldownTime;
        animation = _animation;
    }

    public bool IsAttackPossible()
    {
        return Time.time - lastAttackTime >= CooldownTime;
    }

    public void PerformAttack(HealthComponent target)
    {
        target.MakeDamage(Damage);
        lastAttackTime = Time.time;
        if (animation != null)
        {
            animation.AnimateAttack((Vector2)target.Owner.GameObject.transform.position);
        }
    }
}