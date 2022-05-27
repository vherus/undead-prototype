using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseDead : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer sRenderer;
    private List<GameObject> corpsesInRange = new List<GameObject>();

    void Awake() {
        animator = GetComponent<Animator>();
        sRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        HandleRaiseDead();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy" && col.gameObject.GetComponent<Animator>().GetBool("Dead")) {
            corpsesInRange.Add(col.gameObject);
        }
    }


    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy" && col.gameObject.GetComponent<Animator>().GetBool("Dead")) {
            corpsesInRange.Remove(col.gameObject);
        }
    }

    IEnumerator ControlDeadTransition() {
        yield return new WaitForSeconds(5f);

        if (animator.GetBool(PlayerBools.RAISING_DEAD)) {
            animator.SetBool(PlayerBools.RAISING_DEAD, false);

            #nullable enable
            GameObject? corpse = GetNearestCorpse();
            #nullable disable

            if (corpse != null && IsFacingCorpse(corpse)) {
                Animator corpseAnim = corpse.GetComponent<Animator>();

                #nullable enable
                Transform? skeleton = corpse.transform.Find("Skeleton");
                #nullable disable

                if (corpseAnim.GetBool("Dead") && !corpseAnim.GetBool("Risen") && skeleton != null) {
                    animator.SetBool(PlayerBools.CONTROLLING_DEAD, true);
                    skeleton.gameObject.SetActive(true);

                    corpseAnim.SetBool("Risen", true);
                }
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

    #nullable enable
    private GameObject? GetNearestCorpse() {
        GameObject? nearestCorpse = null;
        float? nearestCorpseDistance = null;

        foreach (GameObject corpse in corpsesInRange) {
            if (!nearestCorpse) {
                nearestCorpse = corpse;
                nearestCorpseDistance = Vector3.Distance(gameObject.transform.position, nearestCorpse.transform.position);
            }

            float currDistance = Vector3.Distance(gameObject.transform.position, corpse.transform.position);

            if (currDistance < nearestCorpseDistance) {
                nearestCorpse = corpse;
            }
        }

        return nearestCorpse;
    }
    #nullable disable
}
