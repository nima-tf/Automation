using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenItemParent {
    public static Player Instance { get; private set; }
    public event EventHandler<OnSelectCounterChangedEventArgs> OnSelectCounterChanged;

    public class OnSelectCounterChangedEventArgs : EventArgs {
        public ClearCounter selectedCounter;
    }

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField]  private float rotationSpeed = 10f;
    [SerializeField] private float interactDistance = 2f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenItemHoldPoint;
    private KitchenItem kitchenItem;
    private bool isWalking;
    private Vector3 lastInteractMoveDir;
    private ClearCounter selectedCounter;

    private void Awake()
    {
        if (Instance != null) {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) {
        if (selectedCounter != null) {
            selectedCounter.Interact(this);
        }
    }

    private void Update() {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        HandleMovement(inputVector, moveDir);
        HandleInteractions(inputVector, moveDir);
    }

    public bool IsWalking() {
        return isWalking;
    }

    private void HandleInteractions(Vector2 inputVector, Vector3 moveDir) {

        if (moveDir != Vector3.zero) {
            lastInteractMoveDir = moveDir;
        }

        if (Physics.Raycast(transform.position, lastInteractMoveDir, out RaycastHit raycastHit, interactDistance, countersLayerMask)) {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter)) {
                // Has ClearCounter
                if (clearCounter != selectedCounter) {
                    SetSelectedCounter(clearCounter);
                } else {
                    SetSelectedCounter(null);
                }
            } else {
                SetSelectedCounter(null);
            }
        }
    }

    private void HandleMovement(Vector2 inputVector, Vector3 moveDir) {
        float moveDistance = moveSpeed * Time.deltaTime;
        float playerHeight = 2f;
        float playerRadius = .7f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove) {
            // Collision

            // Attempts X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove) {
                // Can only move on X
                moveDir = moveDirX;
            } else {
                // Cannot move on X

                // Attempts Z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);
                
                if (canMove) {
                    // Can move on Z
                    moveDir = moveDirZ;
                } else {
                    // Cannot move on any direction
                }
            }
        }

        if (canMove) {
        transform.position += moveDir * moveDistance;
        }

        isWalking = moveDir != Vector3.zero;
        
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotationSpeed);
    }

    private void SetSelectedCounter(ClearCounter selectedCounter) {
        this.selectedCounter = selectedCounter;
            OnSelectCounterChanged?.Invoke(this, new OnSelectCounterChangedEventArgs {
                selectedCounter = selectedCounter
            });        
    }

    public Transform GetKitchenItemFollowTransform() {
        return kitchenItemHoldPoint;
    }

    public void SetKitchenItem(KitchenItem kitchenItem) {
        this.kitchenItem = kitchenItem;
    }
    
    public KitchenItem GetKitchenItem() {
        return kitchenItem;
    }

    public void ClearKitchenItem() {
        kitchenItem = null;
    }

    public bool HasKitchenItem() {
        return this.kitchenItem != null;
    }
}
