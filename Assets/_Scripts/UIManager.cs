using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject btnRestart;
    private void Awake()
    {
        instance = this;
        btnRestart = GameObject.Find("BtnRestart");
        btnRestart.SetActive(false);
    }
}
