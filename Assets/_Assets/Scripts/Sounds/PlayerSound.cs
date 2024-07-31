using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public static PlayerSound Instance { get; private set; }
    private PlayerController player;
    float footstepTimer;
    float footstepTimerMax = .1f;
    float volume = 1f;
    private void Awake()
    {
        Instance = this;
        player = GetComponent<PlayerController>(); 
    }
    private void Update()
    {
        footstepTimer -= Time.deltaTime;
        if(footstepTimer < 0)
        {
            footstepTimer = footstepTimerMax;
            if(player.IsWalking())
            {
                SoundManager.Instance.PlayFootStepsSound(player.transform.position, volume);
            }
             
        }
        
    }

}
