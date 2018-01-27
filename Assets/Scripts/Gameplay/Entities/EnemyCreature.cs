using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyCreature : BaseBeing
{
    private EnemyProperties properties;

    public EnemyCreature(GameObject _go, Vector2 _position, EnemyProperties _properties)
    {
        GameObject = _go;
        properties = _properties;
        Health = new HealthComponent(this, properties.enemyTroopHealth, GameObject.GetComponentInChildren<BloodParticles>());
        Attack = new AttackComponent(properties.enemyDamage, properties.enemyRange, properties.enemyCooldown, null);
        transform = GameObject.transform;
        transform.localPosition = _position - (Vector2)transform.parent.position;
        navAgent = GameObject.GetComponent<NavMeshAgent>();
        navAgent.destination = _position;
        Speed = new SpeedComponent(navAgent);
    }

    public void UpdatePosition(Vector2 target)
    {
        if(GameObject != null && navAgent != null)
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

    public override void Update()
    {
        Speed.Update();
    }
}