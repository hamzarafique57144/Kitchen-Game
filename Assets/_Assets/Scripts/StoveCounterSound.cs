using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] StoveCounter stoveCounter;
    private AudioSource audioSource;
    bool playWarningSound;
    float warningSounTimer;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventsArgs e)
    {
        float burnShowProgressAmount = 0.5f;
         playWarningSound = stoveCounter.IsFried() && e.progressNormalized > burnShowProgressAmount;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e)
    {
        bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        if(playSound)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }

    private void Update()
    {
        if(playWarningSound)
        {
            warningSounTimer -= Time.deltaTime;
            float warningSoundTimerMax = 0.5f;
            if(warningSounTimer<0)
            {
                warningSounTimer = warningSoundTimerMax;
                SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
            }
        }
    }
}
