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
    private float lastStaggeredSpeed = 0;

    public SpeedComponent(float _speed, NavMeshAgent _agent)
    {
        agent = _agent;
        _agent.speed = _speed;
        defaultSpeed = _speed;
    }

    public void Update()
    {
        if (staggered)
        {
            staggerCooldown -= Time.deltaTime;
            agent.speed = Mathf.Lerp(lastStaggeredSpeed, defaultSpeed, (0.5f - staggerCooldown)*2);
            if(staggerCooldown <= 0)
            {
                agent.speed = defaultSpeed;
                staggered = false;
            }
        }
    }

    public void Stagger()
    {
        staggered = true;
        staggerCooldown = 0.5f;

        if (agent != null)
        {
            agent.speed = Mathf.Clamp(agent.speed * 0.95f, defaultSpeed*0.35f, defaultSpeed);
            lastStaggeredSpeed = agent.speed;
        }
    }
}
