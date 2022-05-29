using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Necromancy : MonoBehaviour
{
    private List<GameObject> corpsesInRange = new List<GameObject>();

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Enemy" && col.gameObject.GetComponent<Animator>().GetBool("Dead")) {
            corpsesInRange.Add(col.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Enemy" && col.gameObject.GetComponent<Animator>().GetBool("Dead")) {
            corpsesInRange.Remove(col.gameObject);
        }
    }

    #nullable enable
    public GameObject? GetNearestCorpse() {
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

    #nullable enable
    public bool CanRaiseDead(GameObject? corpse) {
        if (corpse == null) {
            return false;
        }

        Animator corpseAnim = corpse.GetComponent<Animator>();
        Transform? skeleton = corpse.transform.Find("Skeleton");

        if (skeleton == null) {
            return false;
        }

        return corpseAnim.GetBool("Dead") && !corpseAnim.GetBool("Risen");
    }
    #nullable disable
}
