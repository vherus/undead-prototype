using UnityEngine;

public class Minion : MonoBehaviour, IMoveable
{
    private Animator animator;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    public bool CanWalk() {
        return !animator.GetBool("Release") && !animator.GetBool("Dead");
    }
}
