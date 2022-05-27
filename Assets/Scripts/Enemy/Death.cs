using System.Collections;
using UnityEngine;

public class Death : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D boxCollider;
    private CapsuleCollider2D capsuleCollider;
    private CircleCollider2D circleCollider;

    void Awake() {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    IEnumerator DeathAnimation() {
        yield return new WaitForSeconds(0.15f);

        animator.SetBool("Dead", true);

        if (capsuleCollider != null) {
            capsuleCollider.enabled = false;
        }
        
        if (boxCollider != null) {
            boxCollider.enabled = true;
        }
        
        if (circleCollider != null) {
            circleCollider.enabled = true;
        }
    }

    public void Die() {
        if (!animator.GetBool("Dead")) {
            StartCoroutine("DeathAnimation");
        }
    }
}
