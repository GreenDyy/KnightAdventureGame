using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CloverCounter : MonoBehaviour
{
    public static CloverCounter instance;
    public TMP_Text cloverText;
    public int startClovers = 5;
    public int currentClovers = 0;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        currentClovers = startClovers;
        cloverText.text = currentClovers.ToString();
    }

    public void IncreaseClovers(int value)
    {
        currentClovers += value;
        cloverText.text = currentClovers.ToString();
    }

    public void DecreaseClovers(int value)
    {
        currentClovers -= value;
        cloverText.text = currentClovers.ToString();
    }
}
