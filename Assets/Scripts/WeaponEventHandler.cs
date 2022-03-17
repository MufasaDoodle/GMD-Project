using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WeaponEventHandler : MonoBehaviour
{

    public void SetAttackingBool(int state)
    {
        GetComponentInParent<Animator>().SetBool("isAttacking", Convert.ToBoolean(state));
    }
}
