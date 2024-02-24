using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        //fill = GetComponent<Image>();
    }
    public void SetMaxHeath(int maxhp)
    {
        slider.maxValue = maxhp;
        slider.value = maxhp;
    }
    public void SetHealth(int heath)
    {
        slider.value = heath;
    }
}
