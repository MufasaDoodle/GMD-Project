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

    //where the actual attack gets executed
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Enemy")
        {
            return;
        }

        collision.GetComponent<EnemyCombat>().TakeDamage(GetComponentInParent<CharacterStats>().AttackPower.Value, 20, GetComponentInParent<Transform>());
    }
}
