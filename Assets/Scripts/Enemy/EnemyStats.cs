using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int level = 1;
    public int currentHealth = 10;
    public int maxHealth = 10;
    public int attackDamage = 1;
    public float knockbackPower = 20f;
    public float xpMultiplierFactor = 1.0f;
    public EnemyHealthUI enemyHealthUI;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        enemyHealthUI.SetHealthSlider(currentHealth);
        if (currentHealth <= 0)
        {
            GrantXP();
            GiveLoot();
            Destroy(gameObject);
        }
    }

    private void GiveLoot()
    {
        var lootTable = GetComponent<LootTable>();

        if (lootTable.canDropGold)
        {
            LootUtil.GiveLootFromTable(lootTable.lootEntries, PlayerManager.Instance.PlayerInventory, lootTable.goldDropMin, lootTable.goldDropMax);
        }
        else
        {
            LootUtil.GiveLootFromTable(lootTable.lootEntries, PlayerManager.Instance.PlayerInventory);
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        enemyHealthUI.SetHealthSlider(currentHealth);
    }

    public void EnableHealthBar()
    {
        if(enemyHealthUI.gameObject.activeSelf == true)
        {
            return;
        }
        enemyHealthUI.gameObject.SetActive(true);
    }

    public void DisableHealthBar()
    {
        if (enemyHealthUI.gameObject.activeSelf == false)
        {
            return;
        }
        enemyHealthUI.gameObject.SetActive(false);
    }

    public void GrantXP()
    {
        PlayerManager.Instance.PlayerStats.AddXP(FormulaHelper.CalculateRewardXP(level, xpMultiplierFactor));
    }
}
