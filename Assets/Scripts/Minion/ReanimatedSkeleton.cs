using System.Collections;
using UnityEngine;

public class ReanimatedSkeleton : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private Rigidbody2D body;
    private SpriteRenderer sRenderer;
    private Animator animator;

    void Awake() {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        HandleRelease();
    }

    IEnumerator Die() {
        yield return new WaitForSeconds(1.917f);

        animator.SetBool("Release", false);
        animator.SetBool("Dead", true);

        Animator pAnim = player.GetComponent<Animator>();
        pAnim.SetBool(PlayerBools.CONTROLLING_DEAD, false);
    }

    private void HandleRelease() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            animator.SetBool("Release", true);
            StartCoroutine("Die");
        }

        if (Input.GetKeyUp(KeyCode.Space)) {
            animator.SetBool("Release", false);
            StopCoroutine("Die");
        }
    }

    IEnumerator TurnOffRiseAnimation() {
        yield return new WaitForSeconds(0.542f);

        animator.SetBool("Rising", false);
    }

    void OnEnable() {
        StartCoroutine("TurnOffRiseAnimation");
    }
}
