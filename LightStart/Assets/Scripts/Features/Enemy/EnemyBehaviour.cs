using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform Target;
    public float damage = 10;
    public float speed = 2f;
    public Hero hero;
    private NavMeshAgent _agent => GetComponent<NavMeshAgent>();
    private void Start()
    {
        _agent.SetDestination(Target.position);
        _agent.speed = speed;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, Target.position) < 0.5f) 
            hero.GetDamage(damage);
    }
}
