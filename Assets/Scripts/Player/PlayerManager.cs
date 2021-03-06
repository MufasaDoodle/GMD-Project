using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public bool isInRangeOfShop = false;

    public CharacterStats PlayerStats;
    public PlayerEquipment PlayerEquipment;
    public PlayerInventory PlayerInventory;

    public AudioSource musicPlayer;
    public AudioSource sfxPlayer;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        var settings = GameSettingsManager.Instance;
        musicPlayer.volume = settings.musicSetting;
        sfxPlayer.volume = settings.sfxSetting;
    }
}
