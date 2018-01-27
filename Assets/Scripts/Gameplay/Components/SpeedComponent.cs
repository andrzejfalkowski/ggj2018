using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpeedComponent
{
    private NavMeshAgent agent;
    private float defaultSpeed = 1f;

    private bool staggered = false;
    private float staggerCooldown = 0f;

    public SpeedComponent(NavMeshAgent _agent)
    {
        agent = _agent;

        defaultSpeed = _agent.speed;
    }

    public void Update()
    {
        if (staggered)
        {
            staggerCooldown -= Time.deltaTime;

            if (staggerCooldown <= 0)
            {
                staggered = false;
                if (agent != null)
                {
                    agent.speed = defaultSpeed;
                }
            }
        }
    }

    public void Stagger()
    {
        staggered = true;
        staggerCooldown = 0.5f;

        if (agent != null)
        {
            agent.speed = 0f;//defaultSpeed / 2f;
        }
    }
}
