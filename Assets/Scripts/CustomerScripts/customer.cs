using UnityEngine;
using TMPro;
using System.Collections;

public class customer : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    public SpriteRenderer spriteRenderer;
    public TMP_Text opinion;
    public float speechDuration = 0.5f;
    public string order = "Clam";
    private bool shootable = true;


    void Start()
    {
        opinion.text = "";
        opinion.gameObject.SetActive(false);
        if(spriteRenderer != null)
        {
            if (order == "Clam")
                spriteRenderer.color = Color.magenta;
            else if (order == "Cone")
                spriteRenderer.color = Color.cyan;
            else if (order == "Sundial")
                spriteRenderer.color = Color.yellow;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!shootable) return;
        if (collision.gameObject.CompareTag("Store"))
        {
            shootable = false;
            if (scoreManager.Instance != null)
            {
                scoreManager.Instance.SubtractScore(7);
                scoreManager.Instance.SubtractMoney(2.00f);
                Debug.Log(scoreManager.Instance.score);
            }
            Destroy(gameObject);
        } else if (collision.gameObject.CompareTag("Clam") || collision.gameObject.CompareTag("Cone") || collision.gameObject.CompareTag("Sundial")) {
            shootable = false;
            if (collision.gameObject.CompareTag(order))
            {
                //Correct
                if (scoreManager.Instance != null)
                {
                    scoreManager.Instance.AddScore(5);
                    scoreManager.Instance.AddMoney(1.50f);
                    Debug.Log(scoreManager.Instance.score);
                }
                StartCoroutine(Thankful());

            }
            else
            {
                //Penalty
                if (scoreManager.Instance != null)
                {
                    scoreManager.Instance.SubtractScore(5);
                    scoreManager.Instance.SubtractMoney(1.00f);
                    Debug.Log(scoreManager.Instance.score);
                }
                StartCoroutine(Hater());

            }
        }
            
    }

    void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
        if (transform.position.y < Camera.main.ViewportToWorldPoint(new Vector3(0,0,0)).y)
            Destroy(gameObject);
    }

    IEnumerator Thankful()
    {
        opinion.text = "Thnx";
        opinion.gameObject.SetActive(true);
        speed = 0;
        yield return new WaitForSeconds(speechDuration/2);
        speed = -1f;
        yield return new WaitForSeconds(speechDuration / 2);
        Destroy(gameObject);
    }

    IEnumerator Hater()
    {
        opinion.text = ">:(";
        opinion.gameObject.SetActive(true);
        speed = 0;
        yield return new WaitForSeconds(speechDuration / 2);
        speed = -1f;
        yield return new WaitForSeconds(speechDuration / 2);
        Destroy(gameObject);
    }

}
