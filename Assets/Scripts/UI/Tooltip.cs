using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    public static Tooltip Instance;

    public GameObject tooltipPanel;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        tooltipPanel.SetActive(false);
    }

    public void DisplayItemTooltip(Item item)
    {
        TextMeshProUGUI[] textFields = GetComponentsInChildren<TextMeshProUGUI>(true);

        textFields[0].text = item.ItemName;
        textFields[1].text = "Vendor price: " + item.SellPrice.ToString() + " gold";
        textFields[2].text = item.ItemRarity.ToString();
        textFields[3].text = item.ItemType.ToString();

        if (item.GetType() == typeof(Equipment))
        {
            EnableEquipmentObjects(textFields);
            Equipment temp = (Equipment)item;
            textFields[4].text = "Type: " + temp.EquipmentType.ToString();
            string mods = "";
            foreach (var mod in temp.equipmentStats)
            {
                mods += $"    {mod.statType} +{mod.statValue}\n";
            }
            textFields[5].text = mods;
            textFields[6].text = "Requires level " + temp.LevelRequirement.ToString();
        }
        else
        {
            DisableEquipmentObjects(textFields);
        }

        tooltipPanel.SetActive(true);
        //Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        
        Vector2 mousePos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(GetComponentInParent<Canvas>().transform as RectTransform, Input.mousePosition, GetComponentInParent<Canvas>().worldCamera, out mousePos);
        mousePos.y -= 50;
        transform.position = GetComponentInParent<Canvas>().transform.TransformPoint(mousePos);

        //Vector3 mousePosTemp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Vector2 mousePos = new Vector2(mousePosTemp.x, mousePosTemp.y);
        //tooltipPanel.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge.anchoredPosition = mousePos;
        //LayoutRebuilder.MarkLayoutForRebuild(tooltipPanel.GetComponent<RectTransform>());
    }


    void DisableEquipmentObjects(TextMeshProUGUI[] textFields)
    {
        textFields[4].gameObject.SetActive(false);
        textFields[5].gameObject.SetActive(false);
        textFields[6].gameObject.SetActive(false);
    }

    void EnableEquipmentObjects(TextMeshProUGUI[] textFields)
    {
        textFields[4].gameObject.SetActive(true);
        textFields[5].gameObject.SetActive(true);
        textFields[6].gameObject.SetActive(true);
    }
}
