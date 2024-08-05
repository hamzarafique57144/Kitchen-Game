using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{
    private const string Player_Prefs_Sound_Effects_Volume = "SoundEffectsVolume";
    public static SoundManager Instance {get; private set; }
    [SerializeField] AdioCilpRefsSO audioClipRefsSO;
    float volume = 1f;
    private void Awake()
    {
        Instance = this;
        if (!PlayerPrefs.HasKey(Player_Prefs_Sound_Effects_Volume))
        {
            PlayerPrefs.SetFloat(Player_Prefs_Sound_Effects_Volume, volume);
            PlayerPrefs.Save();
        }
        volume = PlayerPrefs.GetFloat(Player_Prefs_Sound_Effects_Volume);
    }
    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        PlayerController.Instance.OnPlayerPickedSomething += PlayerController_OnPlayerPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
    }

    private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefsSO.objectDrop, baseCounter.transform.position);
    }

    private void PlayerController_OnPlayerPickedSomething(object sender, System.EventArgs e)
    {
        PlaySound(audioClipRefsSO.objectPickup, PlayerController.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliveryFail, deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliverySuccess, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip[] audiClipArray, Vector3 position, float volume = 1)
    {
        PlaySound(audiClipArray[UnityEngine.Random.Range(0, audiClipArray.Length)], position, volume);
    }
    private void PlaySound(AudioClip audiClip,Vector3 position,float volumeMultiplier =1)
    {
        AudioSource.PlayClipAtPoint(audiClip,position,volumeMultiplier*volume);
    }

    //for player sound, we can play here, we just play it in other class for learning purpose
    public void PlayFootStepsSound(Vector3 positition, float volume)
    {
        PlaySound(audioClipRefsSO.foorStep,positition,volume);
    }

    public void playCountDownSound()
    {
        PlaySound(audioClipRefsSO.warning, Vector3.zero);
    }
    public void ChangeVolume()
    {
        volume += .1f;
        if(volume> 1f)
        {
            volume = 0f;
        }
        PlayerPrefs.SetFloat(Player_Prefs_Sound_Effects_Volume, volume);
        PlayerPrefs.Save();
    }

    public void PlayWarningSound(Vector3 positon)
    {
        PlaySound(audioClipRefsSO.warning, positon);
    }
    public float GetVolume()
    {
        volume = PlayerPrefs.GetFloat(Player_Prefs_Sound_Effects_Volume);
        return volume;
    }
}
