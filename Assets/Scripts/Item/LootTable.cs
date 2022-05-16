using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTable : MonoBehaviour
{
    public bool canDropGold = false;
    public int goldDropMin = 0;
    public int goldDropMax = 4;

    public LootTableEntry[] lootEntries;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public struct LootTableEntry
{
    [SerializeField]
    private int id;
    public int ID
    {
        get { return id; }
    }

    [SerializeField]
    private int amountMin;
    public int AmountMin
    {
        get { return amountMin; }
        set { amountMin = value; }
    }

    [SerializeField]
    private int amountMax;
    public int AmountMax
    {
        get { return amountMax; }
        set { amountMax = value; }
    }

    [SerializeField]
    private float chance;
    public float Chance
    {
        get { return chance; }
        set { chance = value; }
    }

    public LootTableEntry(int id, int amountMin, int amountMax, float chance) : this()
    {
        this.id = id;
        AmountMin = amountMin;
        AmountMax = amountMax;
        Chance = chance;
    }
}
