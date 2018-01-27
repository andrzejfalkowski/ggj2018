﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseBeing
{
    public HealthComponent Health;
    public AttackComponent Attack;

    public Vector2 Position { get { return transform != null ? (Vector2)transform.position : Vector2.zero; } }

    protected Transform transform;
    protected NavMeshAgent navAgent;

    public bool IsAlive()
    {
        return Health.IsAlive();
    }

    public void PerformAttack(HealthComponent opponentHealth, Vector2 opponentPosition)
    {
        if (Attack.IsAttackPossible(Position, opponentPosition))
        {
            Attack.PerformAttack(opponentHealth);
        }
    }
}