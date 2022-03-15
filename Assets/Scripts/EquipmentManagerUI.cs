using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManagerUI : MonoBehaviour
{
    //TODO tooltip when hovering over the slots

    bool isInitialized = false;

    [SerializeField]
    EquipmentUISlot[] slots = new EquipmentUISlot[10]; //10 equiment slots in total

    public Sprite unequippedSprite;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Init());
    }

    private void OnDestroy()
    {
        if (isInitialized)
            UnsubscribeToEvents();
    }

    private void UpdateAddedEquipment(Equipment equipment)
    {
        int index = FindSlotIndexByType(equipment.EquipmentType);
        if (index == -1)
        {
            Debug.LogError("Could not find the correct item type for slot in equipment UI manager");
            return;
        }

        slots[index].SetImageSprite(Resources.Load<Sprite>(equipment.ImagePath));
        slots[index].equipmentID = equipment.ID;
    }

    private void UpdateRemovedEquipment(Equipment equipment)
    {
        int index = FindSlotIndexByType(equipment.EquipmentType);
        if (index == -1)
        {
            Debug.LogError("Could not find the correct item type for slot in equipment UI manager");
            return;
        }

        slots[index].SetImageSprite(unequippedSprite);
        slots[index].equipmentID = -1;
    }
    void SubscribeToEvents()
    {
        PlayerManager.Instance.PlayerEquipment.onEquipmentAdded += UpdateAddedEquipment;
        PlayerManager.Instance.PlayerEquipment.onEquipmentRemoved += UpdateRemovedEquipment;
    }

    void UnsubscribeToEvents()
    {
        PlayerManager.Instance.PlayerEquipment.onEquipmentAdded -= UpdateAddedEquipment;
        PlayerManager.Instance.PlayerEquipment.onEquipmentRemoved -= UpdateRemovedEquipment;
    }

    IEnumerator Init()
    {
        new WaitForSeconds(0.1f);

        RefreshUI();
        SubscribeToEvents();

        yield return null;
    }

    void RefreshUI()
    {
        var eqSlots = PlayerManager.Instance.PlayerEquipment.EquipmentSlots;

        //get the correct equipment type index of the ui slots and assign its equipment type to it
        for (int i = 0; i < eqSlots.Length; i++)
        {
            if (eqSlots[i].equipment == null)
            {
                Debug.Log($"slots type: {slots[i].equipmentType}, eqslot type: {eqSlots[i].equipmentType}");
                Debug.Log("Setting image for slot " + slots[i].name + " to default");
                slots[i].SetImageSprite(unequippedSprite);
                slots[i].equipmentID = -1;
            }
            else
            {
                Debug.Log($"slots type: {slots[i].equipmentType}, eqslot type: {eqSlots[i].equipmentType}");
                Debug.Log("Setting image for slot " + slots[i].name + " item sprite");
                slots[i].SetImageSprite(Resources.Load<Sprite>(eqSlots[i].equipment.ImagePath));
                slots[i].equipmentID = eqSlots[i].equipment.ID;
            }

        }
        isInitialized = true;
    }

    int FindSlotIndexByType(EquipmentType type)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].equipmentType == type)
            {
                return i;
            }
        }

        return -1;
    }
}
