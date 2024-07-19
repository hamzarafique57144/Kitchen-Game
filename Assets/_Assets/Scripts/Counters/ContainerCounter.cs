using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter,IKitchenObjectParent
{
    [SerializeField] KitchenObjectsSO kitchenObjetcSO;
    [SerializeField] Transform counterTopPoint;
    private KitchenObject kitchenObject;
    public override void Interact(PlayerController player)
    {
        if (kitchenObject == null)
        {
            Transform kitchenObjetcTransform = Instantiate(kitchenObjetcSO.prefab, counterTopPoint);
            kitchenObjetcTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(this);
            //Debug.Log(kitchenObjetcTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().name);
        }
        else
        {
            kitchenObject.SetKitchenObjectParent(player);
        }

    }
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
