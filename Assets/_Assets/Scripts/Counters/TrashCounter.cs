using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public override void Interact(PlayerController player)
    {
        Debug.Log("Interaction with trash counter");
        if(player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();
        }
    }
}
