using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GameOverZone : MonoBehaviour
{
    public float offset = -0.5f; 

    void Start()
    {
        Camera cam = Camera.main;

        // 화면 좌하단
        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 bottomRight = cam.ViewportToWorldPoint(new Vector3(1, 0, 0));

        float width = bottomRight.x - bottomLeft.x;

        // 위치
        transform.position = new Vector3(
            0f,
            bottomLeft.y + offset,
            0f
        );

        // Collider 크기
        BoxCollider2D col = GetComponent<BoxCollider2D>();
        col.isTrigger = true;
        col.size = new Vector2(width, 1f);
    }
}
