using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabedObject;
    [SerializeField] KitchenObjectsSO kitchenObjetcSO;
    
    public override void Interact(PlayerController player)
    {
        if (!player.HasKitchenObject())
        {
            //Player is not carrting anything
            KitchenObject.SpawnKitchenObject(kitchenObjetcSO, player);
            OnPlayerGrabedObject?.Invoke(this, EventArgs.Empty);

        }
        

    }
    
}
