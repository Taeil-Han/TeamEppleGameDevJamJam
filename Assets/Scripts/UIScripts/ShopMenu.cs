using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopMenu : MonoBehaviour
{
    public GameObject bgPanel;
    public static bool isShopOpen = false;
    [SerializeField] float[] shellCosts = { 1.00f, 2.00f, 3.00f };
    [SerializeField] Button up1B;
    [SerializeField] Button up2B;
    [SerializeField] Button up3B;
    [SerializeField] Image up1I;
    [SerializeField] Image up2I;
    [SerializeField] Image up3I;
    [SerializeField] TMP_Text up1T;
    [SerializeField] TMP_Text up2T;
    [SerializeField] TMP_Text up3T;
    public Color available = Color.white;
    public Color purchased = Color.gray;
    public Color lockedColor = new Color(0.35f, 0.35f, 0.35f, 1f);
    private int lvl = 0;

    void Start()
    {

        bgPanel.SetActive(false);
        RefreshUB();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isShopOpen)
                Resume();
            else
                Pause();
        }
    }

    public void Up1()
    {
        if (ScoreManager.Instance != null)
        {
            if (ScoreManager.Instance.money < shellCosts[0] || lvl != 0) return;
            ScoreManager.Instance.SubtractMoney(shellCosts[0]);
            lvl = 1;
            RefreshUB();
        }
    }

    public void Up2()
    {
        if (ScoreManager.Instance != null)
        {
            if (ScoreManager.Instance.money < shellCosts[1] || lvl != 1) return;
            ScoreManager.Instance.SubtractMoney(shellCosts[1]);
            lvl = 2;
            RefreshUB();
        }
    }

    public void Up3()
    {
        if (ScoreManager.Instance != null)
        {
            if (ScoreManager.Instance.money < shellCosts[2] || lvl != 2) return;
            ScoreManager.Instance.SubtractMoney(shellCosts[2]);
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


    public void Resume()
    {
        bgPanel.SetActive(false);
        Time.timeScale = 1f;
        isShopOpen = false;
    }
    public void Pause()
    {
        bgPanel.SetActive(true);
        Time.timeScale = 0f;
        isShopOpen = true;
    }
}
