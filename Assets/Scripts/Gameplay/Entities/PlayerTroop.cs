using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerTroop : BaseBeing
{
    private float playerTroopHealth = 100;
    private float playerDamage = 20;
    private float playerRange = 4;
    private float playerCooldown = 0.1f;

    public GameObject TroopGO;

    public int Index;

    private AnimatedCharacter animation;

    public PlayerTroop(GameObject _go, int _index, Vector2 _position)
    {
        TroopGO = _go;
        Index = _index;
        transform = TroopGO.transform;
        transform.localPosition = _position;
        Health = new HealthComponent(playerTroopHealth, TroopGO.GetComponentInChildren<HealthBar>());
        Attack = new AttackComponent(playerDamage, playerRange, playerCooldown);
        navAgent = TroopGO.GetComponent<NavMeshAgent>();
        navAgent.destination = Position;
        animation = TroopGO.GetComponentInChildren<AnimatedCharacter>();
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
}
