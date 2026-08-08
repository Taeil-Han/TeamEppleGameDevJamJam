using UnityEngine;

public class customer : MonoBehaviour
{
    [SerializeField] float speed = 1f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Store"))
        {
            Debug.Log("Robbed");
            Destroy(gameObject);
        } else if (collision.gameObject.CompareTag("PlayerBullet")) {
            if (scoreManager.Instance != null)
            {
                scoreManager.Instance.AddScore(5);
                scoreManager.Instance.AddMoney(1.50f);
                Debug.Log(scoreManager.Instance.score);
            }
            Destroy(gameObject);
        }
            
    }

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
        if (transform.position.y < Camera.main.ViewportToWorldPoint(new Vector3(0,0,0)).y)
            Destroy(gameObject);
    }

}
