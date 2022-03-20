using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEventHandler : MonoBehaviour
{

    public void SetAttackingBool(int state)
    {
        GetComponentInParent<Animator>().SetBool("isAttacking", Convert.ToBoolean(state));
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Enemy")
        {
            return;
        }

        //collision.attachedRigidbody.AddRelativeForce(new Vector2(0, -200));
        collision.GetComponent<EnemyController>().TakeDamage(GetComponentInParent<CharacterStats>().Strength.Value, 20, GetComponentInParent<Transform>());
    }
}
