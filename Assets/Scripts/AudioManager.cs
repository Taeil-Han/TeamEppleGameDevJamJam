using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioClip UI_Confirm;
    [SerializeField] AudioClip purchase;
    [SerializeField] AudioClip UI_Cancel;
    [SerializeField] AudioClip UI_Move;

    void Awake()
    {
        Instance = this;
    }

    public void UIConfirmClick()
    {
        sfxSource.PlayOneShot(UI_Confirm);
    }

    public void PlayPurchaseSound()
    {

        sfx.Source.PlayOneShot(purchase);

    }

    public void UICancelClick()
    {
        sfx.Source.PlayOneShot(UI_Cancel);
    }

    public void UiMovement()
    {
        sfx.Source.PlayOneShot(UI_Move);
    }


}
