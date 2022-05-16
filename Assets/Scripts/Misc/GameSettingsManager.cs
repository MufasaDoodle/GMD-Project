using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameSettingsManager : MonoBehaviour
{
    public static GameSettingsManager Instance;

    public GameObject settingsUI;
    public Slider maxFPSSlider;
    public Slider musicVolSlider;
    public Slider sfxVolSlider;

    public TextMeshProUGUI fpsText;
    public TextMeshProUGUI musicVolText;
    public TextMeshProUGUI sfxVolText;

    public float musicSetting;
    public float sfxSetting;

    private void Awake()
    {
        Instance = this; 

        if (settingsUI == null)
        {
            return;
        }
        musicVolSlider.minValue = 0f;
        musicVolSlider.maxValue = 1f;
        musicVolSlider.value = 0.15f;

        sfxVolSlider.minValue = 0f;
        sfxVolSlider.maxValue = 1f;
        sfxVolSlider.value = 0.5f;

        maxFPSSlider.minValue = 30f;
        maxFPSSlider.maxValue = 244f;
        maxFPSSlider.value = 60f;

        musicVolSlider.value = PlayerPrefs.GetFloat("MusicVol");
        sfxVolSlider.value = PlayerPrefs.GetFloat("SfxVol");
        maxFPSSlider.value = PlayerPrefs.GetInt("maxFramerate");
        Application.targetFrameRate = (int)maxFPSSlider.value;
        QualitySettings.vSyncCount = 0;
        musicSetting = musicVolSlider.value;
        sfxSetting = sfxVolSlider.value;
    }

    public void OnSliderChange()
    {
        fpsText.text = maxFPSSlider.value.ToString();
        musicVolText.text = (musicVolSlider.value * 100).ToString() + "%";
        sfxVolText.text = (sfxVolSlider.value * 100).ToString() + "%";
        Application.targetFrameRate = (int)maxFPSSlider.value;
        QualitySettings.vSyncCount = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(settingsUI == null)
            {
                return;
            }

            if (!settingsUI.activeSelf)
            {
                return;
            }
            DisableUI();
        }
    }

    public void GoToMainMenu()
    {
        DisableUI();
    }

    private void Save()
    {
        PlayerPrefs.SetInt("maxFramerate", (int)maxFPSSlider.value);
        PlayerPrefs.SetFloat("MusicVol", musicVolSlider.value);
        PlayerPrefs.SetFloat("SfxVol", sfxVolSlider.value);
        musicSetting = musicVolSlider.value;
        sfxSetting = sfxVolSlider.value;
    }

    public void EnableUI()
    {
        settingsUI.SetActive(true);
        musicSetting = musicVolSlider.value;
        sfxSetting = sfxVolSlider.value;
    }

    public void DisableUI()
    {
        Save();
        settingsUI.SetActive(false);
    }
}
