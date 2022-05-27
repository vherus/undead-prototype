using UnityEngine;

public class Player : MonoBehaviour, IMoveable
{
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    public bool CanWalk() {
        return !animator.GetBool(PlayerBools.RAISING_DEAD) && !animator.GetBool(PlayerBools.CONTROLLING_DEAD);
    }
}
