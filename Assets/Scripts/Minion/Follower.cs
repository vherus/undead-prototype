using UnityEngine;

public class Follower : MonoBehaviour, IMoveable
{
    private GameObject player;
    private SpriteRenderer pRenderer;
    private SpriteRenderer sRenderer;
    private Movement pMovement;
    private Animator anim;

    void Awake() {
        sRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        pRenderer = player.GetComponent<SpriteRenderer>();
        pMovement = player.GetComponent<Movement>();
    }

    void FixedUpdate() {
        if (!IsNextToPlayer() && CanWalk()) {
            StartWalking();
            return;
        }

        StopWalking();
    }

    private void StopWalking() {
        anim.SetBool(MovementBools.WALK, false);
    }

    private void StartWalking() {
        sRenderer.flipX = pRenderer.flipX;

        transform.position = Vector3
                .Lerp(transform.position, GetTargetPosition(), (pMovement.MovementForce - 1f) * Time.deltaTime);

        anim.SetBool(MovementBools.WALK, true);
    }

    private Vector3 GetTargetPosition() {
        float posMod = !pRenderer.flipX ? -1.3f : 1.3f;
        return new Vector3(player.transform.position.x + posMod, transform.position.y, transform.position.z);
    }

    private bool IsNextToPlayer() {
        float targetX = GetTargetPosition().x;

        return targetX > transform.position.x ? targetX - transform.position.x < 0.019f : transform.position.x - targetX < 0.019f;
    }

    public bool CanWalk() {
        return !anim.GetBool("Rising");
    }

    public bool CanJump() {
        return false;
    }
}
