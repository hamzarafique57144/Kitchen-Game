using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    [SerializeField] List<KitchenObjectsSO> validKitchenObjectSOList ;
    private List<KitchenObjectsSO> kitchenObjectSOList;

    private void Awake()
    {
        kitchenObjectSOList= new List<KitchenObjectsSO>();
    }
    public bool TryAddIngredient(KitchenObjectsSO kitchenObjectSO)
    {
        if(!validKitchenObjectSOList.Contains(kitchenObjectSO))
        {
            //Not a valid Ingredient
            return false;
        }
        if(kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            //Already has this type
            return false;
        }
        else
        {
           kitchenObjectSOList.Add(kitchenObjectSO);
            return true;
        }
        
    }
}
