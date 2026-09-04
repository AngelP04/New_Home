using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AttackZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Detecta si un enemigo esta cercano al Mascota y lo coloca como objetivo//
        if(collision != null)
        {
            if (collision.gameObject.tag.Equals("Enemy"))
            {
                GetComponentInParent<Mascota>().Attacking = true;
                GetComponentInParent<Mascota>().timeBetweenAttacksCounter = 0;
                GetComponentInParent<Mascota>().target = collision.gameObject.transform;
            }
        }

    }

}
