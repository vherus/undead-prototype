using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject spikes;

    private BoxCollider2D bCollider;
    private float yPos;
    private Animator spikeAnimator;
    private BoxCollider2D spikeDeathTrigger;

    void Awake() {
        bCollider = GetComponent<BoxCollider2D>();
        spikeAnimator = spikes.GetComponent<Animator>();
        spikeDeathTrigger = spikes.GetComponent<BoxCollider2D>();
        yPos = transform.position.y;
    }

    void OnTriggerEnter2D(Collider2D col) {
        transform.position = new Vector3(transform.position.x, yPos - .05f, 0f);
        spikeAnimator.SetBool("Activated", true);
        spikeDeathTrigger.enabled = true;
    }

    void OnTriggerExit2D(Collider2D col) {
        transform.position = new Vector3(transform.position.x, yPos, 0f);
        spikeAnimator.SetBool("Activated", false);
        spikeDeathTrigger.enabled = false;
    }
}
