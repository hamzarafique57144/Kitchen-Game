using Cinemachine.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour,IKitchenObjectParent
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
        public BaseCounter selectedCounter;
        
    }
    [SerializeField]  private GameInput gameInput; 
    private float moveSpeed = 5f;
    private bool isWalking;
    private float rotateSpeed = 7f;
    Vector3 lastInteractDir;
    [SerializeField] private LayerMask counterLayerMask;
    private BaseCounter selectedCounter;
    private KitchenObject kitchenObject;
    [SerializeField] Transform kitchenObjectHolderPoint;
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
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternate;
        
            
        
    }
    private void GameInput_OnInteractAction(object sender,System.EventArgs e)
    {
        
        if (selectedCounter!=null)
        {
            selectedCounter.Interact(this);
        }
       
    }

    private void GameInput_OnInteractAlternate(object sender, System.EventArgs e)
    {
        if (selectedCounter!=null)
        {
            selectedCounter.InteractAlternate(this);
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
        float interactonDistance = 1.2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactonDistance,counterLayerMask))
        {

            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
               
                //Has counterLayerMask
                if (baseCounter != selectedCounter)
                {
                    selectedCounter = baseCounter;
                    SetSelectedCounter(baseCounter);
                  
                }
            }
            else
            {
                
                SetSelectedCounter(null);
                
            }
        }
        else
        {
            SetSelectedCounter(null);
          
            
        }
    }
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVector();
        Vector3 movDir = new Vector3(-inputVector.x, 0, -inputVector.y);
        isWalking = movDir != Vector3.zero;
        //     Debug.Log("Mov Dir " + movDir);
        float moveDistance = moveSpeed * Time.deltaTime;
        float PlayerRadius = 1f;
        float playerHieght = 2.7f;
        bool canWalk = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHieght, PlayerRadius, movDir, moveDistance);
        if (!canWalk)
        {
            //if player can not move towards movDir then try to move only on x movement
            Vector3 movDirX = new Vector3(movDir.x, 0, 0).normalized;
            canWalk = movDir.x !=0 &&!Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHieght, PlayerRadius, movDirX, moveDistance);
            if (canWalk)
            {
                movDir = movDirX;
            }
            else
            {

                //Can not move only on x direction
                //Atemt only in z
                Vector3 movDirZ = new Vector3(0, 0, movDir.z).normalized;
                canWalk = movDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHieght, PlayerRadius, movDirZ, moveDistance);
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
    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        
        this.selectedCounter = selectedCounter;
      
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs 
        {
            selectedCounter = selectedCounter

        });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHolderPoint;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
