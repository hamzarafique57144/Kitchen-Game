using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    public event EventHandler OnPlateSpawned;
    [SerializeField] KitchenObjectsSO platerKitchenObjectSO;
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

}
