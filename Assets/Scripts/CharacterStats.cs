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

    private int stamina;

    public int Stamina
    {
        get { return stamina; }
        private set { stamina = value; }
    }

    private int strength;

    public int Strength
    {
        get { return strength; }
        private set { strength = value; }
    }

    private int agility;

    public int Agility
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

    // Start is called before the first frame update
    void Awake()
    {
        //later we load this initial char data from a savefile
        Level = 1;
        Stamina = 2;
        Strength = 3;
        Agility = 3;
        MaxHealth = Stamina * 10; //every stamina is worth 10 health
        CurrentHealth = MaxHealth;
        CurrentXP = 0;
        XPToLevel = FormulaHelper.CalculateXP(Level);
    }

    private void Update()
    {
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
        PublishStats();

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
        PublishStats();

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
        PublishStats();
    }

    private void LevelUp()
    {
        Level += 1;
        Stamina += 2;
        Strength += 3;
        Agility += 2;
        MaxHealth = Stamina * 10; //every stamina is worth 10 health
        CurrentHealth = MaxHealth;
    }

    void PublishStats()
    {
        onStatChange?.Invoke();
    }

    private void Death()
    {
        Debug.Log("Dead");
    }
}
