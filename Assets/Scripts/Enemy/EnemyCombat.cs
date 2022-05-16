using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public AudioClip onHitSound;

    public float attackCooldown = 2f;

    public float currentAttackCooldown;
    public float takeDamageCooldown = 1.5f;
    private float currentTakeDamageCooldown;

    // Start is called before the first frame update
    void Start()
    {
        currentAttackCooldown = attackCooldown;
        currentTakeDamageCooldown = takeDamageCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        currentAttackCooldown -= Time.deltaTime; 
        
        if (currentAttackCooldown <= 0) //to prevent possible underflow
        {
            currentAttackCooldown = 0;
        }

        currentTakeDamageCooldown -= Time.deltaTime;

        if(currentTakeDamageCooldown <= 0) //to prevent possible underflow
        {
            currentTakeDamageCooldown = 0;
        }
    }

    public void AttackPlayer(Transform target)
    {
        //can't attack if resetting
        if (GetComponent<EnemyAI>().state == EnemyAI.EnemyAIState.Resetting)
        {
            return;
        }

        var stats = GetComponent<EnemyStats>();

        int damage = 0;
        bool isCrit = false;
        if (RNGUtil.Roll(5)) //enemies always has a 5% chance of critting player
        {
            damage = stats.attackDamage * 2;
            isCrit = true;
        }
        else
        {
            damage = stats.attackDamage;
        }

        target.GetComponent<PlayerCombat>().TakeDamage(damage, GetComponent<EnemyStats>().knockbackPower, isCrit, GetComponent<Transform>());
        currentAttackCooldown = attackCooldown;
    }

    public void TakeDamage(int amount, float knockbackPower, bool isCrit, Transform hitDir)
    {
        //can't get hurt if resetting
        if(GetComponent<EnemyAI>().state == EnemyAI.EnemyAIState.Resetting)
        {
            return;
        }

        if(currentTakeDamageCooldown != 0f)
        {
            return;
        }

        PlayerManager.Instance.PlayerEquipment.GetComponent<AudioSource>().PlayOneShot(onHitSound);

        currentTakeDamageCooldown = takeDamageCooldown;
        GetComponent<EnemyStats>().TakeDamage(amount);
        UIManager.Instance.EntityDamageNumber.InstantiateEntityDamage(amount, isCrit, false, transform.position);

        GetComponent<EnemyAI>().navAgent.speed = 0;
        float timer = 0;

        while (1f > timer)
        {
            timer += Time.deltaTime;
            Vector2 direction = (hitDir.position - transform.position).normalized;
            GetComponent<Rigidbody2D>().AddForce(-direction * knockbackPower);
        }
        GetComponent<EnemyAI>().navAgent.speed = GetComponent<EnemyAI>().runSpeed;
    }
}
