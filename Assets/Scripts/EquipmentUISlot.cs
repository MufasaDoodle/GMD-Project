using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentUISlot : MonoBehaviour
{
    public EquipmentType equipmentType;

    public void SetImageSprite(Sprite sprite)
    {
        GetComponent<Image>().sprite = sprite;
    }
}
