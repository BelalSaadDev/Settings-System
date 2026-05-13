using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ToggleUI : MonoBehaviour
{
    public string dataName;
    public Toggle toggle;

    private void Start()
    {
        Load();
        SettingsSystem.OnProfileLoaded += Load;
    }
    void Load()
    {
        toggle.isOn = SettingsSystem.GetCustomToggle(dataName);
    }
    public void OnValueChanged(bool value)
    {
        SettingsSystem.SetCustomToggle(dataName, value);
    }
}
