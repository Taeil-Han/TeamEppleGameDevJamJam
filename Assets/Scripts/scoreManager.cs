using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    public int score = 0;
    public float money = 0f;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void AddScore(int a)
    {
        score += a;
    }
    public void ResetScore()
    {
        score = 0;
    }
    public void SubtractScore(int a)
    {
        score -= a;
    }
    public void AddMoney(float a)
    {
        money += a;
    }
    public void ResetMoney()
    {
        money = 0f;
    }
    public void SubtractMoney(float a)
    {
        money -= a;
    }
}
