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
    public TextMeshProUGUI armorUI;
    public TextMeshProUGUI critChanceUI;
    public TextMeshProUGUI apUI;
    public TextMeshProUGUI blockChanceUI;

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
        armorUI.text = $"Armor: {stats.Armor.Value}";
        critChanceUI.text = $"Crit Chance: {stats.CritChance.ToString("0.00")}%";
        apUI.text = $"Attack Power: {stats.AttackPower.Value}";
        blockChanceUI.text = $"Block Chance: {stats.BlockChance.Value}";
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
