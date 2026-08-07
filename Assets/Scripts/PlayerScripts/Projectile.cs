using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 direction;
    private bool hasTarget;

    [SerializeField] float speed;
    private float lifetime = 3f;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }
    public void Init(Vector3 start, Vector3 end)
    {
        startPos = start;
        direction = (end - start).normalized;
        transform.position = startPos;
        hasTarget = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (!hasTarget) { return; }

        transform.position += direction * speed * Time.deltaTime;

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Stuff it does when it gets hit
        }
        Destroy(gameObject);
    }
}
