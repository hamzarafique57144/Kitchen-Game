using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    //private PlayerInputActions playerInputActions;
    private void Awake()
    {
       /* playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();*/
    }
    public Vector2 GetMovementVector()
    {
        /*  Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

          *//*Vector2 inputVector =new Vector2(0f,0f);
          if (Input.GetKey(KeyCode.W))
              inputVector.y = +1;
          if (Input.GetKey(KeyCode.S))
              inputVector.y = -1;
          if (Input.GetKey(KeyCode.A))
              inputVector.x = -1;
          if (Input.GetKey(KeyCode.D))
              inputVector.x = +1;*//*
          inputVector = inputVector.normalized;
          return inputVector;*/
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        return new Vector2(horizontalInput, verticalInput);
    }
}
