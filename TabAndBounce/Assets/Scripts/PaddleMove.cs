using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class PaddleMove : MonoBehaviour
{
    private float fixedY = -1.5f;

    private Camera cam;
    private Rigidbody2D rb;
    private Collider2D col;

    private float minX;
    private float maxX;

    private float targetX;
    private bool hasInput;

    private void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        Vector2 left = cam.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 right = cam.ViewportToWorldPoint(new Vector2(1, 0));

        float halfWidth = col.bounds.extents.x;
        minX = left.x + halfWidth;
        maxX = right.x - halfWidth;
    }

    void Update()
    {
        float? screenX = null;

        
        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.isPressed)
        {
            screenX = Touchscreen.current.primaryTouch.position.ReadValue().x;
        }

#if UNITY_EDITOR
        
        if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            screenX = Mouse.current.position.ReadValue().x;
        }
#endif

        if (screenX.HasValue)
        {
            float depth = cam.WorldToScreenPoint(transform.position).z;
            Vector3 worldPos = cam.ScreenToWorldPoint(
                new Vector3(screenX.Value, 0f, depth)
            );

            targetX = Mathf.Clamp(worldPos.x, minX, maxX);
            hasInput = true;
        }
        else
        {
            hasInput = false;
        }
    }
    void FixedUpdate()
    {
        if (!hasInput) return;

        rb.MovePosition(new Vector2(targetX, fixedY));
    }


}
