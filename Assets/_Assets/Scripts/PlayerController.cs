using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    bool isRotating = false;
    [SerializeField]  private GameInput gameInput; 
    private float moveSpeed = 5f;
    private bool isWalking;
  public  float rotateSpeed = 100f;
    // Start is called before the first frame update
    void Start()
    { 
        
        if (gameInput == null)
        {
            Debug.LogError("GameInput is not set on PlayerController");
        }
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 inputVector = gameInput.GetMovementVector();
        Vector3 movDir = new Vector3(-inputVector.x, 0, -inputVector.y);
        //     Debug.Log("Mov Dir " + movDir);
        float moveDistance = moveSpeed * Time.deltaTime;
        float PlayerRadius = 0.7f;
        float playerHieght = 2f;
        bool canWalk = !Physics.CapsuleCast(transform.position,transform.position +Vector3.up* playerHieght, PlayerRadius,movDir, moveDistance);
        if(!canWalk)
        {
            //if player can not move towards movDir then try to move only on x movement
            Vector3 movDirX = new Vector3(movDir.x, 0, 0).normalized;
            canWalk = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHieght, PlayerRadius, movDirX, moveDistance);
            if(canWalk)
            {
                movDir = movDirX;
            }
            else
            {
                
                //Can not move only on x direction
                //Atemt only in z
                Vector3 movDirZ = new Vector3(0,0,movDir.z).normalized;
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
        transform.forward = Vector3.Slerp(transform.forward, movDir, rotateSpeed * Time.deltaTime);//For player rotation

         if(canWalk)
        {
            //  transform.Translate(Vector3.forward * moveDistance);
            transform.position += movDir * moveSpeed * Time.deltaTime;
        }
         
        
    
        
        /*if(canWalk)
        {
            transform.forward += movDir * moveSpeed * Time.deltaTime;///For player movement
        }*/
        isWalking = movDir != Vector3.zero;    //For walking animation

       
       
      
       
    }

    public bool IsWalking()
    {
        return isWalking;
    }
   
}
