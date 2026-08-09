using UnityEngine;
using TMPro;
using System.Collections;

public class Customer : MonoBehaviour
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
            //robber
            if (ScoreManager.Instance != null && ShopMenu.Instance != null) 
            {
                if (ShopMenu.Instance.lvl == 1)
                {
                    ScoreManager.Instance.SubtractScore(2);
                    ScoreManager.Instance.SubtractMoney(2.50f);
                }
                else if (ShopMenu.Instance.lvl == 2)
                {
                    ScoreManager.Instance.SubtractScore(5);
                    ScoreManager.Instance.SubtractMoney(5.00f);
                }
                else if (ShopMenu.Instance.lvl == 3)
                {
                    ScoreManager.Instance.SubtractScore(15);
                    ScoreManager.Instance.SubtractMoney(15.00f);
                }
                else if (ShopMenu.Instance.lvl == 4)
                {
                    ScoreManager.Instance.SubtractScore(25);
                    ScoreManager.Instance.SubtractMoney(25.00f);
                }
            }
            Destroy(gameObject);
        } else if (collision.gameObject.CompareTag("Clam") || collision.gameObject.CompareTag("Cone") || collision.gameObject.CompareTag("Sundial")) {
            shootable = false;
            if (collision.gameObject.CompareTag(order))
            {
                //Correct
                if (ScoreManager.Instance != null && ShopMenu.Instance != null)
                {
                    if(ShopMenu.Instance.lvl == 1)
                    {
                        ScoreManager.Instance.AddScore(5);
                        ScoreManager.Instance.AddMoney(5.00f);
                    } else if (ShopMenu.Instance.lvl == 2)
                    {
                        ScoreManager.Instance.AddScore(10);
                        ScoreManager.Instance.AddMoney(10.00f);
                    }
                    else if (ShopMenu.Instance.lvl == 3)
                    {
                        ScoreManager.Instance.AddScore(30);
                        ScoreManager.Instance.AddMoney(30.00f);
                    }
                    else if (ShopMenu.Instance.lvl == 4)
                    {
                        ScoreManager.Instance.AddScore(50);
                        ScoreManager.Instance.AddMoney(50.00f);
                    }
                    
                }
                StartCoroutine(Thankful());

            }
            else
            {
                //Penalty
                if (ScoreManager.Instance != null && ShopMenu.Instance != null)
                {
                    if (ShopMenu.Instance.lvl == 1)
                    {
                        ScoreManager.Instance.SubtractScore(2);
                        ScoreManager.Instance.SubtractMoney(2.50f);
                    }
                    else if (ShopMenu.Instance.lvl == 2)
                    {
                        ScoreManager.Instance.SubtractScore(5);
                        ScoreManager.Instance.SubtractMoney(5.00f);
                    }
                    else if (ShopMenu.Instance.lvl == 3)
                    {
                        ScoreManager.Instance.SubtractScore(15);
                        ScoreManager.Instance.SubtractMoney(15.00f);
                    }
                    else if (ShopMenu.Instance.lvl == 4)
                    {
                        ScoreManager.Instance.SubtractScore(25);
                        ScoreManager.Instance.SubtractMoney(25.00f);
                    }

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
