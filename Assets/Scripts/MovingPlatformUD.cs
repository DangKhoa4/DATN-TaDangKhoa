using UnityEngine;

public class MovingPlatformUD : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 2f;           // tốc độ di chuyển
    public Transform[] points;         // các điểm di chuyển

    private int i = 0;

    void Start()
    {
        if (points.Length == 0)
        {
            Debug.LogError("Chưa gán points cho MovingPlatform!");
            return;
        }

        // bắt đầu ở điểm đầu tiên
        transform.position = points[0].position;
    }

    void Update()
    {
        if (points.Length == 0) return;

        // di chuyển tới target hiện tại
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);

        // nếu tới target → chuyển sang point tiếp theo
        if (Vector2.Distance(transform.position, points[i].position) < 0.01f)
        {
            i++;
            if (i >= points.Length) i = 0;
        }
    }

    // cho player đi theo platform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
