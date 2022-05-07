using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DamageUtils
{
    public static int CalculateDamage(int weaponAttackDamage, int charAttackPower)
    {
        return weaponAttackDamage + charAttackPower;
    }
}

