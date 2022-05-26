using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private float movementForce = 5;

    [SerializeField]
    private GameObject minion;

    [SerializeField]
    private GameObject goblin;

    private float movementX;

    private Rigidbody2D body;
    private SpriteRenderer sRenderer;
    private Animator animator;

    private void Awake() {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate() {
        HandleMovement();
    }

    void Update() {
        HandleRaiseDead();
    }

    IEnumerator ControlDeadTransition() {
        yield return new WaitForSeconds(5f);

        if (animator.GetBool(PlayerBools.RAISING_DEAD)) {
            animator.SetBool(PlayerBools.RAISING_DEAD, false);

            Animator gAnim = goblin.GetComponent<Animator>();
            if (goblin.GetComponent<Death>().isDead && !gAnim.GetBool("Risen")) {
                animator.SetBool(PlayerBools.CONTROLLING_DEAD, true);
                minion.SetActive(true);
                gAnim.SetBool("Risen", true);
            }
        }
    }

    private void HandleRaiseDead() {
        if (Input.GetKeyDown(KeyCode.Space) && !animator.GetBool(PlayerBools.CONTROLLING_DEAD)) {
            animator.SetBool(PlayerBools.RAISING_DEAD, true);

            StartCoroutine("ControlDeadTransition");
            return;
        }

        if (Input.GetKeyUp(KeyCode.Space)) {
            StopCoroutine("ControlDeadTransition");
            animator.SetBool(PlayerBools.RAISING_DEAD, false);
        }
    }

    private void HandleMovement() {
        movementX = Input.GetAxisRaw("Horizontal");

        if (movementX == 0) {
            StopWalking();
            return;
        }

        if (CanWalk()) {
            StartWalking();
        }
    }

    private bool CanWalk() {
        return !animator.GetBool(PlayerBools.RAISING_DEAD) && !animator.GetBool(PlayerBools.CONTROLLING_DEAD);
    }

    private void StopWalking() {
        animator.SetBool(PlayerBools.WALK, false);
    }

    private void StartWalking() {
        sRenderer.flipX = movementX < 0;
        transform.position += new Vector3(movementX, 0f, 0f) * movementForce * Time.deltaTime;

        animator.SetBool(PlayerBools.WALK, true);
    }
}
