using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{
    private const string Player_Prefs_Music_Volume="MusicVolume" ;
    public static MusicManager Instance { get; private set; }
    private AudioSource audioSource;
    float volume = .3f;
    private void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
       
        if(!PlayerPrefs.HasKey(Player_Prefs_Music_Volume))
        {
            PlayerPrefs.SetFloat(Player_Prefs_Music_Volume, .3f);
            PlayerPrefs.Save();
        }
        volume = PlayerPrefs.GetFloat(Player_Prefs_Music_Volume);
        audioSource.volume = volume;
    }
    public void ChangeVolume()
    {
        volume += .1f;
        if (volume > 1f)
        {
            volume = 0f;
        }
        audioSource.volume = volume;
      PlayerPrefs.SetFloat(Player_Prefs_Music_Volume, volume);
        PlayerPrefs.Save();
    }
    public float GetVolume()
    {
        volume = PlayerPrefs.GetFloat(Player_Prefs_Music_Volume);
        return volume;
    }
}
