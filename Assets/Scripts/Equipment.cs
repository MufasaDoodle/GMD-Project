using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment
{

    #region Properties

    private string equipmentName;

    private EquipmentType equipmentType;

    private int levelRequirement;

    private EquipmentRarity equipmentRarity;

    public EquipmentRarity EquipmentRarity
    {
        get { return equipmentRarity; }
        set { equipmentRarity = value; }
    }

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

    public string EquipmentName
    {
        get { return equipmentName; }
        set { equipmentName = value; }
    }

    #endregion

    public List<EquipmentStat> equipmentStats = new List<EquipmentStat>();

    public string imagePath = "Item Art/Equipment/Placeholder";

    public void AddStat(EquipmentStat stat)
    {
        equipmentStats.Add(stat);
    }

}
public enum EquipmentType { Weapon, Head, Body, Legs, Feet, Offhand, Earring, Necklace, Bracelet, Ring }

public enum EquipmentRarity { Common, Uncommon, Rare, Epic, Legendary }
