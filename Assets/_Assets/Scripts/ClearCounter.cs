using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] KitchenObjectsSO kitchenObjetcSO;
    [SerializeField] Transform counterTopPoint;
    private KitchenObject kitchenObject;
    [SerializeField] ClearCounter secondCleanerCounter;
    [SerializeField] bool testing;

    private void Update()
    {
        if(testing && Input.GetKeyDown(KeyCode.T))
        {
            if(kitchenObject!=null)
            {
                kitchenObject.SetCleanerCounter(secondCleanerCounter);
              
            }
        }
    }
    public void Interact()
    {
        if(kitchenObject == null)
        {
            Transform kitchenObjetcTransform = Instantiate(kitchenObjetcSO.prefab, counterTopPoint);
            kitchenObjetcTransform.GetComponent<KitchenObject>().SetCleanerCounter(this);
            //Debug.Log(kitchenObjetcTransform.GetComponent<KitchenObject>().GetKitchenObjectSO().name);
        }
        else
        {
            Debug.Log(kitchenObject.GetClearCounter());
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
