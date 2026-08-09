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
    private int[] numOfShells;

    public void Init(PlayerManager playerRef, GameManager gameRef)
    {
        player = playerRef;
        gameManager = gameRef;
    }

    void Update()
    {
        if (player == null) return;
        int index = player.GetAmmoIndex();
        if (index >= 0 && index < ammoSprites.Length)
        {
            ammoIcon.sprite = ammoSprites[index];
        }
        numOfShells = player.GetAmmoCount();
        ChangeAmmoCount(index);
        waveTMP.SetText("Stage Lvl: " + gameManager.wavelvl.ToString());
    }

    public void ChangeAmmoCount(int index)
    {
        ammoCountTMP.SetText(numOfShells[index].ToString());
    }
}
