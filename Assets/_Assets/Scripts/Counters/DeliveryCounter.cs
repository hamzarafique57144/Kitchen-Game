using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance { get; private set; }

    private void Awake()
    {
        
            Instance = this;
        
    }
    public override void Interact(PlayerController player)
    {
        if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
        {
            //Only Accepts Plates
            DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
            player.GetKitchenObject().DestroySelf();
        }
    }
}
