using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class VendorItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private int itemID;

    public Image image;
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI price;

    public void SetData(Item item, Vendor vendor)
    {
        itemID = item.ID;
        image.sprite = Resources.Load<Sprite>(item.ImagePath);
        itemName.text = item.ItemName;
        price.text = item.MarketPrice.ToString();
        var button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(() => vendor.PurchaseItem(item));
        LayoutRebuilder.MarkLayoutForRebuild(GetComponent<RectTransform>());
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
}
