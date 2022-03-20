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

    private void Start()
    {
        StartCoroutine(Init());
    }

    private void OnDestroy()
    {
        UnsubscribeToEvents();
    }

    void UpdateUI()
    {
        CharacterStats stats = PlayerManager.Instance.PlayerStats;
        levelUI.text = $"Level: {stats.Level}";
        healthUI.text = $"Health: {stats.CurrentHealth}/{stats.MaxHealth}";
        xpUI.text = $"XP: {stats.CurrentXP}/{stats.XPToLevel}";
        strengthUI.text = $"Strength: {stats.Strength.Value}";
        staminaUI.text = $"Stamina: {stats.Stamina.Value}";
        agilityUI.text = $"Agility: {stats.Agility.Value}";
    }

    void SubscribeToEvents()
    {
        PlayerManager.Instance.PlayerStats.onStatChange += UpdateUI;
        UpdateUI();
    }

    IEnumerator Init()
    {
        new WaitForSeconds(0.1f);

        UpdateUI();
        SubscribeToEvents();

        yield return null;
    }

    void UnsubscribeToEvents()
    {
        PlayerManager.Instance.PlayerStats.onStatChange -= UpdateUI;
    }
}
