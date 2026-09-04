using SuperTiled2Unity.Editor.ClipperLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Player"))
        {
            collision.gameObject.GetComponent<HealthManager>().TakeDamage(damage);
        }
    }
}
