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

    public delegate void OnInventoryChanged(InventorySlot[] inventorySlots);
    public OnInventoryChanged onInventoryChanged;


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
        Trash ruinedPelt = new Trash { ID = 5, ItemName = "Ruined Pelt", ItemRarity = ItemRarity.Common, ItemType = ItemType.Trash, MarketPrice = 0, SellPrice = 1 };
        Trash brokenTusk = new Trash { ID = 6, ItemName = "Broken Tusk", ItemRarity = ItemRarity.Common, ItemType = ItemType.Trash, MarketPrice = 0, SellPrice = 1 };
        AddItemToInventory(ruinedPelt);
        AddItemToInventory(brokenTusk);
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

    public void AddItemToInventory(Item item)
    {
        int availableIndex = GetIndexOfFirstEmptySlot();

        if (availableIndex == -1) return; //TODO give user a notification that inventory is full

        InventorySlots[availableIndex].item = item;
        onInventoryChanged?.Invoke(inventorySlots);
        Debug.Log($"Added item {item.ItemName} to inventory");
    }

    public void RemoveItemFromInventoryWithIndex(int index)
    {
        if (index > InventorySlots.Length || index < 0)
        {
            Debug.LogError("Invalid index");
            return;
        }

        Debug.Log($"removed item {InventorySlots[index].item.ItemName} from inventory");
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
            if(InventorySlots[i].item.ID == id)
            {
                return i;
            }
        }

        return -1;
    }
}
