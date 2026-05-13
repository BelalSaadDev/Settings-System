using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class KeybindUI : MonoBehaviour
{
    public string dataName;
    public TMP_Text keyText;
    public TMP_Text altKeyText;

    private void Start()
    {
        Load();
        SettingsSystem.OnProfileLoaded += Load;
    }
    void Load()
    {
        SettingsProfile.KeyBind key = SettingsSystem.GetKeybind(dataName);
        UpdateUI(key);
    }
    public void ChangeKeybind()
    {
        SettingsUI.instance.ChangeKeyBind(this);
    }
    public void ChangeAltKeybind()
    {
        SettingsUI.instance.ChangeAltKeyBind(this);
    }
    public void UpdateUI(SettingsProfile.KeyBind keybind)
    {
        keyText.text = keybind.key.ToString();
        altKeyText.text = keybind.altKey.ToString();
    }
}
