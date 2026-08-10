using UnityEngine;

public class Lvl2Projectile : Projectile
{
    private float chargePercent = 0.0f;
    [SerializeField] float chargeMultiplier = 5.0f;
    public void Init(Vector3 start, Vector3 end, float chargePercent)
    {
        base.Init(start, end);
        this.chargePercent = chargePercent;
    }
    
    //Right-Click Charge
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            sfxSource.PlayOneShot(sfxClip);
            //Stuff it does when it gets hit

            Destroy(gameObject);
        }
    }

    protected override void Update()
    {
        if (!hasTarget) { return; }

        transform.position += direction * speed * (1 + (chargePercent * chargeMultiplier)) * Time.deltaTime;

    }

}
