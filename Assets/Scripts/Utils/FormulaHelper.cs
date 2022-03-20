using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FormulaHelper
{
    public static int CalculateXP(int currentLevel)
    {
        int diffMod = GetDiffMod(currentLevel);
        int newXP = ((8 * currentLevel) + diffMod) * (45 + (5 * currentLevel));
        newXP = (newXP + 50) / 100 * 100; //rounding to nearest hundred

        return newXP;
    }

    private static int GetDiffMod(int level)
    {
        int diffMod = 0;
        if (level <= 28) diffMod = 0;
        if (level == 29) diffMod = 1;
        if (level == 30) diffMod = 3;
        if (level == 31) diffMod = 6;
        if (level >= 32) diffMod = 5 * (level - 30);

        return diffMod;
    }

}
