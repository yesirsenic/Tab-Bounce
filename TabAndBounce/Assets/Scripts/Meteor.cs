using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Meteor : MonoBehaviour
{
    public float bounceForceY = 5f;     
    public float bounceForceX = 12f;      
    public float maxX = 1.2f;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Paddle"))
            return;

        Transform paddle = collision.transform;

        // paddle 중심 기준으로 충돌 위치 계산
        float diffX = transform.position.x - paddle.position.x;

        // -1 ~ 1 범위로 정규화
        float normalizedX = Mathf.Clamp(diffX / maxX, -1f, 1f);

        // 기존 속도 제거
        rb.linearVelocity = Vector2.zero;

        // 새 속도 부여 
        Vector2 force = new Vector2(
            normalizedX * bounceForceX,
            bounceForceY
        );

        rb.AddForce(force, ForceMode2D.Impulse);
    }
}
