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

    public static float CalculateCritChance(int level, int totalAgility)
    {
        return 5f + (totalAgility / (level * 1.3f)); 
    }
}

