using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    public GameObject bgPanel;
    [SerializeField] Sprite[] sprites = new Sprite[3];
    [SerializeField] float[] shellCosts = { 0.00f, 1.00f, 4.00f };
    [SerializeField] Image uiImage;

    public bool isShopOpen = false;
    public Color available = Color.white;
    public Color purchased = Color.gray;
    public Color lockedColor = new Color(0.35f, 0.35f, 0.35f, 1f);
    private int lvl = 0;

    //Text
    [SerializeField] TMP_Text moneyTMP;
    [SerializeField] TMP_Text button2TMP;
    [SerializeField] TMP_Text button3TMP;

    void Start()
    {
        bgPanel.SetActive(false);
        uiImage.sprite = sprites[0];
        moneyTMP.SetText("$" + ScoreManager.Instance.money.ToString());
        button2TMP.SetText("$" + shellCosts[1].ToString());
        button3TMP.SetText("$" + shellCosts[2].ToString());
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
        moneyTMP.SetText("$" + ScoreManager.Instance.money.ToString());
        ChangeBG();
    }

    public void ChangeBG() 
    {
        if (lvl == 2)
        {
            uiImage.sprite = sprites[1];
        }
        else if (lvl == 3) 
        { 
            uiImage.sprite = sprites[2]; 
        }

        //Enough Money
        if (ScoreManager.Instance.money > shellCosts[1] && lvl <= 2) 
        {
            uiImage.sprite = sprites[1];
        }
        if (ScoreManager.Instance.money > shellCosts[2] && lvl <= 3 && !(lvl <= 1))
        {
            uiImage.sprite = sprites[2];
        }
    }

    public void BuyLvl1() 
    { 
        
    }

    public void BuyLvl2() 
    { 
        
    }

    public void BuyLvl3() 
    { 
        
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

    public void MainMenu()
    {
        Time.timeScale = 1f;
        isShopOpen = false;
        SceneManager.LoadScene("MainMenu");
    }


    /*
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
    */



}
