using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerTroop
{
    public GameObject TroopGO;

    public int Index;
    public Vector2 Position;

    private Transform transform;
    private NavMeshAgent navAgent;
    private AnimatedCharacter animation;

    public PlayerTroop(GameObject _go, int _index, Vector2 _position)
    {
        TroopGO = _go;
        Index = _index;
        Position = _position;
        transform = TroopGO.transform;
        transform.localPosition = Position;
        navAgent = TroopGO.GetComponent<NavMeshAgent>();
        navAgent.destination = Position;
        animation = TroopGO.GetComponentInChildren<AnimatedCharacter>();
        animation.SetMoveTarget(Position);
    }

    public void UpdatePosition(Vector2 target)
    {
        navAgent.destination = target;
        animation.SetMoveTarget(target);
    }
}
