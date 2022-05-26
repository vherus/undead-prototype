using UnityEngine;

public class SpikeTrigger : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col) {
        Debug.Log(col.name + " was hit by spikes!");

        Death death = col.GetComponent<Death>();

        death.Die();
    }
}
