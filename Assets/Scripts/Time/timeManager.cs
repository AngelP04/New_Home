using Articy.New_Home.GlobalVariables;
using Cultive;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Events;

namespace TimeSystem
{
    public class timeManager : MonoBehaviour
    {
        [Header("Date & Time Settings")]
        [Range(1, 28)]
        public int dateInMonth;
        [Range(1, 4)]
        public int season;
        [Range(1, 99)]
        public int year;
        [Range(0, 24)]
        public int hour;
        [Range(0, 6)]
        public int minute;
        [Range(1, 12)]
        public int month;

        public int tickSecondsIncrease = 10;
        public static SerializableDateTime dateTime;

        [Header("Tick Settings")]
        public float timeBetweenTicks = 1;
        private float currentTimeBetweenTicks = 0;

        public static UnityAction<SerializableDateTime> OnDateTimeChanged;

        public static timeManager instance { get; private set; }

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
            dateTime = new(dateInMonth, season, year, hour, minute, month);
        }

        private void Start()
        {
            OnDateTimeChanged?.Invoke(dateTime);
        }

        // Update is called once per frame
        void Update()
        {
            currentTimeBetweenTicks += Time.deltaTime;
            if(currentTimeBetweenTicks > timeBetweenTicks)
            {
                currentTimeBetweenTicks = 0;
                Tick();
            }
            dateInMonth = dateTime.Date;
            month = (int)dateTime.Month;
        }

        private void Tick()
        {
            AdvanceTime();
        }

        private void AdvanceTime()
        {
            dateTime.AdvanceMinutes(tickSecondsIncrease);
            OnDateTimeChanged?.Invoke(dateTime);
        }

        public SerializableDateTime SaveDateTime()
        {
            return dateTime.SaveDay();
        }

        public void LoadDay(SerializableDateTime serializableDateTime)
        {
            dateTime.LoadDay(serializableDateTime);
        }

    }

    [Serializable]
    public struct SerializableDateTime
    {
        #region Fields
        private Days day;
        [SerializeField] private Season season;
        [SerializeField] private int date;
        [SerializeField] private int year;
        [SerializeField] private Month month;

        private int totalNumDays;
        private int totalNumWeeks;
        [SerializeField] private int hours;
        [SerializeField] private int minutes;
        #endregion

        #region Properties
        public Days Day => day;
        public int Date => date;

        public int Hours => hours;

        public int Minutes => minutes;

        public Season Season => season;
        public int Year => year;
        public int TotalNumDays => totalNumDays;

        public int TotalNumWeeks => totalNumWeeks;

        public Month Month => month;

        public int CurrentWeek => totalNumWeeks % 16 == 0 ? 16 : totalNumWeeks % 16;

        #endregion

        #region Constructor
        public SerializableDateTime(int date, int season, int year, int hour, int minutes, int month)
        {
            this.day = (Days)(date % 7);
            if (day == 0) day = (Days)7;
            this.date = date;
            this.season = (Season)season;
            this.year = year;
            this.hours = hour;
            this.minutes = minutes;
            this.month = (Month)month;

            totalNumDays = date + (28 * (int)this.season) + (112 * (year - 1));
            totalNumWeeks = 1 + totalNumDays / 7;
        }
        #endregion

        #region Time Advancement
        public void AdvanceMinutes(int secondsToAdvanceBy)
        {
            if(minutes + secondsToAdvanceBy >= 60)
            {
                minutes = (minutes + secondsToAdvanceBy) % 60;
                AdvanceHour();
            }
            else
            {
                minutes += secondsToAdvanceBy;
            }
        }

        private void AdvanceHour()
        {
            if((hours + 1) == 24)
            {
                hours = 0;
                AdvanceDay();
            }
            else
            {
                hours++;
            }
        }
        private void AdvanceDay()
        {
            day++;
            if (day > (Days)7)
            {
                day = (Days)1;
                totalNumWeeks++;
            }
            date++;
            if(date % 29 == 0)
            {
                AdvanceMonth();
                date = 1;
            }
            totalNumDays++;
        }

        private void AdvanceMonth()
        {
            month++;
            if(month > (Month)11)
            {
                month = 0;
                AdvanceYear();
            }
            if((int)month % 3 == 0)
            {
                AdvanceSeason();
            }
        }

        private void AdvanceSeason()
        {
            if(season == Season.Winter)
            {
                season = Season.Spring;
                AdvanceYear();
            }
            else
            {
                season++;
            }
        }
        private void AdvanceYear()
        {
            date = 1;
            year++;
        }
        #endregion

        #region Bool Checks
        public bool IsNight()
        {
            return hours > 18 || hours < 6;
        }

        public bool IsMorning()
        {
            return hours >= 6 && hours <= 17;
        }

        public bool IsAfternoon()
        {
            return hours > 12 && hours < 18;
        }

        public bool IsWeekend()
        {
            return day > Days.Fri ? true : false;
        }

        public bool IsParticularyDay(Days _day)
        {
            return day == _day;
        }
        #endregion

        #region Key Dates
        public SerializableDateTime NewYearDay(int year)
        {
            if (year == 0) year = 1;
            return new SerializableDateTime(1, 0, year, 6, 0, 0);
        }
        public SerializableDateTime SummerSolstice(int year)
        {
            if (year == 0) year = 1;
            return new SerializableDateTime(20, 1, year, 6, 0, 3);
        }
        public SerializableDateTime PumpkinHarves(int year)
        {
            if (year == 0) year = 1;
            return new SerializableDateTime(28, 2, year, 6, 0, 9);
        }
        #endregion

        #region Guadar y Cargar
        public SerializableDateTime SaveDay()
        {
            SerializableDateTime serializableDateTime = new SerializableDateTime(this.date, (int)this.season, this.year, this.hours, this.minutes, (int)this.month);
            return serializableDateTime;
        }

        public void LoadDay(SerializableDateTime serializableDateTime)
        {
            this = serializableDateTime;
        }
        #endregion
    }

    [Serializable]
    public enum Days
    {
        NULL = 0,
        Mon = 1,
        Tue = 2,
        Wed = 3,
        Thu = 4,
        Fri = 5,
        Sat = 6,
        Sun = 7
    }

    [Serializable]
    public enum Month
    {
        Enero = 0,
        Febrero = 1,
        Marzo = 2,
        Abril = 3,
        Mayo = 4,
        Junio = 5,
        Julio = 6,
        Agosto = 7,
        Septiembre = 8,
        Octubre = 9,
        Noviembre = 10,
        Diciembre = 11
    }

    [Serializable]
    public enum Season
    {
        Spring = 0,
        Summer = 1,
        Autum = 2,
        Winter = 3
    }
}