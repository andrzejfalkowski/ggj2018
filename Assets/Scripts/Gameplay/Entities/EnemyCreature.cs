using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyCreature : BaseBeing
{
    private EnemyProperties properties;

    public EnemyCreature(GameObject _go, Vector2 _position, EnemyProperties _properties) : base(_go, _position)
    {
        properties = _properties;
        Health = new HealthComponent(this, properties.enemyTroopHealth, GameObject.GetComponentInChildren<BloodParticles>());
        Attack = new AttackComponent(properties.enemyDamage, properties.enemyRange, properties.enemyCooldown, animation);
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