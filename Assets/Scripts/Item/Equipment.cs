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


    public void AddStat(EquipmentStat stat)
    {
        equipmentStats.Add(stat);
    }

}
public enum EquipmentType { Weapon, Head, Body, Legs, Feet, Offhand, Earring, Necklace, Bracelet, Ring }

