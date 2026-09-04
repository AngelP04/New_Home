using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using LitJson;
using UnityEngine.Tilemaps;

public class WeatherChange : MonoBehaviour
{
    private int actualWeather;
    [SerializeField] DigitalRuby.RainMaker.RainScript2D rainMaker;
    private int estacionActual = 0;
    private int estacionSiguiente = 1;
    private int estacionAnterior = -1;
    [SerializeField] private float tiempoCiclo;
    private float tiempoActual = 0;
    public Tilemap[] estaciones;
    void Start()
    {
        //StartCoroutine(GetWeather());
        CambiarEstacion();
    }

    private void WeatherChanger()
    {
        if(actualWeather >= 22 && actualWeather < 300)
        {
            rainMaker.RainIntensity += 1;
        }
        else if(actualWeather >= 300 && actualWeather < 400)
        {
            rainMaker .RainIntensity += 0.2f;
        }
        else
        {
            rainMaker.RainIntensity = 0;
        }
    }


    IEnumerator GetWeather()
    {
        UnityWebRequest www = UnityWebRequest.Get("https://api.openweathermap.org/data/2.5/weather?q=caracas&appid=685c67d44cab4043ec9dc4200ba43784");
        yield return www.SendWebRequest();

        if(www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            actualWeather = 800;
        }
        else
        {
            JsonData jsonData = JsonMapper.ToObject(www.downloadHandler.text);
            actualWeather = (int)jsonData["weather"][0]["id"];
        }
        WeatherChanger();
        StopCoroutine(GetWeather());
    }

    private void Update()
    {
        tiempoActual += Time.deltaTime;
        if (tiempoActual >= tiempoCiclo)
        {

            tiempoActual = 0;
            estacionAnterior = estacionActual;
            estacionActual = estacionSiguiente;
            if (estacionSiguiente >= estaciones.Length - 1)
            {
                estacionSiguiente = 0;
            }
            else
            {
                estacionSiguiente += 1;
            }
        }
        if (!rainMaker.Camera)
        {
            rainMaker.Camera = FindObjectOfType<Camera>();
        }
        CambiarEstacion();
    }

    private void CambiarEstacion()
    {
        if(estacionAnterior >= 0)
        {
            estaciones[estacionAnterior].gameObject.SetActive(false);
        }
        estaciones[estacionActual].gameObject.SetActive(true);
    }

}
