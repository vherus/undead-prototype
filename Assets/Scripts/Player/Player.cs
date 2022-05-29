using UnityEngine;

public class Player : MonoBehaviour, IMoveable
{
    [SerializeField]
    private GameObject camOffset;

    [SerializeField]
    private float offsetX = .888f;

    private Animator animator;
    private SpriteRenderer sRenderer;
    private bool flipX;

    private void Awake() {
        animator = GetComponent<Animator>();
        sRenderer = GetComponent<SpriteRenderer>();
        flipX = sRenderer.flipX;
    }

    void FixedUpdate() {
        if (flipX != sRenderer.flipX) {
            camOffset.transform.position = new Vector3(
                flipX ? transform.position.x + offsetX : transform.position.x - offsetX,
                camOffset.transform.position.y,
                camOffset.transform.position.z
            );

            flipX = sRenderer.flipX;
        }
    }

    public bool CanWalk() {
        return !animator.GetBool(PlayerBools.RAISING_DEAD) && !animator.GetBool(PlayerBools.CONTROLLING_DEAD);
    }

    public bool CanJump() {
        return true;
    }
}
