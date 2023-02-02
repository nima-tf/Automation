using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField]  private float rotationSpeed = 10f;

    [SerializeField] private GameInput gameInput;

    private bool isWalking;

    private void Update() {
        HandleMovement();
    }

    public bool IsWalking() {
        return isWalking;
    }

    private void HandleMovement() {
                Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

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
}
