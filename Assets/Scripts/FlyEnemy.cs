using UnityEngine;

public class FlyEnemy : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 2f;          // tốc độ bay
    public Transform[] points;        // các điểm bay tuần tra

    private int i = 0;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        rb = GetComponent<Rigidbody2D>();
        if (rb == null) rb = gameObject.AddComponent<Rigidbody2D>();

        // Kinematic → không bị physics tác động
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;
        rb.freezeRotation = true;

        if (points.Length == 0)
        {
            Debug.LogError("Chưa gán điểm bay!");
        }
    }

    void Update()
    {
        if (points.Length == 0) return;

        // target hiện tại
        Vector2 targetPos = new Vector2(points[i].position.x, transform.position.y); // chỉ di chuyển ngang

        // di chuyển ngang bằng MovePosition (không bị physics đẩy)
        rb.MovePosition(Vector2.MoveTowards(rb.position, targetPos, speed * Time.deltaTime));

        // khi tới target → chuyển sang point tiếp theo
        if (Mathf.Abs(rb.position.x - targetPos.x) < 0.05f)
        {
            i++;
            if (i >= points.Length) i = 0;
        }

        // flip sprite theo hướng di chuyển
        float dirX = targetPos.x - rb.position.x;
        spriteRenderer.flipX = dirX > 0;
    }
}
