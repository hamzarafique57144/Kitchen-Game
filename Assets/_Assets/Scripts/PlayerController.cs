using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance {get;private set;}
   /* private static PlayerController instance;
    public static PlayerController Instance
    {
        get { return instance; }
        set { Instance = value; }
    }*/
    //The below commented code is same as above code, we just make a singleton
  /*  public static PlayerController instanceField;
    public static PlayerController GetInstanceField(PlayerController instanceField)
    {
        return instanceField;
    }

    public static void SetInstanceField(PlayerController instanceField)
    {
        PlayerController.instanceField = instanceField;
    }*/
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public ClearCounter selectedCounter;
        
    }
    [SerializeField]  private GameInput gameInput; 
    private float moveSpeed = 5f;
    private bool isWalking;
    private float rotateSpeed = 7f;
    Vector3 lastInteractDir;
    [SerializeField] private LayerMask counterLayerMask;
    private ClearCounter selectedCounter;
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject); // Ensure there is only one instance
            return;
        }
        Instance = this;

    }
    void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        
            
        
    }
    private void GameInput_OnInteractAction(object sender,System.EventArgs e)
    {
        
        if (selectedCounter!=null)
        {
            selectedCounter.Interact();
        }
       
    }
    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking()
    {
        return isWalking;
    }
    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVector();
        Vector3 movDir = new Vector3(-inputVector.x, 0, -inputVector.y);
        if (movDir != Vector3.zero)
        {
            lastInteractDir = movDir;
        }
        float interactonDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactonDistance,counterLayerMask))
        {

            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
               
                //Has counterLayerMask
                if (clearCounter != selectedCounter)
                {
                    selectedCounter = clearCounter;
                    SetSelectedCounter(clearCounter);
                    Debug.Log("Clear counter is different from selected counter ");
                }
            }
            else
            {
                
                SetSelectedCounter(null);
                Debug.Log("Selected counter is null1");
            }
        }
        else
        {
            SetSelectedCounter(null);
            Debug.Log("Clear Counter is not in Range");
            
        }
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
    private void SetSelectedCounter(ClearCounter selectedCounter)
    {
        Debug.Log("Selected Counter before assingning reference:" + this.selectedCounter);
        this.selectedCounter = selectedCounter;
        Debug.Log("Selected Counter after assingning reference:" + this.selectedCounter);
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs 
        {
            selectedCounter = selectedCounter

        });
    }
}
