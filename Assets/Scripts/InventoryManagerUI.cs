using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManagerUI : MonoBehaviour
{
    public GameObject innerInventoryPanel;

    public InventorySlotUI[] inventorySlots;

    public Sprite emptySlotSprite;

    private bool isInitialized = false;

    // Start is called before the first frame update
    void Start()
    {
        int inventorySlotAmount = innerInventoryPanel.GetComponentsInChildren<InventorySlotUI>().Length;
        inventorySlots = new InventorySlotUI[inventorySlotAmount];

        for (int i = 0; i < inventorySlotAmount; i++)
        {
            inventorySlots[i] = innerInventoryPanel.GetComponentsInChildren<InventorySlotUI>()[i];
        }

        StartCoroutine(Init());
    }

    private void OnDestroy()
    {
        if (isInitialized)
            UnsubscribeToEvents();
    }

    void SubscribeToEvents()
    {
        PlayerManager.Instance.PlayerInventory.onInventoryChanged += RefreshUI;
    }

    void UnsubscribeToEvents()
    {
        PlayerManager.Instance.PlayerInventory.onInventoryChanged -= RefreshUI;
    }

    IEnumerator Init()
    {
        new WaitForSeconds(0.1f);

        RefreshUI(PlayerManager.Instance.PlayerInventory.InventorySlots);
        SubscribeToEvents();

        yield return null;
    }

    void RefreshUI(InventorySlot[] invSlots)
    {
        //get the correct equipment type index of the ui slots and assign its equipment type to it
        for (int i = 0; i < invSlots.Length; i++)
        {
            if (invSlots[i].item == null)
            {
                //Debug.Log("Setting image for slot " + inventorySlots[i].name + " to default");
                inventorySlots[i].SetImageSprite(emptySlotSprite);
                inventorySlots[i].itemID = -1;
            }
            else
            {
                //Debug.Log("Setting image for slot " + inventorySlots[i].name + " item sprite");
                inventorySlots[i].SetImageSprite(Resources.Load<Sprite>(invSlots[i].item.ImagePath));
                inventorySlots[i].itemID = invSlots[i].item.ID;
            }

        }
        isInitialized = true;
    }
}
