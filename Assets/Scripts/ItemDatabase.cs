using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance;

    private List<Item> items;

    public List<Item> Items
    {
        get { return items; }
        private set { items = value; }
    }

    /// <summary>
    /// Gets the item with the specified ID. Returns null if no items found
    /// </summary>
    /// <param name="itemID"></param>
    /// <returns></returns>
    public Item GetItemByID(int itemID)
    {
        foreach (var item in Items)
        {
            if(item.ID == itemID)
            {
                return item;
            }
        }
        return null;
    }

    public void AddItemToDatabase(Item item)
    {
        if(GetItemByID(item.ID) != null)
        {
            Debug.LogError($"Item with ID {item.ID} already exists in database");
            return;
        }

        Items.Add(item);
    }

    private void Awake()
    {
        Instance = this;
        items = new List<Item>();

        CreateAndAddTestItems();
    }

    void CreateAndAddTestItems()
    {
        Equipment weapon = new Equipment();
        weapon.ItemName = "Used Sword";
        weapon.ItemRarity = ItemRarity.Common;
        weapon.EquipmentType = EquipmentType.Weapon;
        weapon.LevelRequirement = 1;
        weapon.ID = 1;
        weapon.ItemType = ItemType.Equipment;
        weapon.SellPrice = 3;
        weapon.AddStat(new EquipmentStat { statType = StatType.Stamina, statValue = 1 });
        weapon.AddStat(new EquipmentStat { statType = StatType.Strength, statValue = 1 });
        weapon.AddStat(new EquipmentStat { statType = StatType.Agility, statValue = 1 });

        Equipment ring1 = new Equipment
        {
            ItemName = "ring 1",
            ItemRarity = ItemRarity.Common,
            EquipmentType = EquipmentType.Ring,
            LevelRequirement = 1,
            ID = 2,
            ItemType = ItemType.Equipment
        };
        ring1.AddStat(new EquipmentStat { statType = StatType.Strength, statValue = 2 });

        Equipment earring = new Equipment
        {
            ItemName = "Earring",
            ItemRarity = ItemRarity.Common,
            EquipmentType = EquipmentType.Earring,
            LevelRequirement = 1,
            ID = 3,
            ItemType = ItemType.Equipment
        };
        earring.AddStat(new EquipmentStat { statType = StatType.Stamina, statValue = 2 });


        Trash ruinedPelt = new Trash { ID = 4, ItemName = "Ruined Pelt", ItemRarity = ItemRarity.Common, ItemType = ItemType.Trash, MarketPrice = 0, SellPrice = 1 };
        Trash brokenTusk = new Trash { ID = 5, ItemName = "Broken Tusk", ItemRarity = ItemRarity.Common, ItemType = ItemType.Trash, MarketPrice = 0, SellPrice = 1 };

        AddItemToDatabase(weapon);
        AddItemToDatabase(ring1);
        AddItemToDatabase(earring);
        AddItemToDatabase(ruinedPelt);
        AddItemToDatabase(brokenTusk);
    }
}
