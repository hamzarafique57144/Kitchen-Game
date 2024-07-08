using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
   [SerializeField]   private GameInput gameInput;
    private float moveSpeed = 3f;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVector();
        Vector3 movDir = new Vector3(inputVector.x, 0, inputVector.y);
        transform.position += movDir * moveSpeed * Time.deltaTime;
        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, movDir, rotateSpeed * Time.deltaTime);
        Debug.Log(inputVector);
    }
   
}
