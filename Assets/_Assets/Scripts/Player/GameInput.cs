using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance { get; private set; }
    private PlayerInputActions playerInputActions;
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;
    public event EventHandler OnPauseAction;
    private const string Player_Prefs_Bindings = "InputBindings";

    public event EventHandler OnBindingRebind;
    public enum Binding
    {
        Move_Up,
        Move_Down,
        Move_Left,
        Move_Right,
        Interact,
        InteractAlternate,
        Pause,
    }
    private void Awake()
    {
        Instance = this;
        playerInputActions = new PlayerInputActions();

        if (PlayerPrefs.HasKey(Player_Prefs_Bindings))
        {
            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(Player_Prefs_Bindings));
        }
        playerInputActions.Enable();
        playerInputActions.PlayerController.Interact.performed += Interact_performed;
        playerInputActions.PlayerController.InteractAlternate.performed += InteractAlternate_Performed;
        playerInputActions.PlayerController.Pause.performed += Pause_performed;


    }
    private void OnDestroy()
    {
        playerInputActions.PlayerController.Interact.performed -= Interact_performed;
        playerInputActions.PlayerController.InteractAlternate.performed -= InteractAlternate_Performed;
        playerInputActions.PlayerController.Pause.performed -= Pause_performed;
        playerInputActions.Dispose();
    }
    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        ;
        OnPauseAction?.Invoke(this, EventArgs.Empty);
    }
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        /* if(OnInteractAction != null)
         {
             OnInteractAction(this, EventArgs.Empty);
         }*/
        
        OnInteractAction?.Invoke(this, EventArgs.Empty);//this dose Same fuction as above if statement
       
    }
    private void InteractAlternate_Performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }
    public Vector2 GetMovementVector()
    {
          Vector2 inputVector = playerInputActions.PlayerController.Move.ReadValue<Vector2>();

         /* *//*Vector2 inputVector =new Vector2(0f,0f);
          if (Input.GetKey(KeyCode.W))
              inputVector.y = +1;
          if (Input.GetKey(KeyCode.S))
              inputVector.y = -1;
          if (Input.GetKey(KeyCode.A))
              inputVector.x = -1;
          if (Input.GetKey(KeyCode.D))
              inputVector.x = +1;*/
          inputVector = inputVector.normalized;
          return inputVector;
       /* float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        return new Vector2(horizontalInput, verticalInput);*/
    }
    public string GetBindingText(Binding binding)
    {
        switch(binding)
        {
            default:
            case Binding.Move_Up:
                return playerInputActions.PlayerController.Move.bindings[1].ToDisplayString();
               
            case Binding.Move_Down:
                return playerInputActions.PlayerController.Move.bindings[2].ToDisplayString();
             
            case Binding.Move_Left:
                return playerInputActions.PlayerController.Move.bindings[3].ToDisplayString();
                
            case Binding.Move_Right:
                return playerInputActions.PlayerController.Move.bindings[4].ToDisplayString();
                
            case Binding.Interact:
                return playerInputActions.PlayerController.Interact.bindings[0].ToDisplayString();
                
            case Binding.InteractAlternate:
                return playerInputActions.PlayerController.InteractAlternate.bindings[0].ToDisplayString();
                
            case Binding.Pause:
                return playerInputActions.PlayerController.Pause.bindings[0].ToDisplayString();
             

        }
    }
    //Action is delegate and we use it here to disable panel when key is renound
    public void RebindBinding(Binding binding, Action OnActionRebound)
    { 
    
        playerInputActions.PlayerController.Disable();

        InputAction inputAction;
        int bindingIndex;
        switch(binding)
        {
            default:
            case Binding.Move_Up:
                inputAction = playerInputActions.PlayerController.Move;
                bindingIndex = 1;
                break;
            case Binding.Move_Down:
                inputAction = playerInputActions.PlayerController.Move;
                bindingIndex = 2;
                break;
            case Binding.Move_Left:
                inputAction = playerInputActions.PlayerController.Move;
                bindingIndex = 3;
                break;
            case Binding.Move_Right:
                inputAction = playerInputActions.PlayerController.Move;
                bindingIndex = 4;
                break;
            case Binding.Interact:
                inputAction = playerInputActions.PlayerController.Interact;
                bindingIndex = 0;
                break;
            case Binding.InteractAlternate:
                inputAction = playerInputActions.PlayerController.InteractAlternate;
                bindingIndex = 0;
                break;
            case Binding.Pause:
                inputAction = playerInputActions.PlayerController.Pause;
                bindingIndex = 0;
                break;
        }
        inputAction.PerformInteractiveRebinding(bindingIndex)
        .OnComplete(callback =>
        {
            callback.Dispose();
            playerInputActions.PlayerController.Enable();
            OnActionRebound();

            ///To Save the rebindings
           

            PlayerPrefs.SetString(Player_Prefs_Bindings  ,playerInputActions.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();
            OnBindingRebind?.Invoke(this, EventArgs.Empty);
        })
        .Start();
    }

   

}
