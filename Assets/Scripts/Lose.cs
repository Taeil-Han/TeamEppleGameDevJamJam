using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lose : MonoBehaviour
{
    [SerializeField] TMP_Text scoreTMP;

    void Update()
    {
        scoreTMP.SetText(ScoreManager.Instance.score.ToString());
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
