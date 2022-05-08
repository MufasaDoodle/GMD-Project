using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public int itemID;

    public void SetImageSprite(Sprite sprite)
    {
        GetComponent<Image>().sprite = sprite;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (itemID == -1) return;

        Tooltip.Instance.DisplayItemTooltip(ItemDatabase.Instance.GetItemByID(itemID));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.Instance.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (itemID == -1) return;

        int slotIndex = transform.GetSiblingIndex();
        ContextMenuUI.Instance.DisplayContextMenu(slotIndex, PlayerManager.Instance.PlayerInventory.GetItemAtIndex(slotIndex));
    }
}
