using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VendorUI : MonoBehaviour
{
    private VendorItemEntry[] itemsForSale;

    public GameObject scrollviewContent;
    public GameObject vendorEntryPrefab;

    public void Init(VendorItemEntry[] items, Vendor vendor)
    {
        itemsForSale = items;

        for (int i = 0; i < scrollviewContent.transform.childCount; i++)
        {
            GameObject.Destroy(scrollviewContent.transform.GetChild(i).gameObject);
        }

        foreach (var itemForSale in itemsForSale)
        {
            var go = Instantiate(vendorEntryPrefab, scrollviewContent.transform);
            Item itemToSet = ItemDatabase.Instance.GetItemByID(itemForSale.ID);
            go.GetComponent<VendorItemUI>().SetData(itemToSet, vendor);
        }
    }
}
