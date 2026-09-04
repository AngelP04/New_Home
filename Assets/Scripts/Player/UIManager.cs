using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;
using UnityEngine.InputSystem;
using Unity.Mathematics;
using System;
using TimeSystem;

public class UIManager : MonoBehaviour
{
    public Slider HealthBar;
    public Scrollbar progressBar;
    public TextMeshProUGUI HealthText, reloj;

    public static UIManager instance {  get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        timeManager.OnDateTimeChanged += UpdateTime;
    }

    // Update is called once per frame
    void Update()
    {
        StringBuilder sb = new StringBuilder("HP: ");
        sb.Append(HealthBar.value);
        sb.Append("/");
        sb.Append(HealthBar.maxValue);
        HealthText.text = sb.ToString();
        HealthBar.maxValue = GameManager.instance.playerHealthMax;
        HealthBar.value = GameManager.instance.playerHealthCurrent;
    }

    private void UpdateTime(SerializableDateTime dateTime)
    {
        reloj.text = string.Format("{00:00}:{01:00}", dateTime.Hours, dateTime.Minutes);
    }
}
