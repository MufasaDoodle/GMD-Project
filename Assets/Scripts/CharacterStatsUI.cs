using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CharacterStatsUI : MonoBehaviour
{
    public TextMeshProUGUI levelUI;
    public TextMeshProUGUI healthUI;
    public TextMeshProUGUI xpUI;
    public TextMeshProUGUI strengthUI;
    public TextMeshProUGUI staminaUI;
    public TextMeshProUGUI agilityUI;

    private void OnEnable()
    {
        SubscribeToEvents();
    }

    private void OnDisable()
    {
        UnsubscribeToEvents();
    }

    void UpdateUI()
    {
        CharacterStats stats = PlayerManager.Instance.PlayerStats;
        levelUI.text = $"Level: {stats.Level}";
        healthUI.text = $"Health: {stats.CurrentHealth}/{stats.MaxHealth}";
        xpUI.text = $"XP: {stats.CurrentXP}/{stats.XPToLevel}";
        strengthUI.text = $"Strength: {stats.Strength}";
        staminaUI.text = $"Stamina: {stats.Stamina}";
        agilityUI.text = $"Agility: {stats.Agility}";
    }

    void SubscribeToEvents()
    {
        PlayerManager.Instance.PlayerStats.onStatChange += UpdateUI;
        UpdateUI();
    }

    void UnsubscribeToEvents()
    {
        PlayerManager.Instance.PlayerStats.onStatChange -= UpdateUI;
    }
}
