using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    private Rigidbody2D Rb;

    public float jumpForce = 10f;
    public float maxJumpTime = 0.2f;

    public LayerMask groundLayer;
    public Transform feetPos;
    public float groundDistance;

    private bool isGrounded;
    private bool isJumping;
    private float jumpTimer;

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, groundDistance, groundLayer);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpTimer = 0;
            Rb.linearVelocity = Vector2.up * jumpForce;
        }

        if (isJumping && Input.GetButton("Jump"))
        {
            if (jumpTimer < maxJumpTime)
            {
                Rb.linearVelocity = Vector2.up * jumpForce;
                jumpTimer += Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
    }
}
