using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] Transform container;
    [SerializeField] Transform recipeTemplate;
    [SerializeField] DeliveryManager deliveryManager;
    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);

        
    }

    private void Start()
    {


        deliveryManager.OnRecipeSpawned += DeliveryManager_OnRecipeSpawned;
        deliveryManager.OnRecipeComplete += DeliveryManager_OnRecipeComplte;
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeComplte(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void DeliveryManager_OnRecipeSpawned(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach(Transform child in container)
        {
            
            if (child == recipeTemplate)
                continue;
            Destroy(child.gameObject);
        }
        foreach (RecipeSO recipeSO in deliveryManager.GetWaitingRecipeSOList())
        {
            Transform recipeTransform = Instantiate(recipeTemplate, container.transform);
            recipeTransform.gameObject.SetActive(true);
            recipeTransform.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSO);
        }
    }

}
