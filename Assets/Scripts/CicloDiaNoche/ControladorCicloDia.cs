using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ControladorCicloDia : MonoBehaviour
{
    [SerializeField] private Light2D globalLight;
    [SerializeField] private CicloDia[] cicloDia;
    [SerializeField] private float tiempoCiclo;
    private float tiempoActual = 0;
    private float porcentajeCiclo;
    private int cicloActual = 0;
    private int cicloSiguiente = 1;
    private void Start()
    {
        globalLight.color = cicloDia[0].colorCiclo;
    }

    private void Update()
    {
        tiempoActual = GameManager.instance.decimalMinutes;
        porcentajeCiclo = tiempoActual / tiempoCiclo;

        if(tiempoActual >= tiempoCiclo)
        {
            tiempoActual = 0;
            cicloActual = cicloSiguiente;
            if(cicloSiguiente + 1 > cicloDia.Length - 1)
            {
                cicloSiguiente = 0;
            }
            else
            {
                cicloSiguiente += 1;
            }
        }
        CambiarColor(cicloDia[cicloActual].colorCiclo, cicloDia[cicloSiguiente].colorCiclo);
    }

    private void CambiarColor(Color colorActual, Color colorSiguiente)
    {
        globalLight.color = Color.Lerp(colorActual, colorSiguiente, porcentajeCiclo);
    }
}
