using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeComplete;
    public event EventHandler OnRecipeFailed;
    public event EventHandler OnRecipeSuccess;
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeSOList recipeListSO;
    private List<RecipeSO> waitingRecipeSOList;
        
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRacipesMax=4;
    private int successfullRecipesAmount;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject); // Ensure there is only one instance
            return;
        }
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
                RecipeSO waitingRecipeSO = recipeListSO.RecipeSOlist[UnityEngine.Random.Range(0, recipeListSO.RecipeSOlist.Count)];
                Debug.Log(waitingRecipeSO.Name);
                waitingRecipeSOList.Add(waitingRecipeSO);

                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
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
                    successfullRecipesAmount++;
                    waitingRecipeSOList.RemoveAt(i);
                    OnRecipeComplete?.Invoke(this, EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        //No matches found
        //Player did not deliver a correct recipe
        Debug.Log("Player did not deliver the correct recipe");
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingRecipeSOList;
    }
    public void InstanceChecking(int number)
    {
        Debug.Log("Square of number is " + number * number);
    }
    public int GetSuccessfullRecipesAmount()
    {
        return successfullRecipesAmount;
    }
}
