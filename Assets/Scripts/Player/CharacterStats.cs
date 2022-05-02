using System;
using System.Collections;
using System.Collections.Generic;
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
        if(currentHealth != maxHealth)
        {
            healthRegenTimer -= Time.deltaTime;
            healthRegenTimer = Mathf.Clamp(healthRegenTimer, 0f, 3f); //ensure timer doesn't go below zero

            if(healthRegenTimer == 0f)
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

    public void HealHealth(int healAmount)
    {
        CurrentHealth += healAmount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth); //ensures health cannot go above maxHealth
        //Update UI
        HealthChanged();

        if(currentHealth == maxHealth)
        {
            healthRegenTimer = 3f;
        }

        //Debug.Log(CurrentHealth);
    }

    public void AddXP(int amount)
    {
        CurrentXP += amount;

        while(CurrentXP >= XPToLevel)
        {
            CurrentXP -= XPToLevel;
            LevelUp();
            XPToLevel = FormulaHelper.CalculateXP(Level);
        }

        //Update UI
        HealthChanged();
        PublishStats();
    }

    private void LevelUp()
    {
        Level += 1;
        Stamina.rawValue += 2;
        Strength.rawValue += 3;
        Agility.rawValue += 2;
        MaxHealth = Stamina.Value * 10; //every stamina is worth 10 health
        CurrentHealth = MaxHealth;
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
            if(eqStat.statType == StatType.Stamina)
            {
                Stamina.modifiers.Add(eqStat.statValue);
                RecalculateHealth();
            }
            else if(eqStat.statType == StatType.Strength)
            {
                Strength.modifiers.Add(eqStat.statValue);
            }
            else if(eqStat.statType == StatType.Agility)
            {
                Agility.modifiers.Add(eqStat.statValue);
            }
            else
            {
                Debug.LogError("Stat type not defined in player character");
            }

            //TODO recalculateStats();
            HealthChanged();
            PublishStats();
        }
        Debug.Log("Equipment change registered");
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
            else
            {
                Debug.LogError("Stat type not defined in player character");
            }

            //TODO recalculateStats(); //for other stats not yet implemented, like crit, armor
            HealthChanged();
            PublishStats();
        }
        Debug.Log("Equipment change registered");
    }

    void RecalculateHealth()
    {
        MaxHealth = Stamina.Value * 10;
    }

    private void Death()
    {
        Debug.Log("Dead");
    }
}
