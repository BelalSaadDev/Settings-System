using UnityEngine.UI;
using UnityEngine;
using TMPro;
public class DropdownUI : MonoBehaviour
{
    public string dataName;
    public TMP_Dropdown dropdown;

    private void Start()
    {
        Load();
        SettingsSystem.OnProfileLoaded += Load;
    }
    void Load()
    {
        if (dataName == "quality")
            dropdown.value = SettingsSystem.QualityIndex;
        else
        {
            int value = SettingsSystem.GetCustomIndex(dataName);
            if (value == -1) return;
            dropdown.value = value;
        }
    }
    public void OnValueChanged(int value)
    {
        if (dataName == "quality")
            SettingsSystem.QualityIndex = dropdown.value;
        else
            SettingsSystem.SetCustomIndex(dataName, value);
    }
}
