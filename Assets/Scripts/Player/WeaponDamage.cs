using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<IDamageable>() != null)
        {
            collision.gameObject.GetComponent<IDamageable>().TakeDamage(damage);
        }
    }
}
