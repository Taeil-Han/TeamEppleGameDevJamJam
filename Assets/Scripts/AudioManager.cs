using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioClip buttonClickSound;

    void Awake()
    {
        Instance = this;
    }

    public void PlayButtonClick()
    {
        sfxSource.PlayOneShot(buttonClickSound);
    }
}
