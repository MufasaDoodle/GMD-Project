using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    #region Properties

    private int currentHealth;

    public int CurrentHealth
    {
        get { return currentHealth; }
        private set { currentHealth = value; }
    }

    private int maxHealth;

    public int MaxHealth
    {
        get { return maxHealth; }
        private set { maxHealth = value; }
    }

    private int currentXP;

    public int CurrentXP
    {
        get { return currentXP; }
        private set { currentXP = value; }
    }

    private int xpToLevel;

    public int XPToLevel
    {
        get { return xpToLevel; }
        private set { xpToLevel = value; }
    }

    private Stat stamina;

    public Stat Stamina
    {
        get { return stamina; }
        private set { stamina = value; }
    }

    private Stat strength;

    public Stat Strength
    {
        get { return strength; }
        private set { strength = value; }
    }

    private Stat agility;

    public Stat Agility
    {
        get { return agility; }
        private set { agility = value; }
    }

    private int level;

    public int Level
    {
        get { return level; }
        set { level = value; }
    }

    private Stat attackPower;

    public Stat AttackPower
    {
        get { return attackPower; }
        private set { attackPower = value; }
    }

    private Stat armor;

    public Stat Armor
    {
        get { return armor; }
        set { armor = value; }
    }

    private Stat blockChance;

    public Stat BlockChance
    {
        get { return blockChance; }
        set { blockChance = value; }
    }


    private float critChance;

    public float CritChance
    {
        get { return critChance; }
        set { critChance = value; }
    }



    #endregion

    public delegate void OnStatChange();
    public OnStatChange onStatChange;

    public delegate void OnHealthChange(int current, int max);
    public OnHealthChange onHealthChange;

    private float healthRegenTimer = 3f;

    // Start is called before the first frame update
    void Awake()
    {
        Stamina = new Stat();
        Strength = new Stat();
        Agility = new Stat();
        AttackPower = new Stat();
        Armor = new Stat();
        BlockChance = new Stat();

        //later we load this initial char data from a savefile
        Level = 1;
        Stamina.rawValue = 2;
        Strength.rawValue = 3;
        Agility.rawValue = 3;
        MaxHealth = Stamina.Value * 10; //every stamina is worth 10 health
        CurrentHealth = MaxHealth;
        CurrentXP = 0;
        XPToLevel = FormulaHelper.CalculateXP(Level);
        GetComponent<PlayerEquipment>().onEquipmentAdded += EquipmentAdded;
        GetComponent<PlayerEquipment>().onEquipmentRemoved += EquipmentRemoved;
    }

    private void Update()
    {
        if (currentHealth != maxHealth)
        {
            healthRegenTimer -= Time.deltaTime;
            healthRegenTimer = Mathf.Clamp(healthRegenTimer, 0f, 3f); //ensure timer doesn't go below zero

            if (healthRegenTimer == 0f)
            {
                HealHealth(maxHealth / 12);
                healthRegenTimer = 3f;
            }
        }

        //For debugging
        if (Input.GetKeyDown(KeyCode.H))
        {
            HealHealth(1);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(1);
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            AddXP(10000);
        }
    }

    public void TakeDamage(int damageAmount)
    {
        CurrentHealth -= damageAmount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth); //ensures health cannot go below zero;
        //Update UI
        HealthChanged();

        //Debug.Log(CurrentHealth);

        if (CurrentHealth == 0)
        {
            Death();
        }
    }

    public void HealHealth(int healAmount, bool postInChat = false)
    {
        if (postInChat)
            ChatLog.Instance.AddEntry($"<color=green>Healed for <color=white>{healAmount}</color></color>");
        CurrentHealth += healAmount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth); //ensures health cannot go above maxHealth
        //Update UI
        HealthChanged();

        if (currentHealth == maxHealth)
        {
            healthRegenTimer = 3f;
        }

        //Debug.Log(CurrentHealth);
    }

    public void AddXP(int amount)
    {
        CurrentXP += amount;

        while (CurrentXP >= XPToLevel)
        {
            CurrentXP -= XPToLevel;
            LevelUp();
            XPToLevel = FormulaHelper.CalculateXP(Level);
        }
        ChatLog.Instance.AddEntry($"<color=purple>Recieved <color=white>{amount}</color> XP</color>");
        //Update UI
        HealthChanged();
        PublishStats();
    }

    private void LevelUp()
    {
        ChatLog.Instance.AddEntry("<color=green>Levelled up!</color>");
        Level += 1;
        Stamina.rawValue += 2;
        Strength.rawValue += 3;
        Agility.rawValue += 2;
        MaxHealth = Stamina.Value * 10; //every stamina is worth 10 health
        CurrentHealth = MaxHealth;
        RecalculateStats();
    }

    void PublishStats()
    {
        onStatChange?.Invoke();
    }

    void HealthChanged()
    {
        onHealthChange?.Invoke(CurrentHealth, MaxHealth);
    }

    void EquipmentAdded(Equipment equipment)
    {
        foreach (var eqStat in equipment.equipmentStats)
        {
            if (eqStat.statType == StatType.Stamina)
            {
                Stamina.modifiers.Add(eqStat.statValue);
                RecalculateHealth();
            }
            else if (eqStat.statType == StatType.Strength)
            {
                Strength.modifiers.Add(eqStat.statValue);
            }
            else if (eqStat.statType == StatType.Agility)
            {
                Agility.modifiers.Add(eqStat.statValue);
            }
            else if (eqStat.statType == StatType.AttackPower)
            {
                AttackPower.modifiers.Add(eqStat.statValue);
            }
            else if (eqStat.statType == StatType.Armor)
            {
                Armor.modifiers.Add(eqStat.statValue);
            }
            else if (eqStat.statType == StatType.BlockChance)
            {
                BlockChance.modifiers.Add(eqStat.statValue);
            }
            else if(eqStat.statType == StatType.CritChance)
            {
                CritChance += eqStat.statValue;
            }
            else
            {
                Debug.LogError("Stat type not defined in player character");
            }

            RecalculateStats();
            HealthChanged();
            PublishStats();
        }
    }

    void EquipmentRemoved(Equipment equipment)
    {
        foreach (var eqStat in equipment.equipmentStats)
        {
            if (eqStat.statType == StatType.Stamina)
            {
                Stamina.modifiers.Remove(eqStat.statValue);
                RecalculateHealth();
            }
            else if (eqStat.statType == StatType.Strength)
            {
                Strength.modifiers.Remove(eqStat.statValue);
            }
            else if (eqStat.statType == StatType.Agility)
            {
                Agility.modifiers.Remove(eqStat.statValue);
            }
            else if (eqStat.statType == StatType.AttackPower)
            {
                AttackPower.modifiers.Remove(eqStat.statValue);
            }
            else if (eqStat.statType == StatType.Armor)
            {
                Armor.modifiers.Remove(eqStat.statValue);
            }
            else if (eqStat.statType == StatType.BlockChance)
            {
                BlockChance.modifiers.Remove(eqStat.statValue);
            }
            else if (eqStat.statType == StatType.CritChance)
            {
                CritChance -= eqStat.statValue;
            }
            else
            {
                Debug.LogError("Stat type not defined in player character");
            }

            RecalculateStats();
            HealthChanged();
            PublishStats();
        }
        Debug.Log("Equipment change registered");
    }

    void RecalculateStats()
    {
        AttackPower.rawValue = Strength.Value / 4;
        CritChance = DamageUtils.CalculateCritChance(Level, Agility.Value);
    }

    void RecalculateHealth()
    {
        MaxHealth = Stamina.Value * 10;
    }

    private void Death()
    {
        ChatLog.Instance.AddEntry("<color=red>Dead</color>");
    }
}
