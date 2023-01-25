using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyAnimator))]
public class AnimateAlongAgent : MonoBehaviour
{
    private const float  minimalVelocity = 0.1f;
    public NavMeshAgent agent;
    public EnemyAnimator animator;

    void Update()
    {
        if (ShouldMove())
        {
            animator.Move(agent.velocity.magnitude);
        }    
        else
        {
            animator.StopMove();
        }
            
    }

    private bool ShouldMove()
    {
       return agent.velocity.magnitude > minimalVelocity && agent.remainingDistance > agent.radius;
    }
}
