using JsonSubTypes;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[JsonConverter(typeof(JsonSubtypes), "ItemType")]
[JsonSubtypes.KnownSubType(typeof(Equipment), ItemType.Equipment)]
[JsonSubtypes.KnownSubType(typeof(Trash), ItemType.Trash)]
//[JsonSubtypes.KnownSubType(typeof(Equipment), ItemType.CraftingMaterial)]
//[JsonSubtypes.KnownSubType(typeof(Equipment), ItemType.Equipment)]
public abstract class Item
{
    #region Properties

    [SerializeField]
    private int id;
    [SerializeField]
    private ItemRarity itemRarity;
    [SerializeField]
    private ItemType itemType;
    [SerializeField]
    private string itemName;
    [SerializeField]
    private int sellPrice;
    [SerializeField]
    private int marketPrice;
    [SerializeField]
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
