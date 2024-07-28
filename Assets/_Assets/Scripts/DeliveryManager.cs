using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeSOList recipeListSO;
    private List<RecipeSO> waitingRecipeSOList
        ;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRacipesMax=4;
    private void Awake()
    {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }
    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;

        if(spawnRecipeTimer<=0)
        {
            if(waitingRecipeSOList.Count < waitingRacipesMax)
            {
                spawnRecipeTimer = spawnRecipeTimerMax;
                RecipeSO waitingRecipeSO = recipeListSO.RecipeSOlist[Random.Range(0, recipeListSO.RecipeSOlist.Count)];
                Debug.Log(waitingRecipeSO.Name);
                waitingRecipeSOList.Add(waitingRecipeSO);
            }
           
        }
    }
    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for(int i =0; i < waitingRecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];
            if(waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                bool plateContentsMatchesRecipe = true;
                //Has the same number of ingredients 
                foreach(KitchenObjectsSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    //Cycling through all ingredients in the Recipe
                    bool ingredientFound = false;
                    foreach(KitchenObjectsSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        //Cycling through all ingredients in the Recipe
                        if (plateKitchenObjectSO == recipeKitchenObjectSO)
                        {
                            //Ingrients Matches
                            ingredientFound = true;
                            break;
                        }
                    }
                    if(!ingredientFound)
                    {
                        //This recipe ingredients are not found on the plate
                        plateContentsMatchesRecipe = false;
                    }
                }
                if(plateContentsMatchesRecipe)
                {
                    //Player deliver the correct recipe
                    Debug.Log("Player deliver the correct recipe");
                    waitingRecipeSOList.RemoveAt(i);
                    return;
                }
            }
        }
        //No matches found
        //Player did not deliver a correct recipe
        Debug.Log("Player did not deliver the correct recipe");
    }
}
