using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Consumable : Item
{
    [SerializeField]
    private ConsumableEffect effect;

    public ConsumableEffect Effect
    {
        get { return effect; }
        set { effect = value; }
    }

    [SerializeField]
    private int amount;

    public int Amount
    {
        get { return amount; }
        set { amount = value; }
    }


    public Consumable(int iD, ItemRarity itemRarity, ItemType itemType, string itemName, int sellPrice, int marketPrice, string imagePath, ConsumableEffect effect, int amount) : base(iD, itemRarity, itemType, itemName, sellPrice, marketPrice, imagePath)
    {
        contextOptions.Add("Use");
        this.Amount = amount;
        this.Effect = effect;
    }
}


public enum ConsumableEffect { Heal, QuestItem, DamageBoost, HealthBoost}
