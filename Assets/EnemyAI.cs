using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public EnemyAIState state = EnemyAIState.Idle;

    public float runSpeed = 3.5f;
    public float walkSpeed = 1f;
    public float resetSpeed = 6f;

    public float visionRadius = 10f;
    public float resetRadius = 15f;
    public float attackRadius = 1f;

    private Vector3 startingPos;

    private Transform target;
    private Vector3 currentDestination;
    public NavMeshAgent navAgent;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;

        navAgent = GetComponent<NavMeshAgent>();
        navAgent.updateRotation = false;
        navAgent.updateUpAxis = false;

        target = PlayerManager.Instance.PlayerStats.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (state == EnemyAIState.Resetting) //enemy will return to starting position before trying to follow a player again
        {
            CheckIfAtStartingPosition();
            return;
        }

        float distanceToTarget = Vector3.Distance(target.position, transform.position);

        if(distanceToTarget <= resetRadius)
        {
            GetComponent<EnemyStats>().EnableHealthBar();
        }
        else
        {
            GetComponent<EnemyStats>().DisableHealthBar();
        }

        if (distanceToTarget <= attackRadius && GetComponent<EnemyCombat>().currentAttackCooldown == 0)
        {
            AttackPlayer();
        }

        if (distanceToTarget <= visionRadius) //if player is within vision range, chase
        {
            ChasePlayer();
        }
        else if (state == EnemyAIState.Chasing && distanceToTarget <= resetRadius) //still within range, update target destination
        {
            navAgent.SetDestination(target.position);
        }
        else if (state == EnemyAIState.Wandering) //if the enemy is wandering we just check if we're at the current target destination and pick a new one if we are
        {
            CheckWanderDestination();
        }
        else if (state == EnemyAIState.Idle) //if we are now idle, we go to wandering mode
        {
            StartWandering();
        }
        else
        {
            ResetEnemy();
        }
    }

    private void ResetEnemy()
    {
        navAgent.speed = resetSpeed;
        state = EnemyAIState.Resetting;
        currentDestination = startingPos;
        GetComponent<EnemyStats>().ResetHealth();
        ResetPosition();
    }

    private void StartWandering()
    {
        state = EnemyAIState.Wandering;
        currentDestination = GetRandomPositionAroundStartingPosition();
        navAgent.SetDestination(currentDestination);
    }

    private void CheckWanderDestination()
    {
        navAgent.speed = walkSpeed;
        CheckIfAtCurrentDestination();
    }

    private void ChasePlayer()
    {
        if (state != EnemyAIState.Chasing)
            navAgent.speed = runSpeed; //if we are just starting the chase, set speed to run
        currentDestination = target.position;
        navAgent.SetDestination(target.position);
        state = EnemyAIState.Chasing;
    }

    private void AttackPlayer()
    {
        GetComponent<EnemyCombat>().AttackPlayer(target);
    }

    public void ResetPosition()
    {
        navAgent.SetDestination(startingPos);
    }

    private void CheckIfAtStartingPosition()
    {
        float distanceToStartingPos = Vector3.Distance(startingPos, transform.position);

        if (distanceToStartingPos <= 1.5f) //we have reached starting position
        {
            navAgent.speed = walkSpeed;
            state = EnemyAIState.Idle;
        }
    }

    private void CheckIfAtCurrentDestination()
    {
        float distanceToDestination = Vector3.Distance(currentDestination, transform.position);

        if (distanceToDestination <= 3f) //we have reached the destination
        {
            state = EnemyAIState.Idle;
        }
    }

    private Vector3 GetRandomPositionAroundStartingPosition()
    {
        Vector3 random = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
        return startingPos + random * Random.Range(2f, 7f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
        Gizmos.DrawWireSphere(transform.position, resetRadius);
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    public enum EnemyAIState { Idle, Chasing, Resetting, Wandering, Dead }
}
