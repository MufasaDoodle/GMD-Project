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
        var stats = GetComponentInParent<CharacterStats>();

        int damage = 0;
        bool isCrit = false;
        //check if it's a crit
        if (RNGUtil.Roll(stats.CritChance))
        {
            damage = stats.AttackPower.Value * 2;
            isCrit = true;
        }
        else
        {
            damage = stats.AttackPower.Value;
        }

        collision.GetComponent<EnemyCombat>().TakeDamage(damage, 20, isCrit, GetComponentInParent<Transform>());
    }
}
