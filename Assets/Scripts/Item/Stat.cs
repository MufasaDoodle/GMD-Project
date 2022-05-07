using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Stat
{
    /// <summary>
    /// The raw stat value gained from character levels
    /// </summary>
    public int rawValue = 0;

    /// <summary>
    /// Modifiers from armor, potions etc that adds on the existing raw value from general character stats gained from levels
    /// </summary>
    public List<int> modifiers = new List<int>();

    /// <summary>
    /// Gets the value of the stat. Includes modifiers
    /// </summary>
    public int Value
    {
        get
        {
            return GetTotalValue();
        }

        private set
        {
            Value = value;
        }
    }

    private int GetTotalValue()
    {
        int total = rawValue;
        foreach (var mod in modifiers)
        {
            total += mod;
        }

        return total;
    }
}

public enum StatType { Stamina, Strength, Agility, AttackPower, CritChance, Armor, BlockChance }
