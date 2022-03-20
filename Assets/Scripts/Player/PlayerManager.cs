using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public CharacterStats PlayerStats;
    public PlayerEquipment PlayerEquipment;
    public PlayerInventory PlayerInventory;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }
}
