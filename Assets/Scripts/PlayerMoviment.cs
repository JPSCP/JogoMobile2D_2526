using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    private Rigidbody2D Rb;

    [Header("Movement")]
    public float gravityScale = 4f;

    [Header("Jump")]
    public float jumpForce = 12f;

    [Header("Flip")]
    public float flipBoost = 6f;

    [Header("Ground Check")]
    public Transform groundCheckDown;
    public Transform groundCheckUp;
    public float groundDistance = 0.3f;
    public LayerMask groundLayer;

    [Header("Audio")]
    AudioSource audioSource;
    public AudioClip jumpClip;
    public AudioClip landClip;

    private bool isUpsideDown = false;
    private bool isGrounded = false;

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();

        Rb.gravityScale = gravityScale;
        Rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (GameManager.Instance == null || GameManager.Instance.isGameOver)
            return;

        CheckGround();
        CheckBounds();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance == null || GameManager.Instance.isGameOver)
            return;

        float speed = GameManager.Instance.GetSpeed();

        Rb.linearVelocity = new Vector2(
            speed,
            Rb.linearVelocity.y
        );
    }
    public void OnJumpButton()
    {
        Debug.Log("JUMP BUTTON");
        Jump();
    }

    public void OnFlipButton()
    {
        FlipGravity();
    }
    void Jump()
    {
        if (!isGrounded)
            return;

        audioSource.PlayOneShot(jumpClip);

        float direction = isUpsideDown ? -1f : 1f;

        Vector2 velocity = Rb.linearVelocity;
        velocity.y = 0f;

        Rb.linearVelocity = velocity;

        Rb.AddForce(
            Vector2.up * jumpForce * direction,
            ForceMode2D.Impulse
        );
    }

    void FlipGravity()
    {
        isUpsideDown = !isUpsideDown;

        audioSource.PlayOneShot(jumpClip);

        // Inverte gravidade
        Rb.gravityScale = isUpsideDown
            ? -gravityScale
            : gravityScale;

        // Mantém movimento horizontal e suaviza vertical
        Vector2 velocity = Rb.linearVelocity;
        velocity.y *= -0.5f;

        Rb.linearVelocity = velocity;

        // Pequeno impulso para suavidade
        Rb.AddForce(
            Vector2.up * (isUpsideDown ? -flipBoost : flipBoost),
            ForceMode2D.Impulse
        );

        // Flip visual
        Vector3 scale = transform.localScale;

        scale.y = isUpsideDown
            ? -Mathf.Abs(scale.y)
            : Mathf.Abs(scale.y);

        transform.localScale = scale;
    }

    void CheckGround()
    {
        Transform checkPoint = isUpsideDown
            ? groundCheckUp
            : groundCheckDown;

        isGrounded = Physics2D.OverlapCircle(
            checkPoint.position,
            groundDistance,
            groundLayer
        );
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
        Vector3 screenPos =
            Camera.main.WorldToViewportPoint(transform.position);

        if (screenPos.x < -0.1f ||
            screenPos.y < -0.1f ||
            screenPos.y > 1.1f)
        {
            GameManager.Instance.GameOver();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        if (groundCheckDown != null)
        {
            Gizmos.DrawWireSphere(
                groundCheckDown.position,
                groundDistance
            );
        }

        if (groundCheckUp != null)
        {
            Gizmos.DrawWireSphere(
                groundCheckUp.position,
                groundDistance
            );
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}