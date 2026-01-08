using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MeteorWallLimit : MonoBehaviour
{
    private Rigidbody2D rb;
    private Camera cam;

    private float minX;
    private float maxX;
    private float radius;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;

        // 화면 좌우 경계 (월드 기준)
        Vector2 left = cam.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 right = cam.ViewportToWorldPoint(new Vector2(1, 0));

        // 공 반지름
        radius = GetComponent<CircleCollider2D>().radius * transform.localScale.x;

        minX = left.x + radius;
        maxX = right.x - radius;
    }

    void FixedUpdate()
    {
        Vector2 pos = rb.position;
        Vector2 vel = rb.linearVelocity;

        if (pos.x <= minX && vel.x < 0f)
        {
            vel.x = Mathf.Abs(vel.x); 
        }
        else if (pos.x >= maxX && vel.x > 0f)
        {
            vel.x = -Mathf.Abs(vel.x);
        }

        rb.linearVelocity = vel;
    }
}
