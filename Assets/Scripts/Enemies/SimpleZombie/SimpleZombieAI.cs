using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleZombieAI : MonoBehaviour
{

    [Header("Anim/NavMesh")]
    public NavMeshAgent agent;
    private SimpleZombieAnim animManager;

    [Header("Player")]
    public Transform player;
    public LayerMask playerLM;

    [Header("Attack")]
    private bool attacked;
    public float attackCooldown;
    public bool playerInAttackRange;
    public float attackRange;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animManager = GetComponent<SimpleZombieAnim>();
    }

    private void Update()
    {
        if (!GetComponent<SimpleZombieAnim>().Dying)
        {
            if (player != null)
            {
                playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLM);
                if (!playerInAttackRange) { ChasePlayer(); }
                if (playerInAttackRange) { AttackPlayer(); }
            }
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        animManager.Chasing = true;
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        animManager.Chasing = false;
        transform.LookAt(player);

        if (!attacked)
        {
            attacked = true;
            animManager.Attacking = true;
            Invoke(nameof(ApplyAttackCooldown), attackCooldown);
        }
        else { animManager.Attacking = false; }
    }

    private void ApplyAttackCooldown()
    {
        attacked = false;
    }

}
