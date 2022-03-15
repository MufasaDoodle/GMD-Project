using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject characterStatsPanel;
    public GameObject equipmentPanel;
    // Start is called before the first frame update
    void Start()
    {
        characterStatsPanel.SetActive(false);
        equipmentPanel.SetActive(false);
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
    }
}
