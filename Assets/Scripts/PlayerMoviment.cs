using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    private Rigidbody2D Rb;

    [Header("Movement")]
    public float forwardSpeed = 8f;
    public float gravityScale = 4f;

    [Header("Ground Check")]
    public Transform groundCheckDown;
    public Transform groundCheckUp;
    public float groundDistance = 0.2f;
    public LayerMask groundLayer;

    private bool isUpsideDown = false;
    private bool isGrounded = false;

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Rb.gravityScale = gravityScale;
    }

    private void Update()
    {
        CheckGround();

        if (Input.GetButtonDown("Jump"))
        {
            FlipGravity();
        }
    }

    private void FixedUpdate()
    {
        // Constant forward movement
        Rb.linearVelocity = new Vector2(forwardSpeed, Rb.linearVelocity.y);
    }

    void FlipGravity()
    {
        isUpsideDown = !isUpsideDown;

        // Instant gravity flip
        Rb.gravityScale = isUpsideDown ? -gravityScale : gravityScale;

        // Flip sprite visually only
        Vector3 scale = transform.localScale;
        scale.y = isUpsideDown ? -Mathf.Abs(scale.y) : Mathf.Abs(scale.y);
        transform.localScale = scale;
    }

    void CheckGround()
    {
        Transform checkPoint = isUpsideDown ? groundCheckUp : groundCheckDown;

        isGrounded = Physics2D.OverlapCircle(checkPoint.position, groundDistance, groundLayer);

        // Snap to surface (removes floaty feel)
        if (isGrounded)
        {
            Rb.linearVelocity = new Vector2(Rb.linearVelocity.x, 0f);
        }
    }

}