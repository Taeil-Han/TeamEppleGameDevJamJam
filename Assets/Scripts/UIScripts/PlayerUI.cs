using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Image ammoIcon;
    [SerializeField] Sprite[] ammoSprites;
    [SerializeField] TMP_Text ammoCountTMP;
    [SerializeField] TMP_Text waveTMP;
    [SerializeField] GameObject shopButton;
    [SerializeField] TMP_Text scoreTMP;
    [SerializeField] TMP_Text moneyTMP;
    private PlayerManager player;
    private GameManager gameManager;
    private int[] numOfShells;

    public void Init(PlayerManager playerRef, GameManager gameRef)
    {
        player = playerRef;
        gameManager = gameRef;
    }

    private void Start()
    {
        
    }
    void Update()
    {
        if (player == null || gameManager == null) { return; }
        if (Input.GetKeyDown(KeyCode.E) && !PauseMenu1.isPaused)
        {
            if (ShopMenu.isShopOpen)
            {
                ShopMenu.Instance.Resume();
                shopButton.SetActive(true);
            }
            else
            {
                OpenShop();
            }
        }
        int index = player.GetAmmoIndex();
        if (index >= 0 && index < ammoSprites.Length)
        {
            ammoIcon.sprite = ammoSprites[index];
        }
        numOfShells = player.GetAmmoCount();
        if (numOfShells != null && index >= 0 && index < numOfShells.Length)
        {
            ChangeAmmoCount(index);
        }
        scoreTMP.SetText(ScoreManager.Instance.score.ToString());
        moneyTMP.SetText("$" + ScoreManager.Instance.money.ToString());
        waveTMP.SetText("Stage Lvl: " + gameManager.wavelvl.ToString());
    }

    public void ChangeAmmoCount(int index)
    {
        ammoCountTMP.SetText(numOfShells[index].ToString());
    }

    public void OpenShop() 
    {
        if (ShopMenu.isShopOpen) { return; }
        ShopMenu.isShopOpen = true;
        shopButton.SetActive(false);
    }
}
