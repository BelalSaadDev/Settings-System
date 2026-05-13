using UnityEngine;
using System;
using KeyBind = SettingsProfile.KeyBind;
using SliderSettings = SettingsProfile.SliderSettings;
using IndexSettings = SettingsProfile.IndexSettings;
using ToggleSettings = SettingsProfile.ToggleSettings;

[Serializable]
public class SettingsData
{
    public float masterVolume = 0.5f;
    public float sfxVolume = 0.5f;
    public float musicVolume = 0.5f;
    public float uiVolume = 0.5f;

    public int qualityLevelIndex;

    public KeyBind[] keybinds;

    public SliderSettings[] customSliders;
    public IndexSettings[] customIndexes;
    public ToggleSettings[] customToggles;

    public SettingsData()
    {
        masterVolume = 0.5f;
        sfxVolume = 0.5f;
        musicVolume = 0.5f;
        uiVolume = 0.5f;
        qualityLevelIndex = 0;
        keybinds = new KeyBind[0];
        customSliders = new SliderSettings[0];
        customIndexes = new IndexSettings[0];
        customToggles = new ToggleSettings[0];
    }
    public SettingsData(SettingsProfile profile)
    {
        masterVolume = profile.masterVolume;
        musicVolume = profile.musicVolume;
        sfxVolume = profile.sfxVolume;
        uiVolume = profile.uiVolume;

        qualityLevelIndex = profile.qualityLevelIndex;

        keybinds = profile.keybinds;

        customSliders = profile.customSliders;
        customIndexes = profile.customIndexes;
        customToggles = profile.customToggles;
    }
}
