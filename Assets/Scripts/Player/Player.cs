using UnityEngine;

public class Player : MonoBehaviour, IMoveable
{
    private Animator animator;
    private SpriteRenderer sRenderer;

    private void Awake() {
        animator = GetComponent<Animator>();
        sRenderer = GetComponent<SpriteRenderer>();
    }

    public bool CanWalk() {
        return !animator.GetBool(PlayerBools.RAISING_DEAD) && !animator.GetBool(PlayerBools.CONTROLLING_DEAD) && !animator.GetBool(PlayerBools.ANIMATING_DEAD);
    }

    public bool CanJump() {
        return true;
    }
}
