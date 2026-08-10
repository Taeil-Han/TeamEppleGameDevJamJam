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
    private Color available = new Color(255f / 255f, 233f / 255f, 144f / 255f);
    private Color purchased = Color.gray;
    private Color lockedColor = new Color(0.35f, 0.35f, 0.35f, 1f);
    public int lvl = 1;

    //Text
    [SerializeField] TMP_Text moneyTMP;
    [SerializeField] TMP_Text button2TMP;
    [SerializeField] TMP_Text button3TMP;

    [SerializeField] GameObject lvl1Block;
    [SerializeField] GameObject lvl2Block;
    [SerializeField] GameObject lvl3Block;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        bgPanel.SetActive(false);
        uiImage.sprite = sprites[0];
        moneyTMP.SetText("$" + ScoreManager.Instance.money.ToString());
        button2TMP.SetText("$" + upgradeCost2.ToString());
        button3TMP.SetText("$" + upgradeCost3.ToString());
        RefreshUB();
    }

    void Update()
    {
        if (isShopOpen)
        {
            Pause();
        }
        if (ScoreManager.Instance != null)
        {
            moneyTMP.SetText("$" + ScoreManager.Instance.money.ToString());
        }
    }


    public void BuyLvl1()
    {
        if (ScoreManager.Instance.money < shellCost1 || lvl < 1) return;
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
            if (ScoreManager.Instance.money < upgradeCost1 || lvl != 0) return;
            ScoreManager.Instance.SubtractMoney(upgradeCost1);
            lvl = 1;
            RefreshUB();
        }
    }

    public void UpgradeLvl2()
    {
        if (ScoreManager.Instance != null)
        {
            if (ScoreManager.Instance.money < upgradeCost2 || lvl != 1) return;
            ScoreManager.Instance.SubtractMoney(upgradeCost2);
            lvl = 2;
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
            if (ScoreManager.Instance.money < upgradeCost3 || lvl != 2) return;
            ScoreManager.Instance.SubtractMoney(upgradeCost3);
            lvl = 3;
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
        UBState(up1B, up1I, up1T, lvl == 0, lvl >= 1);
        lvl1Block.SetActive(lvl==0);
        UBState(up2B, up2I, up2T, lvl == 1, lvl >= 2);
        lvl2Block.SetActive(lvl < 2);
        UBState(up3B, up3I, up3T, lvl == 2, lvl >= 3);
        lvl3Block.SetActive(lvl < 3);
    }

    void UBState(Button button, Image image, TMP_Text text, bool avail, bool bought)
    {
        button.interactable = avail;
        if (avail)
        {
            image.color = available;
            text.color = new Color(134f / 255f, 67f / 255f, 23f / 255f);
            return;
        }
        if (bought)
        {
            image.color = purchased;
            text.color = Color.gray;
            return;
        }
        image.color = lockedColor;
        text.color = Color.gray;
    }

}