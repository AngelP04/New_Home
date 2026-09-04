using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;

public class GanchorrionAttack : MonoBehaviour
{
    public float speed, timeBetweenAttacks, timeToattrack, timeToAttackCounter;
    public Transform targetPosition;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Atrapa al enemigo y espera unos segundos para atraerlo//
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            timeToAttackCounter = 2;
            collision.GetComponent<EnemyController>().trapped = true;
            GetComponentInParent<Mascota>().target = collision.transform;
            GetComponentInParent<Mascota>().animator.speed = 0;

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Atrae al enemigo//
        if (collision.gameObject.tag.Equals("Enemy"))
        {
            timeToAttackCounter -= Time.deltaTime;
            if (timeToAttackCounter < 0)
            {
                GetComponentInParent<Mascota>().animator.speed = 1;
                GetComponentInParent<Mascota>().target.GetComponent<Rigidbody2D>().position = Vector2.MoveTowards(GetComponentInParent<Mascota>().target.GetComponent<Transform>().position, targetPosition.position, speed * Time.deltaTime);
                timeToAttackCounter = 2;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.tag.Equals("Enemy"))
        {
            collision.GetComponent<EnemyController>().trapped = false;
        }
            
    }

}
