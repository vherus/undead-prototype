using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private GameObject leftBoundary, rightBoundary;

    [SerializeField]
    private float boundaryOffset = 8f;

    [SerializeField]
    private float playerOffset = .7f;

    [SerializeField]
    private float smoothing = 6f;

    [SerializeField]
    private float smoothMod = 1.5f;

    private GameObject player;
    private SpriteRenderer sRenderer;
    private Vector3 targetPosition;
    private float leftLimit;
    private float rightLimit;
    private bool flipX;
    private bool turned = false;
    private bool routineRunning = false;

    void Start() {
        player = GameObject.FindWithTag("Player");
        sRenderer = player.GetComponent<SpriteRenderer>();
        flipX = sRenderer.flipX;
        leftLimit = leftBoundary.transform.position.x + boundaryOffset;
        rightLimit = rightBoundary.transform.position.x - boundaryOffset;
    }

    void LateUpdate() {
        if (!player) {
            return;
        }

        float targetX = flipX ? player.transform.position.x - playerOffset : player.transform.position.x + playerOffset;
        float targetY = player.transform.position.y + 3f;

        if (targetX < leftLimit) {
            targetX = leftLimit;
        }

        if (targetX > rightLimit) {
            targetX = rightLimit;
        }

        if (flipX != sRenderer.flipX) {
            flipX = sRenderer.flipX;
            turned = true;
        }

        targetPosition = new Vector3(targetX, targetY, -10);

        transform.position = Vector3.Lerp(transform.position, targetPosition, (turned ? smoothing + smoothMod : smoothing) * Time.deltaTime);

        if (turned && !routineRunning) {
            StartCoroutine("SwitchTurned");
        }
    }

    IEnumerator SwitchTurned() {
        routineRunning = true;

        yield return new WaitForSeconds(0.05f);

        if (turned) {
            turned = false;
        }

        routineRunning = false;
    }
}
