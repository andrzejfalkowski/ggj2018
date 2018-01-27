using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCreature
{
    public GameObject EnemyGO;
    
    public Vector2 Position;

    private Transform transform;
    private NavMeshAgent navAgent;

    public EnemyCreature(GameObject _go, Vector2 _position)
    {
        EnemyGO = _go;
        Position = _position;
        transform = EnemyGO.transform;
        transform.localPosition = Position - (Vector2)transform.parent.position;
        navAgent = EnemyGO.GetComponent<NavMeshAgent>();
        navAgent.destination = Position;
    }

    public void UpdatePosition(Vector2 target)
    {
        navAgent.destination = target;
    }
}