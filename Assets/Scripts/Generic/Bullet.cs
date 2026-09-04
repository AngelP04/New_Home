using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;

    private Rigidbody2D Rigidbody2D;
    public Vector2 Direction;
    public string targetTag;
    private GameObject target;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag(targetTag);
        Direction = target.transform.position - transform.position;
        Rigidbody2D.velocity = new Vector2(Direction.x, Direction.y).normalized * Speed;

    }

    public void SetDirection(Vector2 direction)
    {
        Direction = direction;
        if (Direction.x != 0)
        {
            transform.eulerAngles = new Vector2(0, transform.eulerAngles.y);
        }

        if (Direction.y != 0)
        {
            if (Direction.y > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, -90);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 90);
            }
        }
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(targetTag))
        {
            collision.GetComponent<EnemyController>().paralyzed = true;
            DestroyBullet();
        }
    }
}
