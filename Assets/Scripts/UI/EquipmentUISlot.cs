using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentUISlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public EquipmentType equipmentType;
    public int equipmentID;

    public void SetImageSprite(Sprite sprite)
    {
        GetComponent<Image>().sprite = sprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (equipmentID == -1) return;

        Tooltip.Instance.DisplayItemTooltip(ItemDatabase.Instance.GetItemByID(equipmentID));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.Instance.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (equipmentID == -1) return;

        PlayerManager.Instance.PlayerEquipment.UnequipItem(equipmentType);
    }
}
