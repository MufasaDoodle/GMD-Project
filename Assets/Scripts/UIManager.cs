using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject characterStatsPanel;
    // Start is called before the first frame update
    void Start()
    {
        characterStatsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            characterStatsPanel.SetActive(!characterStatsPanel.activeSelf);
        }
    }
}
