using System.Collections;
using System.Collections.Generic;

public abstract class Item
{
    #region Properties

    private int id;

    public int ID
    {
        get { return id; }
        set { id = value; }
    }


    private ItemRarity itemRarity;

    public ItemRarity ItemRarity
    {
        get { return itemRarity; }
        set { itemRarity = value; }
    }

    private ItemType itemType;

    public ItemType ItemType
    {
        get { return itemType; }
        set { itemType = value; }
    }

    private string itemName;

    public string ItemName
    {
        get { return itemName; }
        set { itemName = value; }
    }

    private int sellPrice;

    public int SellPrice
    {
        get { return sellPrice; }
        set { sellPrice = value; }
    }

    private int marketPrice;

    public int MarketPrice
    {
        get { return marketPrice; }
        set { marketPrice = value; }
    }

    private string imagePath = "Item Art/Equipment/Placeholder";

    public string ImagePath
    {
        get { return imagePath; }
        set { imagePath = value; }
    }


    #endregion
}

public enum ItemType { Trash, Armor, Weapon, Accessory, CraftingMaterial, Consumable }
public enum ItemRarity { Common, Uncommon, Rare, Epic, Legendary }
