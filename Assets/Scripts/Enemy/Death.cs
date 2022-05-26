using System.Collections;
using UnityEngine;

public class Death : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D boxCollider;
    private CapsuleCollider2D capsuleCollider;

    public bool isDead = false;

    void Awake() {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    IEnumerator DeathAnimation() {
        yield return new WaitForSeconds(0.15f);

        animator.SetBool("Dead", true);
        isDead = true;
        capsuleCollider.enabled = false;
        boxCollider.enabled = true;
    }

    public void Die() {
        if (!animator.GetBool("Dead")) {
            StartCoroutine("DeathAnimation");
        }
    }
}
