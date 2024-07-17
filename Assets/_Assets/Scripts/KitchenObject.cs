using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] KitchenObjectsSO kitchenObjectSO;
    private ClearCounter cleanerCounter;
    public KitchenObjectsSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }
    public void SetCleanerCounter(ClearCounter cleanerCounter)
    {
        if(this.cleanerCounter!= null)
        {
            this.cleanerCounter.ClearKitchenObject();
        }
        this.cleanerCounter = cleanerCounter;
        if(cleanerCounter.HasKitchenObject())
        {
            Debug.LogError("Counter already has a kitchen counter");
        }
        cleanerCounter.SetKitchenObject(this);
        transform.parent = cleanerCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
    public ClearCounter GetClearCounter()
    {
        return cleanerCounter;
    }
   
}
