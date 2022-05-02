using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public AudioClip onHitSound;

    public float attackCooldown = 2f;

    public float currentAttackCooldown;

    // Start is called before the first frame update
    void Start()
    {
        currentAttackCooldown = attackCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        currentAttackCooldown -= Time.deltaTime;
        currentAttackCooldown = Mathf.Clamp(currentAttackCooldown, 0, attackCooldown);

    }

    public void AttackPlayer(Transform target)
    {
        target.GetComponent<PlayerCombat>().TakeDamage(GetComponent<EnemyStats>().attackDamage, GetComponent<EnemyStats>().knockbackPower, GetComponent<Transform>());
        currentAttackCooldown = attackCooldown;
    }

    public void TakeDamage(int amount, float knockbackPower, Transform hitDir)
    {
        PlayerManager.Instance.PlayerEquipment.GetComponent<AudioSource>().PlayOneShot(onHitSound);

        GetComponent<EnemyStats>().TakeDamage(amount);

        float timer = 0;

        while (1f > timer)
        {
            timer += Time.deltaTime;
            Vector2 direction = (hitDir.position - transform.position).normalized;
            GetComponent<Rigidbody2D>().AddForce(-direction * knockbackPower);
        }
    }
}
