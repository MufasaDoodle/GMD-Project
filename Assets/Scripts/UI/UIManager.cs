using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject equipmentPanel;
    public GameObject inventoryPanel;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        equipmentPanel.SetActive(false);
        inventoryPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (equipmentPanel.activeSelf)
            {
                Tooltip.Instance.gameObject.SetActive(false);
            }
            equipmentPanel.SetActive(!equipmentPanel.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryPanel.activeSelf)
            {
                Tooltip.Instance.gameObject.SetActive(false);
            }
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
            Tooltip.Instance.gameObject.SetActive(false);
        }
    }
}
