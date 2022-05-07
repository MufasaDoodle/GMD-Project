using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RNGUtil
{
    /// <summary>
    /// Rolls the chance against a randomized number between 0 and 100. Returns true if percentChance is equal to or higher than random number
    /// </summary>
    /// <param name="percentChance"></param>
    /// <returns></returns>
    public static bool Roll(float percentChance)
    {
        float random = Random.Range(0, 100);
        if (percentChance >= random)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
