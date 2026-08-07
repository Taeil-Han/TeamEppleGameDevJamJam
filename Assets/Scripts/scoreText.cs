using UnityEngine;
using TMPro;
public class scoreText : MonoBehaviour
{
    public TextMeshProUGUI scoreT;
    void Start()
    {
        UpdateScore();
    }
    void Update()
    {
        UpdateScore();
    }
    void UpdateScore()
    {
        if (scoreManager.Instance == null)
        {
            scoreT.text = "Score: 0";
            return;
        }
        scoreT.text = "Score: " + scoreManager.Instance.score;
    }
}
