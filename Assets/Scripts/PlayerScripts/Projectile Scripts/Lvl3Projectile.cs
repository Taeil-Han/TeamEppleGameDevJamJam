using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Lvl3Projectile : Projectile
{
    private Transform target;
    private bool hasHit = false;
    private float turnSpeed = 1000;
    //Boomerang 50% chance
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !hasHit)
        {
            //Stuff it does when it gets hit
            if (Random.value < 0.5f)
            {
                hasHit = true;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.CompareTag("Player") && hasHit)
        {
            PlayerManager player = collision.gameObject.GetComponent<PlayerManager>();
            if (player != null)
            {
                player.AddShell(2, 1);
            }
            Destroy(gameObject);
        }
    }

    protected override void Update()
    {
        base.Update();
        if (hasHit) 
        {
            if (!hasTarget) return;

            if (target != null)
            {
                Vector3 desiredDirection = (target.position - transform.position).normalized;

                direction = Vector3.RotateTowards(direction, desiredDirection, turnSpeed * Mathf.Deg2Rad * Time.deltaTime, 0f);
            }

            transform.position += direction * speed * Time.deltaTime;
        }
    }


}
