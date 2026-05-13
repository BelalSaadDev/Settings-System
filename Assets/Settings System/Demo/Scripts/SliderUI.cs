using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderUI : MonoBehaviour
{
    public string dataName;
    public Slider slider;

    private void Start()
    {
        Load();
        SettingsSystem.OnProfileLoaded += Load;
    }
    void Load()
    {
        if (dataName == "master")
            slider.value = SettingsSystem.MasterVolume;
        else if (dataName == "music")
            slider.value = SettingsSystem.MusicVolume;
        else if (dataName == "sfx")
            slider.value = SettingsSystem.SFXVolume;
        else if (dataName == "ui")
            slider.value = SettingsSystem.UIVolume;
        else
        {
            float value = SettingsSystem.GetCustomSliderValue(dataName);
            if (value == -1f) return;
            slider.value = value;
        }
    }
    public void OnValueChanged(float value)
    {
        if (dataName == "master")
            SettingsSystem.MasterVolume = value;
        else if (dataName == "music")
            SettingsSystem.MusicVolume = value;
        else if (dataName == "sfx")
            SettingsSystem.SFXVolume = value;
        else if (dataName == "ui")
            SettingsSystem.UIVolume = value;
        else
        {
            SettingsSystem.SetCustomSlider(dataName, value);
        }
    }
}
