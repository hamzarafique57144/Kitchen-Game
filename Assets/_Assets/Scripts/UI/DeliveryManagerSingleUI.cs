using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI recipeNameText;
    [SerializeField] Transform iconContainer;
    [SerializeField] Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    public void SetRecipeSO(RecipeSO recipeSO)
    {
        recipeNameText.text = recipeSO.Name;

    
        foreach(Transform child in iconContainer)
        {
            if (child == iconTemplate)
                continue;
            Destroy(child.gameObject);
        }
        foreach(KitchenObjectsSO kitchenObjectsSO in recipeSO.kitchenObjectSOList)
        {
            Transform iconTransfrom = Instantiate(iconTemplate, iconContainer);
            iconTransfrom.gameObject.SetActive(true);
            iconTransfrom.GetComponent<Image>().sprite = kitchenObjectsSO.sprite;
        }
    }
}

