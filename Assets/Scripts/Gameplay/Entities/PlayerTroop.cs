using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerTroop : BaseBeing
{
    public int Index;
    public bool IsInfected;
    public bool IsDead;

    private AnimatedCharacter animation;

    public PlayerTroop(GameObject _go, int _index, Vector2 _position, PlayerProperties properties)
    {
        GameObject = _go;
        Index = _index;
        IsInfected = false;
        IsDead = false;
        animation = GameObject.GetComponentInChildren<AnimatedCharacter>();
        transform = GameObject.transform;
        transform.localPosition = _position;
        Health = new HealthComponent(this, properties.playerTroopHealth, GameObject.GetComponentInChildren<BloodParticles>(), 
                                     GameObject.GetComponentInChildren<HealthBar>());
        Attack = new AttackComponent(properties.playerDamage, properties.playerRange, properties.playerCooldown, animation);
        navAgent = GameObject.GetComponent<NavMeshAgent>();
        navAgent.destination = Position;
        animation.SetMoveTarget(Position);
        animation.Init(this);
        Speed = new SpeedComponent(navAgent);
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
}
