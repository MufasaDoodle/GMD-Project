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

    void UpdateHealth(int current, int max)
    {
        healthText.text = $"{current}/{max}";
        healthSlider.value = current;
        healthSlider.maxValue = max;
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
        PlayerManager.Instance.PlayerStats.onHealthChange += UpdateHealth;

        //need an additional manual check because of the execution order
        CharacterStats stats = PlayerManager.Instance.PlayerStats;
        UpdateHealth(stats.CurrentHealth, stats.MaxHealth);
    }

    void UnsubscribeToEvents()
    {
        PlayerManager.Instance.PlayerStats.onHealthChange -= UpdateHealth;
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
