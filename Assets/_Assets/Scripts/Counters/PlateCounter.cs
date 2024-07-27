using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;
    [SerializeField] KitchenObjectsSO plateKitchenObjectSO;
    private float spawnTimer;
    private float spawnPlateTimerMax=4;
    private int plateSpawnedAmount;
    private int plateSpwanedAmountMax = 4;
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer > spawnPlateTimerMax )
        {
            spawnTimer = 0;

            if(plateSpawnedAmount< plateSpwanedAmountMax )
            {
                plateSpawnedAmount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    public override void Interact(PlayerController player)
    {
        if (!player.HasKitchenObject())
        {
            //Player is empty handed
            if(plateSpawnedAmount > 0)
            {
                //There atleast one plate on the counter
                plateSpawnedAmount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);


            }
        }
    }
}
