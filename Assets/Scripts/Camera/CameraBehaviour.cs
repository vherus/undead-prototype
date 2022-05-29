using Cinemachine;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    private CinemachineFramingTransposer transposer;
    private SpriteRenderer pRenderer;

    void Awake() {
        CinemachineVirtualCamera c = GetComponent<CinemachineVirtualCamera>();
        transposer = c.GetCinemachineComponent<CinemachineFramingTransposer>();
        pRenderer = GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>();
    }

    void LateUpdate() {
        if (pRenderer.flipX && transposer.m_TrackedObjectOffset.x != -.888f) {
            transposer.m_TrackedObjectOffset.x = -.888f;
        }

        if (!pRenderer.flipX && transposer.m_TrackedObjectOffset.x != .888f) {
            transposer.m_TrackedObjectOffset.x = .888f;
        }
    }
}
