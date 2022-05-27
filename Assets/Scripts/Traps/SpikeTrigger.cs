using UnityEngine;

public class SpikeTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col) {
        Death death = col.GetComponent<Death>();

        if (death != null) {
            death.Die();
        }
    }
}
