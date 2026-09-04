using System.Collections;
using System.Collections.Generic;
using TimeSystem;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CalendarPanel : MonoBehaviour, IPointerEnterHandler
{
    public Image sprite, indicadorDia;
    public TextMeshProUGUI day, descriptionText;
    public SerializableDateTime dateTime;
    [HideInInspector] public string description;

    public void SetUpDay(string date)
    {
        day.text = date;
        sprite.sprite = null;
        sprite.color = Color.clear;
        indicadorDia.enabled = false;
        description = "";
    }

    public void AssignKeyDate(KeyDATES keyDATES)
    {
        sprite.sprite = keyDATES.image;
        sprite.color = Color.white;
        description = keyDATES.description;
    }

    public void ActivateActualDay()
    {
        indicadorDia.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionText.text = description;
    }
}
