using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Settings Profile")]
public class SettingsProfile : ScriptableObject
{
    [Header("Audio Settings")]
    [Range(0f, 1f)] public float masterVolume = 0.5f;
    [Range(0f, 1f)] public float sfxVolume = 0.5f;
    [Range(0f, 1f)] public float musicVolume = 0.5f;
    [Range(0f, 1f)] public float uiVolume = 0.5f;

    [Header("Video Settings")]
    public int qualityLevelIndex;

    [Header("Keybind Settings")]
    public KeyBind[] keybinds;
    [Serializable] public struct KeyBind
    {
        public string name;
        public KeyCode key;
        public KeyCode altKey;
    }

    [Header("Custom Settings")]
    public SliderSettings[] customSliders;
    [Serializable] public struct SliderSettings
    {
        public string name;
        [Range(0f, 1f)] public float value;
    }
    public IndexSettings[] customIndexes;
    [Serializable] public struct IndexSettings
    {
        public string name;
        public int value;
    }
    public ToggleSettings[] customToggles;
    [Serializable] public struct ToggleSettings
    {
        public string name;
        public bool value;
    }

    public KeyBind GetKeybind(string name)
    {
        for (int i = 0; i < keybinds.Length; i++)
        {
            if (keybinds[i].name == name) return keybinds[i];
        }
        Debug.LogWarning(name + " keybind was not found");
        return new() { name = "", key = KeyCode.None, altKey = KeyCode.None };
    }
    public KeyBind[] GetKeybindFromKeyCode(KeyCode key)
    {
        List<KeyBind> foundKeybinds = new List<KeyBind>();
        for (int i = 0; i < keybinds.Length; i++)
        {
            if (keybinds[i].key == key || keybinds[i].altKey == key)
            {
                foundKeybinds.Add(keybinds[i]);
            }
        }
        return foundKeybinds.ToArray();
    }
    public void ChangeKeybind(string name, KeyCode newKey)
    {
        for (int i = 0; i < keybinds.Length; i++)
        {
            if (keybinds[i].name == name)
            {
                keybinds[i].key = newKey;
                return;
            }
        }
        Debug.LogWarning(name + " keybind was not found, hence was not set");
    }
    public void ChangeAltKeybind(string name, KeyCode newKey)
    {
        for (int i = 0; i < keybinds.Length; i++)
        {
            if (keybinds[i].name == name)
            {
                keybinds[i].altKey = newKey;
                return;
            }
        }
        Debug.LogWarning(name + " keybind was not found, hence was not set");
    }

    public float GetCustomSliderValue(string name)
    {
        for (int i = 0; i < customSliders.Length; i++)
        {
            if (customSliders[i].name == name) return customSliders[i].value;
        }
        Debug.LogWarning(name + " custom slider was not found");
        return -1f;
    }
    public void SetCustomSlider(string name, float value)
    {
        for (int i = 0; i < customSliders.Length; i++)
        {
            if (customSliders[i].name == name)
            {
                customSliders[i].value = value;
                return;
            }
        }
        Debug.LogWarning(name + " custom slider was not found, hence was not set");
    }

    public int GetCustomIndex(string name)
    {
        for (int i = 0; i < customIndexes.Length; i++)
        {
            if (customIndexes[i].name == name) return customIndexes[i].value;
        }
        Debug.LogWarning(name + " custom index was not found");
        return -1;
    }
    public void SetCustomIndex(string name, int value)
    {
        for (int i = 0; i < customIndexes.Length; i++)
        {
            if (customIndexes[i].name == name)
            {
                customIndexes[i].value = value;
                return;
            }
        }
        Debug.LogWarning(name + " custom index was not found, hence was not set");
    }

    public bool GetCustomToggle(string name)
    {
        for (int i = 0; i < customToggles.Length; i++)
        {
            if (customToggles[i].name == name) return customToggles[i].value;
        }
        Debug.LogWarning(name + " custom toggle was not found");
        throw new Exception("Toggle Not Found");
    }
    public void SetCustomToggle(string name, bool value)
    {
        for (int i = 0; i < customToggles.Length; i++)
        {
            if (customToggles[i].name == name)
            {
                customToggles[i].value = value;
                return;
            }
        }
        Debug.LogWarning(name + " custom toggle was not found, hence was not set");
    }
}
