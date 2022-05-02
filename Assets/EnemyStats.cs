using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int level = 1;
    public int health = 10;
    public int attackDamage = 1;
    public float knockbackPower = 20f;
    public float xpMultiplierFactor = 1.0f;

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            GrantXP();
            Destroy(gameObject);
        }
    }

    public void GrantXP()
    {
        PlayerManager.Instance.PlayerStats.AddXP(FormulaHelper.CalculateRewardXP(level, xpMultiplierFactor));
    }
}
