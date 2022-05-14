using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private InventorySlot[] inventorySlots;

    public InventorySlot[] InventorySlots
    {
        get { return inventorySlots; }
        private set { inventorySlots = value; }
    }

    private int gold;

    public int Gold
    {
        get { return gold; }
        set { gold = value; }
    }


    public delegate void OnInventoryChanged(InventorySlot[] inventorySlots);
    public OnInventoryChanged onInventoryChanged;

    public delegate void OnCurrencyChanged(int newGold);
    public OnCurrencyChanged onCurrencyChanged;


    // Start is called before the first frame update
    void Awake()
    {
        InventorySlots = new InventorySlot[30];
        for (int i = 0; i < InventorySlots.Length; i++)
        {
            InventorySlots[i] = new InventorySlot();
        }
    }

    private void Start()
    {
        AddGold(10);
    }

    public void AddGold(int amount)
    {
        if (amount < 1)
        {
            Debug.LogError("Tried to add gold amount: " + amount); return;
        }
        Gold += amount;
        onCurrencyChanged?.Invoke(Gold);
        ChatLog.Instance.AddEntry($"<color=green>Added <color=white>{amount}</color> gold</color>");
    }

    public void RemoveGold(int amount)
    {
        if (amount < 1)
        {
            Debug.LogError("Tried to remove gold amount: " + amount); return;
        }
        Gold -= amount;
        onCurrencyChanged?.Invoke(Gold);
        ChatLog.Instance.AddEntry($"<color=red>Removed <color=orange>{amount}</color> gold</color>");
    }

    /// <summary>
    /// Gets the item at the requested index. If no item is at index, returns null
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public Item GetItemAtIndex(int index)
    {
        if (index > InventorySlots.Length || index < 0)
        {
            Debug.LogError("Invalid index");
            return null;
        }

        if (inventorySlots[index].item == null) return null;

        return InventorySlots[index].item;
    }

    /// <summary>
    /// Gets the index of the first empty inventory slot. Returns -1 if no slot available
    /// </summary>
    /// <returns></returns>
    public int GetIndexOfFirstEmptySlot()
    {
        for (int i = 0; i < InventorySlots.Length; i++)
        {
            if (InventorySlots[i].item == null)
            {
                return i;
            }
        }

        return -1;
    }

    public void UseItemInInventory(string command, int inventorySlotID)
    {
        Item itemToUse = GetItemAtIndex(inventorySlotID);

        if (command == "Drop")
        {
            RemoveItemFromInventoryWithIndex(inventorySlotID, true);
        }
        else if (command == "Use")
        {
            if (itemToUse.GetType() == typeof(Consumable))
            {
                UseConsumable((Consumable)itemToUse);
                RemoveItemFromInventoryWithIndex(inventorySlotID);
            }
        }
        else if(command == "Equip")
        {
            if (itemToUse.GetType() == typeof(Equipment))
            {
                PlayerManager.Instance.PlayerEquipment.EquipItem((Equipment)itemToUse);
                RemoveItemFromInventoryWithIndex(inventorySlotID);
            }
        }
        else if(command == "Sell")
        {
            SellItem(inventorySlotID);
        }
        else
        {
            Debug.LogError($"Unrecognized context command: {command}");
        }

    }

    public void AddItemToInventory(Item item)
    {
        int availableIndex = GetIndexOfFirstEmptySlot();

        if (availableIndex == -1)
        {
            ChatLog.Instance.AddEntry($"Inventory full. Discarding {item.ItemName}");
            return;
        }

        InventorySlots[availableIndex].item = item;
        onInventoryChanged?.Invoke(inventorySlots);
        ChatLog.Instance.AddEntry($"<color=green>Added item <color=orange>{item.ItemName}</color> to inventory</color>");
    }

    public void RemoveItemFromInventoryWithIndex(int index, bool postInChatLog = false)
    {
        if (index > InventorySlots.Length || index < 0)
        {
            Debug.LogError("Invalid index");
            return;
        }

        if (postInChatLog)
            ChatLog.Instance.AddEntry($"<color=red>Removed item <color=orange>{InventorySlots[index].item.ItemName}</color> from inventory</color>");
        InventorySlots[index].item = null;
        onInventoryChanged?.Invoke(inventorySlots);
    }

    public void RemoveItemFromInventoryWithID(int id)
    {
        int index = FindIndexOfSlotByItemID(id);

        if (index == -1) return; //TODO give user a notification that no such item exists in inventory

        RemoveItemFromInventoryWithIndex(index);
    }

    public int FindIndexOfSlotByItemID(int id)
    {
        for (int i = 0; i < InventorySlots.Length; i++)
        {
            if (InventorySlots[i].item.ID == id)
            {
                return i;
            }
        }

        return -1;
    }

    public void PurchaseItem(Item item)
    {
        if(item.MarketPrice > Gold)
        {
            ChatLog.Instance.AddEntry($"<color=red>Not enough money to buy {item.ItemName}</color>");
            return;
        }

        if(GetIndexOfFirstEmptySlot() == -1)
        {
            ChatLog.Instance.AddEntry($"<color=red>Not enough inventory space to buy {item.ItemName}</color>");
            return;
        }

        RemoveGold(item.MarketPrice);
        AddItemToInventory(item);
    }

    private void UseConsumable(Consumable item)
    {
        if (item.Effect == ConsumableEffect.Heal)
        {
            ChatLog.Instance.AddEntry("Used: " + item.ItemName);
            PlayerManager.Instance.PlayerStats.HealHealth(item.Amount, true);
        }
    }

    private void SellItem(int inventorySlotID)
    {
        if (!PlayerManager.Instance.isInRangeOfShop)
        {
            ChatLog.Instance.AddEntry("You must be at a shop to sell items");
            return;
        }

        Item itemToSell = GetItemAtIndex(inventorySlotID);

        if(itemToSell == null)
        {
            Debug.LogError("No item found at index: " + inventorySlotID);
            return;
        }

        AddGold(itemToSell.SellPrice);
        RemoveItemFromInventoryWithIndex(inventorySlotID);
        ChatLog.Instance.AddEntry($"Sold item {itemToSell.ItemName} for {itemToSell.SellPrice} gold");
    }
}
