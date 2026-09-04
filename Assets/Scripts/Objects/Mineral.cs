using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mineral : MonoBehaviour, IDamageable
{
    public Item item;
    public Herramienta tool;
    public GameObject itemDropped;
    public int golpes;

    public void TakeDamage(int dano)
    {
        DropItem();
        golpes -= dano;
        if(golpes <= 0 )
        {
            Destroy(gameObject);
        }
        return;
    }

    public void DropItem()
    {
        GameObject drop = Instantiate(itemDropped, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        drop.GetComponent<ItemDropped>().item = item;
    }
}

public enum Herramienta
{
    Hacha,
    Pico,
    Pala
}
