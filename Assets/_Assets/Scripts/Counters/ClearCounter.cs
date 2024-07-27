using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] KitchenObjectsSO kitchenObjetcSO;
    
    public override void Interact(PlayerController player)
    {
      if(!HasKitchenObject())
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
            if(player.HasKitchenObject())
            {
                //Player is carrying something
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject ) )
                {
                    //Player is holding a plate
                    
                    if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                  
                }
                else
                {
                    //Player is not carrying plate but something else
                    if(GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObect))
                    {
                        //Counter is holding a plate
                        if(plateKitchenObect.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                        
                    }
                }
            }
          
            else
            {
                //Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
 