using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    [SerializeField] AudioMixer dj;
    [SerializeField] Slider master;
    [SerializeField] Slider music;
    [SerializeField] Slider sfx;

    void Start(){
        float masterV = PlayerPrefs.GetFloat("MasterVolume", 1f);
        float musicV = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfxV = PlayerPrefs.GetFloat("SFXVolume", 1f);
        if (master != null)
        {
            master.value = masterV;
            master.onValueChanged.RemoveAllListeners();
            master.onValueChanged.AddListener(SetMaster);
        }
        if (music != null)
        {
            music.value = musicV;
            music.onValueChanged.RemoveAllListeners();
            music.onValueChanged.AddListener(SetMusic);
        }
        if (sfx != null)
        {
            sfx.value = sfxV;
            sfx.onValueChanged.RemoveAllListeners();
            sfx.onValueChanged.AddListener(SetSFX);
        }
        SetMaster(masterV);
        SetMusic(musicV);
        SetSFX(sfxV);
    }

    public void SetMaster(float v){
        v = Mathf.Clamp(v, 0.0001f, 1f);
        if(master != null) master.value = v;
        if(dj != null) dj.SetFloat("MasterVolume", Mathf.Log10(v)*20f);
        PlayerPrefs.SetFloat("MasterVolume", v);
    }

    public void SetMusic(float v){
        v = Mathf.Clamp(v, 0.0001f, 1f);
        if(music!=null) music.value = v;
        if (dj != null) dj.SetFloat("MusicVolume", Mathf.Log10(v)*20f);
        PlayerPrefs.SetFloat("MusicVolume", v);
    }

    public void SetSFX(float v){
        v = Mathf.Clamp(v, 0.0001f, 1f);
        if(sfx!=null)sfx.value = v;
        if (dj != null) dj.SetFloat("SFXVolume", Mathf.Log10(v)*20f);
        PlayerPrefs.SetFloat("SFXVolume", v);
    }
}
