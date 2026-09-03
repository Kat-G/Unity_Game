using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6f;

    public float leftLimit = -2.6f;
    public float rightLimit = 2.2f;

    Rigidbody2D rb;
    float moveDir;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveDir = 0;

        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x < Screen.width / 2)
                moveDir = -1;
            else
                moveDir = 1;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDir * speed, 0);

        float clampedX = Mathf.Clamp(transform.position.x, leftLimit, rightLimit);

        transform.position = new Vector3(
            clampedX,
            transform.position.y,
            transform.position.z
        );
    }
}