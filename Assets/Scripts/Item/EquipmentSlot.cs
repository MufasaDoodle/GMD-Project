using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot
{
    public EquipmentType equipmentType;
    public Equipment equipment;

    public EquipmentSlot()
    {
        equipmentType = EquipmentType.Weapon;
        equipment = null;
    }
}
