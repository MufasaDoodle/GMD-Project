using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public int health = 10;

    private Vector3 startingPos;

    public float visionRadius = 10f;
    public float resetRadius = 15f;

    private Transform target;
    private Vector3 currentDestination;
    NavMeshAgent navAgent;

    public EnemyAIState state = EnemyAIState.Idle;

    public float runSpeed = 3.5f;
    public float walkSpeed = 1f;
    public float resetSpeed = 6f;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;

        navAgent = GetComponent<NavMeshAgent>();
        navAgent.updateRotation = false;
        navAgent.updateUpAxis = false;

        target = PlayerManager.Instance.PlayerStats.transform;
    }

    private void Update()
    {
        if (state == EnemyAIState.Resetting) //enemy will return to starting position before trying to follow a player again
        {
            CheckIfAtStartingPosition();
            return;
        }        

        float distanceToTarget = Vector3.Distance(target.position, transform.position);

        if (distanceToTarget <= visionRadius)
        {
            navAgent.speed = runSpeed;
            navAgent.SetDestination(target.position);
            state = EnemyAIState.Chasing;
        }
        else if (state == EnemyAIState.Chasing && distanceToTarget <= resetRadius)
        {
            navAgent.SetDestination(target.position);
        }
        else if (state == EnemyAIState.Wandering)
        {
            navAgent.speed = walkSpeed;
            CheckIfAtCurrentDestination();
        }
        else if (state == EnemyAIState.Idle)
        {
            state = EnemyAIState.Wandering;
            currentDestination = GetRandomPositionAroundStartingPosition();
            navAgent.SetDestination(currentDestination);
        }
        else
        {
            navAgent.speed = resetSpeed;
            state = EnemyAIState.Resetting;
            currentDestination = startingPos;
            ResetPosition();
        }
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

    public void TakeDamage(int amount, float knockbackPower, Transform hitDir)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }

        float timer = 0;

        while (1f > timer)
        {
            timer += Time.deltaTime;
            Vector2 direction = (hitDir.position - transform.position).normalized;
            GetComponent<Rigidbody2D>().AddForce(-direction * knockbackPower);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
        Gizmos.DrawWireSphere(transform.position, resetRadius);
    }

    public enum EnemyAIState { Idle, Chasing, Resetting, Wandering, Dead}
}
