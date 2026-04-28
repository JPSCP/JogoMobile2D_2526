using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    private Rigidbody2D Rb;

    public float gravityScale = 3f;

    private bool isUpsideDown = false;

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Rb.gravityScale = gravityScale;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            FlipGravity();
        }
    }

    void FlipGravity()
    {
        isUpsideDown = !isUpsideDown;

        // Flip ONLY vertical gravity
        Rb.gravityScale = isUpsideDown ? -gravityScale : gravityScale;

        // Optional: flip sprite visually (WITHOUT rotating physics)
        Vector3 scale = transform.localScale;
        scale.y = isUpsideDown ? -Mathf.Abs(scale.y) : Mathf.Abs(scale.y);
        transform.localScale = scale;
    }
}
