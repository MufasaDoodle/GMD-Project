using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    public delegate void OnEquipmentChange(Equipment equipment);
    public OnEquipmentChange onEquipmentAdded;
    public OnEquipmentChange onEquipmentRemoved;


    private EquipmentSlot[] equipmentSlots;

    public EquipmentSlot[] EquipmentSlots
    {
        get { return equipmentSlots; }
        private set { equipmentSlots = value; }
    }

    private void Awake()
    {
        EquipmentSlots = new EquipmentSlot[10];

        for (int i = 0; i < EquipmentSlots.Length; i++)
        {
            EquipmentSlots[i] = new EquipmentSlot();
        }

        EquipmentSlots[0].equipmentType = EquipmentType.Weapon;
        EquipmentSlots[1].equipmentType = EquipmentType.Head;
        EquipmentSlots[2].equipmentType = EquipmentType.Body;
        EquipmentSlots[3].equipmentType = EquipmentType.Legs;
        EquipmentSlots[4].equipmentType = EquipmentType.Feet;
        EquipmentSlots[5].equipmentType = EquipmentType.Offhand;
        EquipmentSlots[6].equipmentType = EquipmentType.Earring;
        EquipmentSlots[7].equipmentType = EquipmentType.Necklace;
        EquipmentSlots[8].equipmentType = EquipmentType.Bracelet;
        EquipmentSlots[9].equipmentType = EquipmentType.Ring;
    }

    private void Start()
    {
        //for debugging
        Equipment weapon = new Equipment();
        weapon.ItemName = "Used Sword";
        weapon.ItemRarity = ItemRarity.Common;
        weapon.EquipmentType = EquipmentType.Weapon;
        weapon.LevelRequirement = 1;
        weapon.ID = 1;
        weapon.ItemType = ItemType.Weapon;
        weapon.AddStat(new EquipmentStat { statType = StatType.Stamina, statValue = 1 });
        EquipItem(weapon);

        Equipment ring1 = new Equipment
        {
            ItemName = "ring 1",
            ItemRarity = ItemRarity.Common,
            EquipmentType = EquipmentType.Ring,
            LevelRequirement = 1,
            ID = 2,
            ItemType = ItemType.Accessory
        };
        ring1.AddStat(new EquipmentStat { statType = StatType.Strength, statValue = 2 });
        EquipItem(ring1);

        Equipment earring = new Equipment
        {
            ItemName = "Earring",
            ItemRarity = ItemRarity.Common,
            EquipmentType = EquipmentType.Earring,
            LevelRequirement = 1,
            ID = 3,
            ItemType = ItemType.Accessory
        };
        earring.AddStat(new EquipmentStat { statType = StatType.Stamina, statValue = 2 });
        EquipItem(earring);
    }

    public Equipment GetEquipmentAtType(EquipmentType equipmentType)
    {
        foreach (var equipmentSlot in equipmentSlots)
        {
            if (equipmentSlot.equipmentType == equipmentType)
            {
                return equipmentSlot.equipment;
            }
        }

        return null;
    }

    public void EquipItem(Equipment equipment)
    {
        foreach (var equipmentSlot in equipmentSlots)
        {
            if (equipmentSlot.equipmentType == equipment.EquipmentType)
            {
                if (equipment.LevelRequirement > GetComponent<CharacterStats>().Level)
                {
                    Debug.Log("Equipment requires a higher level to use");
                    //TODO have a message pop up for the user instead of a debug log
                    return;
                }
                equipmentSlot.equipment = equipment;

                //TODO update equipment UI with this info
                Debug.Log($"Equipped {equipment.ItemName}");
                onEquipmentAdded?.Invoke(equipment);
                return;
            }
        }

        Debug.LogError($"No equipment slot with type {equipment.EquipmentType} existed in the player equipment slot list");
    }

    public void UnequipItem(EquipmentType equipmentType)
    {
        //TODO allow unequipping of item with an ID
        foreach (var equipmentSlot in equipmentSlots)
        {
            if (equipmentSlot.equipmentType == equipmentType)
            {
                Equipment equipmentToRemove = equipmentSlot.equipment;
                onEquipmentRemoved?.Invoke(equipmentToRemove);
                equipmentSlot.equipment = null;
            }
        }
    }
}

