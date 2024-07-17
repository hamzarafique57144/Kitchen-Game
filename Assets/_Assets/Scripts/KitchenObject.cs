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
        this.cleanerCounter = cleanerCounter;
        transform.parent = cleanerCounter.GetKitchenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }
    public ClearCounter GetClearCounter()
    {
        return cleanerCounter;
    }
}
