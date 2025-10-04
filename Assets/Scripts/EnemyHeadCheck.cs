using UnityEngine;

public class EnemyHeadCheck : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D playerRb;
    public float jumpForce = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerCheck> ())
        {
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, jumpForce);
        }
    }
}
