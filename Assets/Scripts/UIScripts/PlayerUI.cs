using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public static PlayerUI Instance;

    [SerializeField] Image ammoIcon;
    [SerializeField] Sprite[] ammoSprites;
    [SerializeField] TMP_Text ammoCountTMP;
    [SerializeField] TMP_Text waveTMP;
    [SerializeField] GameObject shopButton;
    [SerializeField] TMP_Text scoreTMP;
    [SerializeField] TMP_Text moneyTMP;
    [SerializeField] TMP_Text Ammo1TMP;
    [SerializeField] TMP_Text Ammo2TMP;
    [SerializeField] TMP_Text Ammo3TMP;
    [SerializeField] Image reactionImage;
    [SerializeField] Sprite normalReactionSprite;
    [SerializeField] Sprite hitReactionSprite;
    private PlayerManager player;
    private GameManager gameManager;
    private int[] numOfShells;

    public void Init(PlayerManager playerRef, GameManager gameRef)
    {
        player = playerRef;
        gameManager = gameRef;
    }

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {

    }
    void Update()
    {
        if (player == null || gameManager == null) { return; }
        if (Input.GetKeyDown(KeyCode.E) && !PauseMenu1.isPaused && !DialogueManager.isDialogueActive)
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
        Ammo1TMP.SetText(numOfShells[0].ToString());
        Ammo2TMP.SetText(numOfShells[1].ToString());
        Ammo3TMP.SetText(numOfShells[2].ToString());
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
    public void FlashReactionImage(bool wasCorrectHit)
    {
        StopCoroutine(nameof(ReactionFlash));
        StartCoroutine(ReactionFlash());
    }

    IEnumerator ReactionFlash()
    {
        reactionImage.sprite = hitReactionSprite;
        yield return new WaitForSeconds(0.5f);
        reactionImage.sprite = normalReactionSprite;
    }
}
