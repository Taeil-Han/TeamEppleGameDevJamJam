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

        sfxSource.PlayOneShot(purchase);

    }

    public void UICancelClick()
    {
        sfxSource.PlayOneShot(UI_Cancel);
    }

    public void UiMovement()
    {
        sfxSource.PlayOneShot(UI_Move);
    }


}
