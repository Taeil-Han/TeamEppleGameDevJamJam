using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Vector3 startPos;
    protected Vector3 direction;
    protected bool hasTarget;

    [SerializeField] protected float speed;
    [SerializeField] protected float lifetime = 3f;
    protected virtual void Start()
    {
        Destroy(gameObject, lifetime);
    }
    public virtual void Init(Vector3 start, Vector3 end)
    {
        startPos = start;
        direction = (end - start).normalized;
        transform.position = startPos;
        hasTarget = true;
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        if (!hasTarget) { return; }

        transform.position += direction * speed * Time.deltaTime;

    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Stuff it does when it gets hit
            Destroy(gameObject);
        }
    }
}
