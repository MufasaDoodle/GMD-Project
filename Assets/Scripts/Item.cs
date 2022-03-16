using System.Collections;
using System.Collections.Generic;

public abstract class Item
{
    #region Properties

    private int id;
    private ItemRarity itemRarity;
    private ItemType itemType;
    private string itemName;
    private int sellPrice;
    private int marketPrice;
    private string imagePath = "Item Art/Equipment/Placeholder";

    public int ID
    {
        get { return id; }
        set { id = value; }
    }

    public ItemRarity ItemRarity
    {
        get { return itemRarity; }
        set { itemRarity = value; }
    }

    public ItemType ItemType
    {
        get { return itemType; }
        set { itemType = value; }
    }

    public string ItemName
    {
        get { return itemName; }
        set { itemName = value; }
    }

    public int SellPrice
    {
        get { return sellPrice; }
        set { sellPrice = value; }
    }

    public int MarketPrice
    {
        get { return marketPrice; }
        set { marketPrice = value; }
    }

    public string ImagePath
    {
        get { return imagePath; }
        set { imagePath = value; }
    }


    #endregion
}

public enum ItemType { Trash, Equipment, CraftingMaterial, Consumable }
public enum ItemRarity { Common, Uncommon, Rare, Epic, Legendary }
