using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ChatLog : MonoBehaviour
{
    public static ChatLog Instance;

    private List<string> chatEntries;

    public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        chatEntries = new List<string>();
    }

    public void AddEntry(string text)
    {
        if(chatEntries.Count > 250)
        {
            for (int i = 0; i < 100; i++)
            {
                chatEntries.RemoveAt(i);
            }
        }

        chatEntries.Add(text);
        UpdateChatUI();
    }

    private void UpdateChatUI()
    {
        string toSet = "";

        bool firstSet = false;
        foreach (var entry in chatEntries)
        {
            if(firstSet == false)
            {
                toSet += entry;
                firstSet = true;
            }
            else
            {
                toSet += "\n" + entry;
            }
        }

        text.text = toSet;
        LayoutRebuilder.MarkLayoutForRebuild(GetComponent<RectTransform>());
    }
}
