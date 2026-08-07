using UnityEngine;

public class customer : MonoBehaviour
{
    public float speed = 5f;

    private void OnTriggerEnter2D(Collider2D obj) {
        if (obj.CompareTag("Store"))
            Debug.Log("Robbed");
            Destroy(gameObject);
    }

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
        if (transform.position.y < Camera.main.ViewportToWorldPoint(new Vector3(0,0,0)).y)
            Destroy(gameObject);
    }
}
