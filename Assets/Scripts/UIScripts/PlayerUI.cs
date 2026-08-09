using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] Image ammoIcon;
    [SerializeField] Sprite[] ammoSprites;
    [SerializeField] TMP_Text ammoCountTMP;
    [SerializeField] TMP_Text waveTMP;
    private PlayerManager player;
    private GameManager gameManager;
    private ShopMenu shopMenu;
    private int[] numOfShells;

    public void Init(PlayerManager playerRef, GameManager gameRef)
    {
        player = playerRef;
        gameManager = gameRef;
    }

    private void Start()
    {
        shopMenu = GetComponentInChildren<ShopMenu>();
    }
    void Update()
    {
        if (player == null || gameManager == null) { return; }
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
        waveTMP.SetText("Stage Lvl: " + gameManager.wavelvl.ToString());

        if (Input.GetKeyDown(KeyCode.R)) //TODO MAKE SURE IT DOES NOT CONFLICT WITH PAUSE SCREEN
        {
            OpenShop();
        }
    }

    public void ChangeAmmoCount(int index)
    {
        ammoCountTMP.SetText(numOfShells[index].ToString());
    }

    public void OpenShop() 
    { 
        shopMenu.isShopOpen = true;
    }
}
