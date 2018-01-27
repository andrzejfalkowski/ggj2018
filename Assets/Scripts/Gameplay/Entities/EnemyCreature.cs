using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCreature : BaseBeing
{
    private float enemyTroopHealth = 100;
    private float enemyDamage = 5;
    private float enemyRange = 0.5f;
    private float enemyCooldown = 0.2f;
    private float enemyScoreValue = 1f;

    public GameObject EnemyGO;

    public EnemyCreature(GameObject _go, Vector2 _position)
    {
        EnemyGO = _go;
        Health = new HealthComponent(enemyTroopHealth, EnemyGO.GetComponentInChildren<BloodParticles>());
        Attack = new AttackComponent(enemyDamage, enemyRange, enemyCooldown);
        transform = EnemyGO.transform;
        transform.localPosition = _position - (Vector2)transform.parent.position;
        navAgent = EnemyGO.GetComponent<NavMeshAgent>();
        navAgent.destination = _position;
    }

    public void UpdatePosition(Vector2 target)
    {
        if(navAgent != null)
        {
            navAgent.destination = target;
        }
    }

    public float GetScoreValue()
    {
        return enemyScoreValue;
    }
}