using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    public static SettingsUI instance;
    bool isChangingKey = false;
    public Image[] navigationButtonImages;
    public GameObject[] menus;
    public Color navigationSelectedColor;
    public Color navigationDeselectedColor;
    private void Awake()
    {
        instance = this;
    }
    public async void ChangeKeyBind(KeybindUI ui)
    {
        if (isChangingKey) return;
        isChangingKey = true;
        KeyCode key = await SettingsSystem.DetectKeybind();
        SettingsSystem.SetKeybind(ui.dataName, key);
        ui.UpdateUI(SettingsSystem.GetKeybind(ui.dataName));
        isChangingKey = false;
    }
    public async void ChangeAltKeyBind(KeybindUI ui)
    {
        if (isChangingKey) return;
        isChangingKey = true;
        KeyCode key = await SettingsSystem.DetectKeybind();
        SettingsSystem.SetAltKeybind(ui.dataName, key);
        ui.UpdateUI(SettingsSystem.GetKeybind(ui.dataName));
        isChangingKey = false;
    }
    public void OpenMenu(int menuIndex)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].SetActive(i == menuIndex);
            navigationButtonImages[i].color = i == menuIndex ? navigationSelectedColor : navigationDeselectedColor;
        }
    }
    public void Save()
    {
        SettingsSystem.SaveSettingsData();
    }
    public void RestoreDefaults()
    {
        SettingsSystem.RestoreAllDefaults();
    }
}
