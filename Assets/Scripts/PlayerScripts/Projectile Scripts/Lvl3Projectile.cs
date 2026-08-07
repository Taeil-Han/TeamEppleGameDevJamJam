using UnityEngine;

public class Lvl3Projectile : Projectile
{
    //Boomerang 50% chance
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Stuff it does when it gets hit
        }
        Destroy(gameObject);
    }

}
