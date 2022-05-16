using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealthUI : MonoBehaviour
{
    Slider healthSlider;
    public EnemyStats enemyStats;
    public TextMeshProUGUI levelText;

    // Start is called before the first frame update
    void Start()
    {
        healthSlider = GetComponent<Slider>();
        healthSlider.minValue = 0;
        healthSlider.maxValue = enemyStats.maxHealth;
        healthSlider.value = enemyStats.currentHealth;
        levelText.text = "Level " + enemyStats.level;
        gameObject.SetActive(false);
    }

    public void SetHealthSlider(int value)
    {
        healthSlider.value = value;
    }
}
