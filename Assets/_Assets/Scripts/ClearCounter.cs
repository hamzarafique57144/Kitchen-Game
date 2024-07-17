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
                Debug.Log(kitchenObject.GetClearCounter());
            }
        }
    }
    public void Interact()
    {
        if(kitchenObject == null)
        {
            Transform kitchenObjetcTransform = Instantiate(kitchenObjetcSO.prefab, counterTopPoint);
            kitchenObjetcSO.prefab.localPosition = Vector3.zero;
            kitchenObject = kitchenObjetcTransform.GetComponent<KitchenObject>();
            kitchenObject.SetCleanerCounter(this);
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
}
