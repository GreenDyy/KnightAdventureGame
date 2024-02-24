using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarPlayer : MonoBehaviour
{
    public Slider slider;
    public Image fill;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }
    public void SetMaxStamina(int maxstamina)
    {
        slider.maxValue = maxstamina;
        slider.value = maxstamina;
    }
    public void SetStamina(int stamina)
    {
        slider.value = stamina;
    }
}
