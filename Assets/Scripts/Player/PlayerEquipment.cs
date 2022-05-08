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
        EquipItem(ItemDatabase.Instance.GetItemByID(1) as Equipment);
        EquipItem(ItemDatabase.Instance.GetItemByID(2) as Equipment);
        EquipItem(ItemDatabase.Instance.GetItemByID(3) as Equipment);
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
                    ChatLog.Instance.AddEntry("<color=red>Equipment requires a higher level to use</color>");
                    return;
                }
                equipmentSlot.equipment = equipment;

                ChatLog.Instance.AddEntry($"Equipped {equipment.ItemName}");
                onEquipmentAdded?.Invoke(equipment);
                return;
            }
        }

        Debug.LogError($"No equipment slot with type {equipment.EquipmentType} existed in the player equipment slot list");
    }

    public void UnequipItem(EquipmentType equipmentType)
    {
        foreach (var equipmentSlot in equipmentSlots)
        {
            if (equipmentSlot.equipmentType == equipmentType)
            {
                Equipment equipmentToRemove = equipmentSlot.equipment;
                PlayerManager.Instance.PlayerInventory.AddItemToInventory(equipmentToRemove);
                onEquipmentRemoved?.Invoke(equipmentToRemove);
                equipmentSlot.equipment = null;
            }
        }
    }
}

