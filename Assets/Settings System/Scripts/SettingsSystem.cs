using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public static class SettingsSystem
{
    static SettingsProfile defaultProfile;
    static SettingsProfile activeProfile;
    static SettingsData saveData;
    public delegate void OnSettingChanged();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Init()
    {
        defaultProfile = (SettingsProfile)Resources.Load("Settings Profiles/Default");
        activeProfile = (SettingsProfile)Resources.Load("Settings Profiles/Active");
        RestoreAllDefaults();
        LoadSettingsData();
    }
    public static void RestoreAllDefaults() => LoadAllFromProfile(defaultProfile);
    public static event OnSettingChanged OnProfileLoaded;
    public static void LoadAllFromProfile(SettingsProfile profile)
    {
        LoadAudioFromProfile(profile);

        LoadVideoFromProfile(profile);

        LoadKeybindsFromProfile(profile);

        LoadCustomSettingsFromProfile(profile);

        OnProfileLoaded?.Invoke();
    }
    public static void RestoreAudioDefaults() => LoadAudioFromProfile(defaultProfile);
    public static void LoadAudioFromProfile(SettingsProfile profile)
    {
        activeProfile.masterVolume = profile.masterVolume;
        activeProfile.musicVolume = profile.musicVolume;
        activeProfile.sfxVolume = profile.sfxVolume;
        activeProfile.uiVolume = profile.uiVolume;
    }
    public static void RestoreVideoDefaults() => LoadVideoFromProfile(defaultProfile);
    public static void LoadVideoFromProfile(SettingsProfile profile)
    {
        QualityIndex = profile.qualityLevelIndex;
    }
    public static void RestoreKeybindsDefaults() => LoadKeybindsFromProfile(defaultProfile);
    public static void LoadKeybindsFromProfile(SettingsProfile profile)
    {
        activeProfile.keybinds = profile.keybinds;
    }
    public static void LoadCustomSettingsFromProfile(SettingsProfile profile)
    {
        activeProfile.customSliders = new SettingsProfile.SliderSettings[profile.customSliders.Length];
        for (int i = 0; i < profile.customSliders.Length; i++) activeProfile.customSliders[i] = profile.customSliders[i];

        activeProfile.customIndexes = new SettingsProfile.IndexSettings[profile.customIndexes.Length];
        for (int i = 0; i < profile.customIndexes.Length; i++) activeProfile.customIndexes[i] = profile.customIndexes[i];
        
        activeProfile.customToggles = new SettingsProfile.ToggleSettings[profile.customToggles.Length];
        for (int i = 0; i < profile.customToggles.Length; i++) activeProfile.customToggles[i] = profile.customToggles[i];
    }

    #region Audio Methods

    public static event OnSettingChanged OnMasterVolumeChanged;
    public static float MasterVolume
    {
        get { return activeProfile.masterVolume; }
        set
        {
            activeProfile.masterVolume = value;
            OnMasterVolumeChanged?.Invoke();
        }
    }

    public static event OnSettingChanged OnMusicVolumeChanged;
    public static float MusicVolume
    {
        get { return activeProfile.musicVolume; }
        set
        {
            activeProfile.musicVolume = value;
            OnMusicVolumeChanged?.Invoke();
        }
    }

    public static event OnSettingChanged OnSFXVolumeChanged;
    public static float SFXVolume
    {
        get { return activeProfile.sfxVolume; }
        set
        {
            activeProfile.sfxVolume = value;
            OnSFXVolumeChanged?.Invoke();
        }
    }

    public static event OnSettingChanged OnUIVolumeChanged;
    public static float UIVolume
    {
        get { return activeProfile.uiVolume; }
        set
        {
            activeProfile.uiVolume = value;
            OnUIVolumeChanged?.Invoke();
        }
    }
    #endregion

    #region Video Methods
    public static event OnSettingChanged OnQualityChanged;
    public static int QualityIndex
    {
        get { return activeProfile.qualityLevelIndex; }
        set
        {
            activeProfile.qualityLevelIndex = value;
            QualitySettings.SetQualityLevel(value);
            OnQualityChanged?.Invoke();
        }
    }
    #endregion

    #region Keybind Methods
    public static int KeybindsCount => defaultProfile.keybinds.Length;
    public delegate void OnKeybindDelegate(string name, SettingsProfile.KeyBind bind);
    public static event OnKeybindDelegate OnKeybindChanged;
    public static SettingsProfile.KeyBind GetKeybind(string name)
    {
        return activeProfile.GetKeybind(name);
    }
    public static SettingsProfile.KeyBind[] GetKeybindFromKeyCode(KeyCode key)
    {
        return activeProfile.GetKeybindFromKeyCode(key);
    }
    public static void SetKeybind(string name, KeyCode newKey)
    {
        for (int i = 0; i < activeProfile.keybinds.Length; i++)
        {
            if (activeProfile.keybinds[i].name == name)
            {
                activeProfile.keybinds[i].key = newKey;
                OnKeybindChanged?.Invoke(name, activeProfile.keybinds[i]);
                return;
            }
        }
        Debug.LogWarning(name + " keybind was not found, hence was not set");
    }
    public static void SetKeybind(string name, SettingsProfile.KeyBind newKey)
    {
        for (int i = 0; i < activeProfile.keybinds.Length; i++)
        {
            if (activeProfile.keybinds[i].name == name)
            {
                activeProfile.keybinds[i] = newKey;
                OnKeybindChanged?.Invoke(name, activeProfile.keybinds[i]);
                return;
            }
        }
        Debug.LogWarning(name + " keybind was not found, hence was not set");
    }
    public static void SetAltKeybind(string name, KeyCode newKey)
    {
        for (int i = 0; i < activeProfile.keybinds.Length; i++)
        {
            if (activeProfile.keybinds[i].name == name)
            {
                activeProfile.keybinds[i].altKey = newKey;
                OnKeybindChanged?.Invoke(name, activeProfile.keybinds[i]);
                return;
            }
        }
        Debug.LogWarning(name + " keybind was not found, hence was not set");
    }
    public static void RestoreSpecificKeybindDefault(string name)
    {
        SettingsProfile.KeyBind defaultValue = defaultProfile.GetKeybind(name);
        SetKeybind(name, defaultValue);
    }

    #if ENABLE_LEGACY_INPUT_MANAGER
    // Add code specific to the New Input System here
    public static async Task<KeyCode> DetectKeybind()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.Escape)) break;
            if (Input.GetKey(KeyCode.Backspace)) return KeyCode.None;

            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(kcode))
                    return kcode;
            }

            await Task.Delay(10);
        }
    return KeyCode.None;
    }
    #else
    // Add code specific to the Legacy Input Manager here
    public static async Task<KeyCode> DetectKeybind()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.Escape)) break;
            if (Input.GetKey(KeyCode.Backspace)) return KeyCode.None;

            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(kcode))
                    return kcode;
            }

            await Task.Delay(10);
        }
        return KeyCode.None;
    }
    #endif
    #endregion

    #region Custom Settings Methods
    public static float GetCustomSliderValue(string name) => activeProfile.GetCustomSliderValue(name);
    public static void SetCustomSlider(string name, float value) => activeProfile.SetCustomSlider(name, value);
    public static void RestoreCustomSliderDefault(string name)
    {
        float defaultValue = defaultProfile.GetCustomSliderValue(name);
        SetCustomSlider(name, defaultValue);
    }

    public static int GetCustomIndex(string name) => activeProfile.GetCustomIndex(name);
    public static void SetCustomIndex(string name, int value) => activeProfile.SetCustomIndex(name, value);
    public static void RestoreCustomIndexDefault(string name)
    {
        int defaultValue = defaultProfile.GetCustomIndex(name);
        SetCustomIndex(name, defaultValue);
    }

    public static bool GetCustomToggle(string name) => activeProfile.GetCustomToggle(name);
    public static void SetCustomToggle(string name, bool value) => activeProfile.SetCustomToggle(name, value);
    public static void RestoreCustomToggleDefault(string name)
    {
        bool defaultValue = defaultProfile.GetCustomToggle(name);
        SetCustomToggle(name, defaultValue);
    }
    #endregion

    #region Data Methods
    const string SAVE_FILE_NAME = "Settings_Data.dat";
    public static void SaveSettingsData()
    {
        saveData = new SettingsData(activeProfile);
        SaveSystem.SaveData(saveData, SAVE_FILE_NAME);
    }
    static void LoadSettingsData()
    {
        saveData = SaveSystem.LoadData<SettingsData>(SAVE_FILE_NAME);
        if(saveData == null) SaveSettingsData();
        LoadProfileFromData(saveData);
    }
    static void LoadProfileFromData(SettingsData data)
    {
        activeProfile.masterVolume = data.masterVolume;
        activeProfile.musicVolume = data.musicVolume;
        activeProfile.sfxVolume = data.sfxVolume;
        activeProfile.uiVolume = data.uiVolume;

        activeProfile.qualityLevelIndex = data.qualityLevelIndex;

        activeProfile.keybinds = data.keybinds;

        for (int i = 0; i < activeProfile.customSliders.Length; i++)
        {
            for (int j = 0; j < data.customSliders.Length; j++)
            {
                if (activeProfile.customSliders[i].name == data.customSliders[j].name)
                {
                    activeProfile.customSliders[i] = data.customSliders[j];
                    break;
                }
            }
        }
        for (int i = 0; i < activeProfile.customIndexes.Length; i++)
        {
            for (int j = 0; j < data.customIndexes.Length; j++)
            {
                if (activeProfile.customIndexes[i].name == data.customIndexes[j].name)
                {
                    activeProfile.customIndexes[i] = data.customIndexes[j];
                    break;
                }
            }
        }
        for (int i = 0; i < activeProfile.customToggles.Length; i++)
        {
            for (int j = 0; j < data.customToggles.Length; j++)
            {
                if (activeProfile.customToggles[i].name == data.customToggles[j].name)
                {
                    activeProfile.customToggles[i] = data.customToggles[j];
                    break;
                }
            }
        }
    }
    public static void UndoUnsavedSettings()
    {
        LoadProfileFromData(saveData);
    }
    #endregion
}
