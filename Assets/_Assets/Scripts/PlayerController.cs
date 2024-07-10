using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField]  private GameInput gameInput;
    private float moveSpeed = 2f;
    private bool isWalking;
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
        Debug.Log("Mov Dir " + movDir);
         transform.forward += movDir * moveSpeed * Time.deltaTime;///For player movement
        isWalking = movDir != Vector3.zero;    //For walking animation
        float rotateSpeed = .1f;
        transform.forward = Vector3.Slerp(transform.forward, movDir, rotateSpeed * Time.deltaTime);//For player rotation
       
    }

    public bool IsWalking()
    {
        return isWalking;
    }
   
}
