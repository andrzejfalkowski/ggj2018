using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyCreature : BaseBeing
{
    public GameObject EnemyGO;

    private EnemyProperties properties;

    public EnemyCreature(GameObject _go, Vector2 _position, EnemyProperties _properties)
    {
        EnemyGO = _go;
        properties = _properties;
        Health = new HealthComponent(_go, properties.enemyTroopHealth, EnemyGO.GetComponentInChildren<BloodParticles>());
        Attack = new AttackComponent(properties.enemyDamage, properties.enemyRange, properties.enemyCooldown, null);
        transform = EnemyGO.transform;
        transform.localPosition = _position - (Vector2)transform.parent.position;
        navAgent = EnemyGO.GetComponent<NavMeshAgent>();
        navAgent.destination = _position;
    }

    public void UpdatePosition(Vector2 target)
    {
        if(EnemyGO != null && navAgent != null)
        {
            try
            {
                navAgent.destination = target;
            }
            catch(Exception ex)
            {
            }
        }
    }

    public float GetScoreValue()
    {
        return properties.enemyScoreValue;
    }
}