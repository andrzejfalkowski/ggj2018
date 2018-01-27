using UnityEngine;

public class AttackComponent
{
    public float Damage;
    public float Range;
    public float CooldownTime;

    private float lastAttackTime = 0;
    private AnimatedCharacter animation;

    private float defaultDamage;
    private bool doubleDamage = false;
    private float doubleDamageCooldown = 0f;

    public AttackComponent(float _damage, float _range, float _cooldownTime, AnimatedCharacter _animation)
    {
        Damage = _damage;
        defaultDamage = _damage;
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

    public void DoubleDamage()
    {
        Damage = 2f * defaultDamage;
        doubleDamage = true;
        doubleDamageCooldown = 5f;
    }

    public void Update()
    {
        if (doubleDamage)
        {
            doubleDamageCooldown -= Time.deltaTime;

            if (doubleDamageCooldown <= 0)
            {
                doubleDamage = false;
                Damage = defaultDamage;
            }
        }
    }
}