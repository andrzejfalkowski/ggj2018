using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerTroop : BaseBeing
{
    public int Index;
    public bool IsInfected;
    public bool IsDead;

    public PlayerTroop(GameObject _go, int _index, Vector2 _position, PlayerProperties properties) : base(_go, _position)
    {
        Index = _index;
        IsInfected = false;
        IsDead = false;
        Health = new HealthComponent(this, properties.playerTroopHealth * UnityEngine.Random.Range(0.9f, 1.1f),
            // (1f - properties.playerTroopVariation, 1f + properties.playerTroopVariation,)
            GameObject.GetComponentInChildren<BloodParticles>(), 
                                     GameObject.GetComponentInChildren<HealthBar>());
        Attack = new AttackComponent(properties.playerDamage * UnityEngine.Random.Range(0.9f, 1.1f),
            // (1f - properties.playerDamageVariation, 1f + properties.playerDamageVariation,)
            properties.playerRange, properties.playerCooldown, animation);
        Speed = new SpeedComponent(properties.playerSpeed * UnityEngine.Random.Range(0.9f, 1.1f), navAgent);
        animation.Init(this);
        animation.SetMoveTarget(Position);
    }

    public void UpdatePosition(Vector2 target)
    {
        if (navAgent != null)
        {
            navAgent.SetDestination(target);
            animation.SetMoveTarget(target);
        }

        SupplyManager.Instance.UpdateLastMovePoint(target);
    }

    public override bool IsAlive()
    {
        return base.IsAlive() || !IsDead;
    }

    public bool ShouldBeInfected()
    {
        return !Health.IsAlive();
    }

    public void ChangeColorToInfected()
    {
        animation.ChangeSpriteColor(Color.red);
    }

    public void ChangeColorToNormal()
    {
        animation.ChangeSpriteColor(Color.white);
    }

    public override void Update()
    {
        Attack.Update();
    }
}
