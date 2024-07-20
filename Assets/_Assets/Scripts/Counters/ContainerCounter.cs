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
            Transform kitchenObjetcTransform = Instantiate(kitchenObjetcSO.prefab);
            kitchenObjetcTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
            
            OnPlayerGrabedObject?.Invoke(this, EventArgs.Empty);

        }
        

    }
    
}
