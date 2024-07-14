using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField]  private GameInput gameInput; 
    private float moveSpeed = 5f;
    private bool isWalking;
    private float rotateSpeed = 7f;
    Vector3 lastInteraction;
    [SerializeField] private LayerMask counterLayerMask;
    // Start is called before the first frame update
    void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
     
    }
    private void GameInput_OnInteractAction(object sender,System.EventArgs e)
    {
        Debug.Log("Event ");
        Vector2 inputVector = gameInput.GetMovementVector();
        Vector3 movDir = new Vector3(-inputVector.x, 0, -inputVector.y);
        if (movDir != Vector3.zero)
        {
            lastInteraction = movDir;
        }
        float interactonDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteraction, out RaycastHit raycastHit, interactonDistance, counterLayerMask))
        {
            Debug.Log(raycastHit.transform);
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
                //Has counterLayerMask
                clearCounter.Interact();
        }
        else
        {
            Debug.Log("Raycast : Nothing");
        }

    }
    // Update is called once per frame
    void Update()
    {
        HandleMovement();
       // HandleInteractions();
    }

    public bool IsWalking()
    {
        return isWalking;
    }
    private void HandleInteractions()
    {
          }
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVector();
        Vector3 movDir = new Vector3(-inputVector.x, 0, -inputVector.y);
        isWalking = movDir != Vector3.zero;
        //     Debug.Log("Mov Dir " + movDir);
        float moveDistance = moveSpeed * Time.deltaTime;
        float PlayerRadius = 0.7f;
        float playerHieght = 2f;
        bool canWalk = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHieght, PlayerRadius, movDir, moveDistance);
        if (!canWalk)
        {
            //if player can not move towards movDir then try to move only on x movement
            Vector3 movDirX = new Vector3(movDir.x, 0, 0).normalized;
            canWalk = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHieght, PlayerRadius, movDirX, moveDistance);
            if (canWalk)
            {
                movDir = movDirX;
            }
            else
            {

                //Can not move only on x direction
                //Atemt only in z
                Vector3 movDirZ = new Vector3(0, 0, movDir.z).normalized;
                canWalk = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHieght, PlayerRadius, movDirZ, moveDistance);
                if (canWalk)
                {
                    //Can move in z direction
                    movDir = movDirZ;
                }
                else
                {
                   
                    //Can not move in any direction
                    Debug.Log("Player can not move in any direction");
                }

            }

        }
        

        if (canWalk)
        {
            //  transform.Translate(Vector3.forward * moveDistance);
            transform.position += movDir * moveSpeed * Time.deltaTime;
        }
        transform.forward = Vector3.Slerp(transform.forward, movDir, rotateSpeed * Time.deltaTime);//For player rotation
    }
   
}
