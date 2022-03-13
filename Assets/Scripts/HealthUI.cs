using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Slider healthSlider;
    public TextMeshProUGUI healthText;

    bool isInitialized = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InitSlider());
    }

    void UpdateHealth()
    {
        CharacterStats stats = PlayerManager.Instance.PlayerStats;
        healthText.text = $"{stats.CurrentHealth}/{stats.MaxHealth}";
        healthSlider.value = stats.CurrentHealth;
    }
    private void OnEnable()
    {
        if(isInitialized)
            SubscribeToEvents();
    }

    private void OnDisable()
    {
        UnsubscribeToEvents();
    }

    void SubscribeToEvents()
    {
        PlayerManager.Instance.PlayerStats.onStatChange += UpdateHealth;
        UpdateHealth();
    }

    void UnsubscribeToEvents()
    {
        PlayerManager.Instance.PlayerStats.onStatChange -= UpdateHealth;
    }
    private IEnumerator InitSlider()
    {
        new WaitForSeconds(0.1f);
        gameObject.SetActive(true);
        healthSlider.minValue = 0f;
        healthSlider.maxValue = PlayerManager.Instance.PlayerStats.MaxHealth;
        healthSlider.value = PlayerManager.Instance.PlayerStats.CurrentHealth;
        SubscribeToEvents();
        isInitialized = true;
        yield return null;
    }

     
}
