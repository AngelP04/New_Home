using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pochu : Mascota
{
    public GameObject bulletPre;
    public float lastShoot;
    // Start is called before the first frame update
    public override void DoSomething()
    {
        if(Attacking && Time.time > lastShoot + 2f)
        {
            Shoot();
            lastShoot = Time.time;
        }
    }

    private void Shoot()
    {
        if(target != null)
        {
            Vector2 pos = new Vector2(transform.position.x, transform.position.y);

            Instantiate(bulletPre, pos, Quaternion.identity);
        }

    }
}
