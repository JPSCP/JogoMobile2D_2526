using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    private Rigidbody2D Rb;

    [Header("Movement")]
    public float gravityScale = 4f;

    [Header("Jump")]
    public float jumpForce = 12f;

    [Header("Ground Check")]
    public Transform groundCheckDown;
    public Transform groundCheckUp;
    public float groundDistance = 0.2f;
    public LayerMask groundLayer;

    private bool isUpsideDown = false;
    private bool isGrounded = false;
    private bool canJump = false;

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Rb.gravityScale = gravityScale;
    }

    private void Update()
    {
        if (GameManager.Instance == null || GameManager.Instance.isGameOver) return;

        CheckGround();
        CheckBounds();
        HandleInput();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance == null || GameManager.Instance.isGameOver) return;

        // Movimento automático (velocidade do GameManager)
        float speed = GameManager.Instance.GetSpeed();
        Rb.linearVelocity = new Vector2(speed, Rb.linearVelocity.y);
    }

    void HandleInput()
    {
        // MOBILE
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (touch.position.x < Screen.width / 2)
                {
                    Jump();
                }
                else
                {
                    FlipGravity();
                }
            }
        }

        // PC (para testes)
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.mousePosition.x < Screen.width / 2)
            {
                Jump();
            }
            else
            {
                FlipGravity();
            }
        }
    }

    void Jump()
    {
        if (!canJump) return;

        canJump = false;

        float direction = isUpsideDown ? -1f : 1f;

        Rb.linearVelocity = new Vector2(Rb.linearVelocity.x, 0f);
        Rb.AddForce(Vector2.up * jumpForce * direction, ForceMode2D.Impulse);
    }

    void FlipGravity()
    {
        isUpsideDown = !isUpsideDown;

        Rb.gravityScale = isUpsideDown ? -gravityScale : gravityScale;

        // Flip visual
        Vector3 scale = transform.localScale;
        scale.y = isUpsideDown ? -Mathf.Abs(scale.y) : Mathf.Abs(scale.y);
        transform.localScale = scale;
    }

    void CheckGround()
    {
        Transform checkPoint = isUpsideDown ? groundCheckUp : groundCheckDown;

        isGrounded = Physics2D.OverlapCircle(checkPoint.position, groundDistance, groundLayer);

        if (isGrounded)
        {
            Rb.linearVelocity = new Vector2(Rb.linearVelocity.x, 0f);
            canJump = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            GameManager.Instance.GameOver();
        }
    }

    void CheckBounds()
    {
        Vector3 screenPos = Camera.main.WorldToViewportPoint(transform.position);

        if (screenPos.x < -0.1f || screenPos.y < -0.5f || screenPos.y > 1.1f)
        {
            GameManager.Instance.GameOver();
            canJump = true;
        }
    }
}