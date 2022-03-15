using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject characterStatsPanel;
    public GameObject equipmentPanel;
    public GameObject inventoryPanel;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        characterStatsPanel.SetActive(false);
        equipmentPanel.SetActive(false);
        inventoryPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            characterStatsPanel.SetActive(!characterStatsPanel.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            equipmentPanel.SetActive(!equipmentPanel.activeSelf);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }
    }
}