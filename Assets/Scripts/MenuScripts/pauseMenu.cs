using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class pauseMenu : MonoBehaviour
{
    public GameObject bgPanel;
    public static bool isPaused = false;
    public float[] shellCosts = { 15.00f, 30.00f, 45.00f };
    public Button up1B;
    public Button up2B;
    public Button up3B;
    public Image up1I;
    public Image up2I;
    public Image up3I;
    public TMP_Text up1T;
    public TMP_Text up2T;
    public TMP_Text up3T;
    public Color available = Color.white;
    public Color purchased = Color.gray;
    public Color lockedColor = new Color(0.35f, 0.35f, 0.35f, 1f);
    private int lvl = 0;

    void Start()
    {
        bgPanel.SetActive(false);
        RefreshUB();
    }

    public void Up1()
    {
        if (scoreManager.Instance != null)
        {
            if (scoreManager.Instance.money < shellCosts[0] || lvl != 0) return;
            scoreManager.Instance.SubtractMoney(shellCosts[0]);
            lvl = 1;
            RefreshUB();
        }
    }

    public void Up2()
    {
        if (scoreManager.Instance != null)
        {
            if (scoreManager.Instance.money < shellCosts[1] || lvl != 1) return;
            scoreManager.Instance.SubtractMoney(shellCosts[1]);
            lvl = 2;
            RefreshUB();
        }
    }

    public void Up3()
    {
        if (scoreManager.Instance != null)
        {
            if (scoreManager.Instance.money < shellCosts[2] || lvl != 2) return;
            scoreManager.Instance.SubtractMoney(shellCosts[2]);
            lvl = 3;
            RefreshUB();
        }
    }

    void RefreshUB()
    {
        UBState(up1B, up1I, up1T, lvl == 0, lvl >= 1);
        UBState(up2B, up2I, up2T, lvl == 1, lvl >= 2);
        UBState(up3B, up3I, up3T, lvl == 2, lvl >= 3);
    }

    void UBState(Button button, Image image, TMP_Text text, bool avail, bool bought)
    {
        button.interactable = avail;
        if (avail)
        {
            image.color = available;
            text.color = Color.black;
            return;
        }
        if (bought)
        {
            image.color = purchased;
            text.color = Color.white;
            return;
        }
        image.color = lockedColor;
        text.color = Color.gray;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }
    public void Resume()
    {
        bgPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void Pause()
    {
        bgPanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void MainMenu()
    {
        Time.timeScale = 1f;
        isPaused = false;
        SceneManager.LoadScene("MainMenu");
    }
}
