using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter
{
    [SerializeField] FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] BurningRecipeSO[] burningRecipeSOArray;
    FryingRecipeSO fryingRecipeSO;
    BurningRecipeSO burningRecipeSO;
    float fryingTimer;
    float burningTimer;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }
    
    public enum State
    {
        idle,
        Frying,
        Fried,
        Burned,
    }
    private State state;

    private void Start()
    {
        state = State.idle;
    }
    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (state)
            {
            case State.idle:
                break;
            case State.Frying:
                
                    fryingTimer += Time.deltaTime;
                    if (fryingTimer > fryingRecipeSO.fryingMaxTime)
                    {
                        //Fried
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);
                        state = State.Fried;
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                        burningTimer = 0;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        }); 
                    }

             
                break;
            case State.Fried:
                    burningTimer += Time.deltaTime;
                    if (burningTimer > burningRecipeSO.burningMaxTime)
                    {
                        //Fried
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);
                        state = State.Burned;
                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                        {
                            state = state
                        });

                    }
                    break;
            case State.Burned:
                break;

            }
            Debug.Log(state);
        }

    }
    public override void Interact(PlayerController player)
    {
        if (!HasKitchenObject())
        {
            //There is no kitchen object here
            if (player.HasKitchenObject())
            {
                //Player is carrying somthing
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    //Player has something that can be cut
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
                    state = State.Frying;
                    fryingTimer = 0;
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                    {
                        state = state
                    });
                }

            }
            else
            {
                //Player is not carriyng anything

            }
        }
        else
        {
            //There is Kitchen Object here
            if (player.HasKitchenObject())
            {
                //Player is carrying something
            }
            else
            {
                //Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
                state = State.idle;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    state = state
                });
            }
        }
    }
    private bool HasRecipeWithInput(KitchenObjectsSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);
        return fryingRecipeSO != null;
    }
    private KitchenObjectsSO GetOutputForInput(KitchenObjectsSO inputKitchenObjectSO)
    {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inputKitchenObjectSO);

        if (fryingRecipeSO != null)
        {
            return fryingRecipeSO.output;
        }
        else
        {
            return null;
        }
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectsSO inputKitchenObjectSO)
    {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.input == inputKitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }
        return null;
    }
    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectsSO inputKitchenObjectSO)
    {
        foreach (BurningRecipeSO buriningRecipeSO in burningRecipeSOArray)
        {
            if (buriningRecipeSO.input == inputKitchenObjectSO)
            {
                return buriningRecipeSO;
            }
        }
        return null;
    }
}
