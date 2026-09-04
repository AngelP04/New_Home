using Articy.New_Home.GlobalVariables;
using Articy.Unity;
using System.Collections;
using System.Collections.Generic;
using TimeSystem;
using TMPro;
using UnityEngine;

public class CalendarManager : MonoBehaviour
{
    public List<KeyDATES> keyDats;
    public List<CalendarPanel> panelList;
    public TextMeshProUGUI seasonText;
    public GameObject calendar;

    private SerializableDateTime previousDateTime;

    private int actualSeasonView = 0;
    private int actualMonthView = 0;

    private void Awake()
    {
        timeManager.OnDateTimeChanged += DateTimeChanged;
    }

    // Start is called before the first frame update
    void Start()
    {
        previousDateTime = timeManager.dateTime;
        FillPanels((Month)actualMonthView);
    }

    private void DateTimeChanged(SerializableDateTime dateTime)
    {
        if(actualMonthView == (int)dateTime.Month)
        {
            if(previousDateTime.Date != dateTime.Date)
            {
                var index = (previousDateTime.Date - 1) < 0 ? 0 : (previousDateTime.Date - 1);

            }
            previousDateTime = dateTime;
        }
    }

    private void FillPanels(Month month)
    {
        seasonText.text = month.ToString();
        for (int i = 0; i < panelList.Count; i++)
        {
            panelList[i].SetUpDay((i + 1).ToString());
            panelList[i].dateTime = new(i, actualSeasonView, previousDateTime.Year, previousDateTime.Hours, previousDateTime.Minutes, actualMonthView);
            if (panelList[i].dateTime.Date == timeManager.dateTime.Date && panelList[i].dateTime.Month == timeManager.dateTime.Month)
            {
                panelList[i].ActivateActualDay();
            }
            foreach(KeyDATES keydates in keyDats)
            {
                if((i + 1) == keydates.KeyDate.Date)
                {
                    if(keydates.monthly)
                    {
                        panelList[i].AssignKeyDate(keydates);
                    }
                    else
                    {
                        if((Month)actualMonthView == keydates.KeyDate.Month)
                        {
                            panelList[i].AssignKeyDate(keydates);
                        }
                    }
                }
            }
        }
    }

    public void AdvanceSeason()
    {
        actualMonthView++;
        if(actualMonthView > 11)
        {
            actualMonthView = 0;
        }
        FillPanels((Month)actualMonthView);
    }

    public void BackSeason()
    {
        actualMonthView--;
        if (actualMonthView < 0)
        {
            actualMonthView = 11;
        }
        FillPanels((Month)actualMonthView);
    }

    public void ChangeState()
    {
        calendar.SetActive(!calendar.activeInHierarchy);
    }
}
