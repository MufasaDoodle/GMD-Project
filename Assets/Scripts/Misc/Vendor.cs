using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : MonoBehaviour
{
    public VendorItemEntry[] itemsForSale;

    public VendorUI vendorUI;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            return;
        }
        if(vendorUI == null)
        {
            vendorUI = UIManager.Instance.vendorUI.GetComponent<VendorUI>();
        }

        vendorUI.Init(itemsForSale, this);
        ShowShop();
        PlayerManager.Instance.isInRangeOfShop = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CloseShop();
        PlayerManager.Instance.isInRangeOfShop = false;
    }

    public void ShowShop()
    {
        vendorUI.gameObject.SetActive(true);
    }

    public void CloseShop()
    {
        vendorUI.gameObject.SetActive(false);
    }

    public void PurchaseItem(Item itemToBuy)
    {
        int indexOfItemForSale = -1;
        for (int i = 0; i < itemsForSale.Length; i++)
        {
            if(itemToBuy.ID == itemsForSale[i].ID)
            {
                indexOfItemForSale = i;
                break;
            }
        }

        if(indexOfItemForSale == -1)
        {
            Debug.LogError("Item to buy not found at vendor");
            return;
        }

        PlayerManager.Instance.PlayerInventory.PurchaseItem(itemToBuy);
    }
}

[System.Serializable]
public struct VendorItemEntry
{
    [SerializeField]
    private int id;

    public int ID
    {
        get { return id; }
        set { id = value; }
    }

    [SerializeField]
    private int price;

    public int Price
    {
        get { return price; }
        set { price = value; }
    }

    public VendorItemEntry(int iD, int price) : this()
    {
        ID = iD;
        Price = price;
    }
}
