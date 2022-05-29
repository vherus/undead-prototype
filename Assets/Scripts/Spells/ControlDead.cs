using System.Collections;
using UnityEngine;

public class ControlDead : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer sRenderer;
    private Necromancy necromancy;

    void Awake() {
        animator = GetComponent<Animator>();
        sRenderer = GetComponent<SpriteRenderer>();
        necromancy = GetComponent<Necromancy>();
    }

    void Update() {
        HandleRaiseDead();
    }

    IEnumerator ControlDeadTransition() {
        yield return new WaitForSeconds(5f);

        if (animator.GetBool(PlayerBools.RAISING_DEAD)) {
            animator.SetBool(PlayerBools.RAISING_DEAD, false);

            #nullable enable
            GameObject? corpse = necromancy.GetNearestCorpse();
            #nullable disable

            if (necromancy.CanRaiseDead(corpse) && IsFacingCorpse(corpse)) {
                Animator corpseAnim = corpse.GetComponent<Animator>();
                Transform skeleton = corpse.transform.Find("Skeleton");

                animator.SetBool(PlayerBools.CONTROLLING_DEAD, true);
                skeleton.gameObject.SetActive(true);
                skeleton.gameObject.GetComponent<Movement>().enabled = true;
                skeleton.gameObject.GetComponent<Follower>().enabled = false;

                corpseAnim.SetBool("Risen", true);
            }
        }
    }

    private bool IsFacingCorpse(GameObject corpse) {
        Vector3 localCorpseDir = transform.InverseTransformPoint(corpse.transform.position);

        if (localCorpseDir.x < 0 && !sRenderer.flipX) {
            return false;
        }

        if (localCorpseDir.x > 0 && sRenderer.flipX) {
            return false;
        }

        return true;
    }

    private void HandleRaiseDead() {
        if (Input.GetKeyDown(KeyCode.E) && !animator.GetBool(PlayerBools.CONTROLLING_DEAD)) {
            animator.SetBool(PlayerBools.RAISING_DEAD, true);

            StartCoroutine("ControlDeadTransition");
            return;
        }

        if (Input.GetKeyUp(KeyCode.E)) {
            StopCoroutine("ControlDeadTransition");
            animator.SetBool(PlayerBools.RAISING_DEAD, false);
        }
    }
}
