using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class ToggleGraphic : MonoBehaviour
{
    public TransitionType transitionType;
    [Header("Sprite-Swap Settings")]
    public Sprite On;
    public Sprite Off;
    public Image CheckMark;
    [Header("Animation Settings")]
    public Animator animator;
    Toggle toggle;
    private void OnEnable()
    {
        toggle = GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(delegate { ValueChanged(toggle.isOn); });
        ValueChanged(toggle.isOn);
    }
    public void ValueChanged(bool change)
    {
        switch (transitionType)
        {
            case TransitionType.SpriteSwap:
                CheckMark.sprite = change ? On : Off;
                break;
            case TransitionType.Animation:
                animator.SetBool("isOn", change);
                break;
        }
    }
    public enum TransitionType { SpriteSwap, Animation }
}
