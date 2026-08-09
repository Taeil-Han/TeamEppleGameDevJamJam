using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    public static ShopMenu Instance;
    public GameObject bgPanel;
    [SerializeField] Sprite[] sprites = new Sprite[3];
    [SerializeField] float upgradeCost1 = 0f;
    [SerializeField] float upgradeCost2 = 200f;
    [SerializeField] float upgradeCost3 = 750f;
    [SerializeField] float shellCost1 = 10f; //10 lvl 1 shells for $10
    [SerializeField] float shellCost2 = 50f; //5 lvl 2 shells for $50
    [SerializeField] float shellCost3 = 75f; //2 lvl 3 shells for $75
    [SerializeField] Image uiImage;
    [SerializeField] GameObject shopButton;

    public static bool isShopOpen = false;
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
    public int lvl = 1;

    //Text
    [SerializeField] TMP_Text moneyTMP;
    [SerializeField] TMP_Text button2TMP;
    [SerializeField] TMP_Text button3TMP;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        bgPanel.SetActive(false);
        uiImage.sprite = sprites[0];
        moneyTMP.SetText("$" + ScoreManager.Instance.money.ToString());
        button2TMP.SetText("$" + shellCost2.ToString());
        button3TMP.SetText("$" + shellCost3.ToString());
        RefreshUB();
    }

    void Update()
    {
        if (isShopOpen)
        {
            Pause();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isShopOpen) { return; }
            Resume();
        }
        if(ScoreManager.Instance.money != null)
        {
            moneyTMP.SetText("$" + ScoreManager.Instance.money.ToString());
        }
    }


    public void BuyLvl1()
    {
        if (ScoreManager.Instance.money < shellCost1) return;
        ScoreManager.Instance.SubtractMoney(shellCost1);
        PlayerManager.Instance.AddShell(0, 10);
    }

    public void BuyLvl2()
    {
        if (ScoreManager.Instance.money < shellCost2 || lvl < 2) return;
        ScoreManager.Instance.SubtractMoney(shellCost2);
        PlayerManager.Instance.AddShell(1, 5);
    }

    public void BuyLvl3()
    {
        if (ScoreManager.Instance.money < shellCost3 || lvl < 3) return;
        ScoreManager.Instance.SubtractMoney(shellCost3);
        PlayerManager.Instance.AddShell(2, 2);
    }


    public void UpgradeLvl1()
    {
        if (ScoreManager.Instance != null)
        {
            if (ScoreManager.Instance.money < upgradeCost1 || lvl != 1) return;
            ScoreManager.Instance.SubtractMoney(upgradeCost1);
            lvl = 2;
            RefreshUB();
        }
    }

    public void UpgradeLvl2()
    {
        if (ScoreManager.Instance != null)
        {
            if (ScoreManager.Instance.money < upgradeCost2 || lvl != 2) return;
            ScoreManager.Instance.SubtractMoney(upgradeCost2);
            lvl = 3;
            uiImage.sprite = sprites[1];
            PlayerManager.Instance.UnlockShell(2);
            CustomerSpawner.Instance.UnlockCustomer(2);
            RefreshUB();
        }
    }

    public void UpgradeLvl3()
    {
        if (ScoreManager.Instance != null)
        {
            if (ScoreManager.Instance.money < upgradeCost3 || lvl != 3) return;
            ScoreManager.Instance.SubtractMoney(upgradeCost3);
            lvl = 4;
            uiImage.sprite = sprites[2];
            PlayerManager.Instance.UnlockShell(3);
            CustomerSpawner.Instance.UnlockCustomer(3);
            RefreshUB();
        }
    }

    public void Resume()
    {
        bgPanel.SetActive(false);
        Time.timeScale = 1f;
        isShopOpen = false;
        shopButton.SetActive(true);
    }

    public void Pause()
    {
        bgPanel.SetActive(true);
        Time.timeScale = 0f;
        isShopOpen = true;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        isShopOpen = false;
        SceneManager.LoadScene("MainMenu");
    }

    void RefreshUB()
    {
        UBState(up1B, up1I, up1T, lvl == 1, lvl >= 2);
        UBState(up2B, up2I, up2T, lvl == 2, lvl >= 3);
        UBState(up3B, up3I, up3T, lvl == 3, lvl >= 4);
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

}