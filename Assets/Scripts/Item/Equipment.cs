using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Equipment : Item
{

    #region Properties

    [SerializeField]
    private EquipmentType equipmentType;

    [SerializeField]
    private int levelRequirement;

    public int LevelRequirement
    {
        get { return levelRequirement; }
        set { levelRequirement = value; }
    }

    public EquipmentType EquipmentType
    {
        get { return equipmentType; }
        set { equipmentType = value; }
    }

    #endregion

    public List<EquipmentStat> equipmentStats = new List<EquipmentStat>();


    public Equipment(int iD, ItemRarity itemRarity, ItemType itemType, string itemName, int sellPrice, int marketPrice, string imagePath) : base(iD, itemRarity, itemType, itemName, sellPrice, marketPrice, imagePath)
    {
        contextOptions.Add("Equip");
    }

    public void AddStat(EquipmentStat stat)
    {
        equipmentStats.Add(stat);
    }

}
public enum EquipmentType { Weapon, Head, Body, Legs, Feet, Offhand, Earring, Necklace, Bracelet, Ring }

