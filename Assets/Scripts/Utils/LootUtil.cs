using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class LootUtil
{
    public static void GiveLootFromTable(LootTableEntry[] possibleLoot, PlayerInventory inventory, int goldMin = 0, int goldMax = 0)
    {
        string toPrint = "Found following loot:\n";

        List<Item> lootToGive = new List<Item>();

        foreach (var item in possibleLoot)
        {
            if (RNGUtil.Roll(item.Chance)) //check if loot will drop
            {
                //loot has dropped. now determine the quantity of that item
                int amount = Random.Range(item.AmountMin, item.AmountMax + 1);

                for (int i = 0; i < amount; i++)
                {
                    Item toAdd = ItemDatabase.Instance.GetItemByID(item.ID);
                    if(toAdd == null)
                    {
                        Debug.LogError("Tried to give player loot with ID " + item.ID + ". This does not exist in item database");
                        return;
                    }
                    lootToGive.Add(toAdd);
                    toPrint += toAdd.ItemName + "\n";
                }
            }
        }

        //add them to inventory
        foreach (var item in lootToGive)
        {
            inventory.AddItemToInventory(item);
        }

        if(goldMax == 0) //if the max gold to get is 0, return
        {
            return;
        }

        //determine how much gold to give
        int amountOfGold = Random.Range(goldMin, goldMax + 1);
        inventory.AddGold(amountOfGold);
        toPrint += amountOfGold + " gold";
        Debug.Log(toPrint);
    }
}