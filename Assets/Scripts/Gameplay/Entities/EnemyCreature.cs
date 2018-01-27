using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyCreature : BaseBeing
{
    private float enemyTroopHealth = 100;
    private float enemyDamage = 5;
    private float enemyRange = 0.5f;
    private float enemyCooldown = 0.2f;
    private float enemyScoreValue = 1f;

    public EnemyCreature(GameObject _go, Vector2 _position)
    {
        GameObject = _go;
        Health = new HealthComponent(this, enemyTroopHealth, GameObject.GetComponentInChildren<BloodParticles>());
        Attack = new AttackComponent(enemyDamage, enemyRange, enemyCooldown, null);
        transform = GameObject.transform;
        transform.localPosition = _position - (Vector2)transform.parent.position;
        navAgent = GameObject.GetComponent<NavMeshAgent>();
        navAgent.destination = _position;
        Speed = new SpeedComponent(navAgent);
    }

    public void UpdatePosition(Vector2 target)
    {
        if(navAgent != null)
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
        return enemyScoreValue;
    }

    public override void Update()
    {
        Speed.Update();
    }
}