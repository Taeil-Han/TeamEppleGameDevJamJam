using UnityEngine;

public class Lvl2Projectile : Projectile
{
    //Right-Click Charge
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Stuff it does when it gets hit
            Destroy(gameObject);
        }
    }
}
