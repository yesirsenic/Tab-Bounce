using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Meteor : MonoBehaviour
{
    [Header("Base Bounce")]
    public float baseBounceY = 12f;     // 항상 동일한 점프 높이
    public float baseBounceX = 6f;      // 기본 좌우 속도
    public float paddleRange = 1.2f;    // 패들 중심 영향 범위

    [Header("Difficulty Scaling")]
    public float xSpeedPerSecond = 0.4f;   // 시간당 X 증가량
    public float gravityPerSecond = 0.05f;  // 시간당 중력 증가량
    public float maxGravity = 3.0f;

    [Header("Horizontal Control")]
    public float minHorizontalForce = 0.3f;

    Rigidbody2D rb;

    [SerializeField] private GameObject explosionPrefab;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float targetGravity =
            1.8f + DifficultyManager.Instance.ElapsedTime * gravityPerSecond;

        rb.gravityScale = Mathf.Min(targetGravity, maxGravity);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Paddle"))
            return;

        Transform paddle = collision.transform;

        // 패들 중심 기준 충돌 위치
        float diffX = transform.position.x - paddle.position.x;
        float normalizedX = Mathf.Clamp(diffX / paddleRange, -1f, 1f);

        if (Mathf.Abs(normalizedX) < minHorizontalForce)
        {
            normalizedX = Random.value < 0.5f
                ? -minHorizontalForce
                : minHorizontalForce;
        }

        normalizedX = Mathf.Clamp(normalizedX, -1f, 1f);

        //시간 기반 난이도 (좌우 속도만 증가)
        float difficultyX =
            baseBounceX + DifficultyManager.Instance.ElapsedTime * xSpeedPerSecond;

        //velocity 직접 지정 
        rb.linearVelocity = new Vector2(
            normalizedX * difficultyX,
            baseBounceY
        );

        GameManager.Instance.ScoreUp();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("GameOverZone"))
        {
            Vector3 hitPos = transform.position;

            Instantiate(explosionPrefab, hitPos, Quaternion.identity);

            Destroy(gameObject);
        }
    }
}
