using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ContextMenuUI : MonoBehaviour, IPointerExitHandler
{
    public static ContextMenuUI Instance;

    public GameObject menuItemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void DisplayContextMenu(int inventorySlotID, Item item)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject.Destroy(transform.GetChild(i).gameObject);
        }

        List<string> menuItems = item.contextOptions;

        foreach (var menuItem in menuItems)
        {
            var go = Instantiate(menuItemPrefab, transform);
            var button = go.GetComponent<Button>();
            button.onClick.AddListener(() => OnUseItem(menuItem, inventorySlotID));
            go.GetComponentInChildren<TextMeshProUGUI>(true).text = menuItem;
        }

        gameObject.SetActive(true);

        Vector2 mousePos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponentInParent<Canvas>().transform as RectTransform, Input.mousePosition, GetComponentInParent<Canvas>().worldCamera, out mousePos);
        mousePos.x += 50;
        transform.position = GetComponentInParent<Canvas>().transform.TransformPoint(mousePos);
    }

    void OnUseItem(string command, int inventorySlotID)
    {
        PlayerManager.Instance.PlayerInventory.UseItemInInventory(command, inventorySlotID);
        gameObject.SetActive(false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //if (eventData.pointerCurrentRaycast.gameObject.tag == "MenuItem") return;

        //gameObject.SetActive(false);
    }
}
