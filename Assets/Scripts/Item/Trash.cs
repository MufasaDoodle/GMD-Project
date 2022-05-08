using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Trash : Item
{
    public Trash(int iD, ItemRarity itemRarity, ItemType itemType, string itemName, int sellPrice, int marketPrice, string imagePath) : base(iD, itemRarity, itemType, itemName, sellPrice, marketPrice, imagePath)
    {
    }
}
