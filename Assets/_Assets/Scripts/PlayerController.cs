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
        float playerSize = 0.7f;
        bool canWalk = !Physics.Raycast(transform.position, movDir, playerSize);
        transform.forward = Vector3.Slerp(transform.forward, movDir, rotateSpeed * Time.deltaTime);//For player rotation


        // transform.forward += movDir * moveSpeed * Time.deltaTime;
        if (movDir != Vector3.zero)
        {
            if(canWalk)
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
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
