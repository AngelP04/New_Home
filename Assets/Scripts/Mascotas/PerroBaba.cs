using Cultive;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PerroBaba : Mascota
{
    private Semilla[] plants;
    private float distance;
    public Item regadera;
    public CultiveLayer cultiveLayer;
    public override void DoSomething()
    {
        if(FindObjectsByType<Semilla>(FindObjectsSortMode.None).Length != 0)
        {
            plants = FindObjectsByType<Semilla>(FindObjectsSortMode.None);
        }
        if (plants == null) return;
        foreach (Semilla plant in plants)
        {
            if (!plant.water)
            {
                if(math.abs(transform.position.x - plant.gameObject.transform.position.x) < 1f && math.abs(transform.position.y - plant.gameObject.transform.position.y) < 1f)
                {
                    rb2D.velocity = Vector2.zero;
                    cultiveLayer.RegarPlanta(plant.transform.position, regadera);
                }
                else
                {
                    rb2D.position = Vector2.MoveTowards(transform.position, plant.gameObject.transform.position, speed * Time.deltaTime);
                }
            }

        }
    }
}
