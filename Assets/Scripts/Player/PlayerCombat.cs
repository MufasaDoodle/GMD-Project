using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public GameObject slashGameObject;

    public float swingCooldown = 1.5f;

    public AudioClip[] swingClips;

    public AudioClip takeDamageClip;

    // Update is called once per frame
    void Update()
    {
        swingCooldown -= Time.deltaTime;
        swingCooldown = Mathf.Clamp(swingCooldown, 0f, 2f);

        if (Input.GetMouseButtonDown(0) && swingCooldown <= 0)
        {
            Attack();
            swingCooldown = 1.5f;
        }
    }

    public void TakeDamage(int amount, float knockbackPower, Transform hitDir)
    {
        if(amount >= 0)
        {
            GetComponent<AudioSource>().PlayOneShot(takeDamageClip);
            GetComponent<CharacterStats>().TakeDamage(amount);

            float timer = 0;

            //apply knockback
            while (1f > timer)
            {
                timer += Time.deltaTime;
                Vector2 direction = (hitDir.position - transform.position).normalized;
                GetComponent<Rigidbody2D>().AddForce(-direction * knockbackPower);
            }
        }
    }

    void Attack()
    {
        slashGameObject.GetComponent<Animator>().SetBool("isAttacking", true);
        PlaySwingSound();
    }

    public void SetAttackingBool(bool state)
    {
        GetComponent<Animator>().SetBool("Attacking", state);
    }

    public void PlaySwingSound()
    {
        int random = Random.Range(0, swingClips.Length-1);
        GetComponent<AudioSource>().PlayOneShot(swingClips[random]);
    }
}
