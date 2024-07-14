using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    public event EventHandler OnInteractAction;
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();
        playerInputActions.PlayerController.Interact.performed += Interact_performed;
    }
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        /* if(OnInteractAction != null)
         {
             OnInteractAction(this, EventArgs.Empty);
         }*/
        OnInteractAction?.Invoke(this, EventArgs.Empty);//this dose Same fuction as above if statement
       
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
}
