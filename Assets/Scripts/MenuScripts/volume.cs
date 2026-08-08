using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    public AudioMixer dj;
    public Slider master;
    public Slider music;
    public Slider sfx;

    void Start(){
        SetMaster(PlayerPrefs.GetFloat("MasterVolume", 1f));
        SetMusic(PlayerPrefs.GetFloat("MusicVolume", 1f));
        SetSFX(PlayerPrefs.GetFloat("SFXVolume", 1f));
    }

    public void SetMaster(float v){
        master.value = v;
        v = Mathf.Clamp(v, 0.0001f, 1f);
        dj.SetFloat("MasterVolume", Mathf.Log10(v)*20f);
        PlayerPrefs.SetFloat("MasterVolume", v);
    }

    public void SetMusic(float v){
        music.value = v;
        v = Mathf.Clamp(v, 0.0001f, 1f);
        dj.SetFloat("MusicVolume", Mathf.Log10(v)*20f);
        PlayerPrefs.SetFloat("MusicVolume", v);
    }

    public void SetSFX(float v){
        sfx.value = v;
        v = Mathf.Clamp(v, 0.0001f, 1f);
        dj.SetFloat("SFXVolume", Mathf.Log10(v)*20f);
        PlayerPrefs.SetFloat("SFXVolume", v);
    }
}
