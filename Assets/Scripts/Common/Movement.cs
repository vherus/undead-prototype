using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float movementForce = 5;

    private float movementX;
    private Rigidbody2D body;
    private SpriteRenderer sRenderer;
    private Animator animator;
    private IMoveable moveableObject;

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sRenderer = GetComponent<SpriteRenderer>();
        moveableObject = GetComponent<IMoveable>();
    }

    void FixedUpdate() {
        HandleMovement();
    }

    private void HandleMovement() {
        if (!gameObject.activeSelf) {
            return;
        }
        
        movementX = Input.GetAxisRaw("Horizontal");

        if (movementX == 0) {
            StopWalking();
            return;
        }

        if (moveableObject.CanWalk()) {
            StartWalking();
        }
    }

    private void StopWalking() {
        animator.SetBool(MovementBools.WALK, false);
    }

    private void StartWalking() {
        sRenderer.flipX = movementX < 0;
        transform.position += new Vector3(movementX, 0f, 0f) * movementForce * Time.deltaTime;

        animator.SetBool(MovementBools.WALK, true);
    }
}
