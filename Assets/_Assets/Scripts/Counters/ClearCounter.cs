using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] KitchenObjectsSO kitchenObjetcSO;
    /*[SerializeField] Transform counterTopPoint;
    private KitchenObject kitchenObject*/
    public override void Interact(PlayerController player)
    {
        /* if(kitchenObject == null)
         {
             Transform kitchenObjetcTransform = Instantiate(kitchenObjetcSO.prefab, counterTopPoint);
             kitchenObjetcTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
             //Debug.Log(kitchenObjetcTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().name);
         }
         else
         {
             kitchenObject.SetKitchenObjectParent(player);
         }

     }*/

    }
}
 