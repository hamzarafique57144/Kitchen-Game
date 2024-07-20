using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    public override void Interact(PlayerController player)
    {
        if (!HasKitchenObject())
        {
            //There is no kitchen object here
            if (player.HasKitchenObject())
            {
                //Player is carrying somthing
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                //Player is not carriyng anything

            }
        }
        else
        {
            //There is Kitchen Object here
            if (player.HasKitchenObject())
            {
                //Player is carrying something
            }
            else
            {
                //Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
    public override void InteractAlternate(PlayerController player)
    {
        if(HasKitchenObject())
        {
            //There is a kitchen object here
            GetKitchenObject().DestroySelf();
        }

    }
}

