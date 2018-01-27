using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerTroop : BaseBeing
{
    public GameObject TroopGO;

    public int Index;
    public bool IsInfected;
    public bool IsDead;

    private AnimatedCharacter animation;

    public PlayerTroop(GameObject _go, int _index, Vector2 _position, PlayerProperties properties)
    {
        TroopGO = _go;
        Index = _index;
        IsInfected = false;
        IsDead = false;
        animation = TroopGO.GetComponentInChildren<AnimatedCharacter>();
        transform = TroopGO.transform;
        transform.localPosition = _position;
        Health = new HealthComponent(_go, properties.playerTroopHealth, TroopGO.GetComponentInChildren<BloodParticles>(), 
                                     TroopGO.GetComponentInChildren<HealthBar>());
        Attack = new AttackComponent(properties.playerDamage, properties.playerRange, properties.playerCooldown, animation);
        navAgent = TroopGO.GetComponent<NavMeshAgent>();
        navAgent.destination = Position;
        animation.SetMoveTarget(Position);
    }

    public void UpdatePosition(Vector2 target)
    {
        if (navAgent != null)
        {
            navAgent.SetDestination(target);
            animation.SetMoveTarget(target);
        }
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
