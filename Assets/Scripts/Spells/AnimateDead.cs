using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateDead : MonoBehaviour
{
    private Necromancy necromancy;
    private Animator anim;

    void Awake() {
        necromancy = GetComponent<Necromancy>();
        anim = GetComponent<Animator>();
    }

    void Update() {
        HandleAnimateDead();
    }

    private void HandleAnimateDead() {
        if (Input.GetKeyDown(KeyCode.Q) && !anim.GetBool(PlayerBools.ANIMATING_DEAD)) {
            anim.SetBool(PlayerBools.WALK, false);
            anim.SetBool(PlayerBools.ANIMATING_DEAD, true);

            #nullable enable
            GameObject? corpse = necromancy.GetNearestCorpse();
            #nullable disable

            if (necromancy.CanRaiseDead(corpse)) {
                Animator corpseAnim = corpse.GetComponent<Animator>();
                Transform skeleton = corpse.transform.Find("Skeleton");

                skeleton.gameObject.SetActive(true);
                skeleton.gameObject.GetComponent<Movement>().enabled = false;
                skeleton.gameObject.GetComponent<Follower>().enabled = true;

                corpseAnim.SetBool("Risen", true);
            }

            StartCoroutine("TurnOffAnimation");
        }
    }

    IEnumerator TurnOffAnimation() {
        yield return new WaitForSeconds(.417f);

        anim.SetBool(PlayerBools.ANIMATING_DEAD, false);
    }
}
