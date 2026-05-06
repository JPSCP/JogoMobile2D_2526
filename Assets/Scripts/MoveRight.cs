using UnityEngine;

public class MoveRight : MonoBehaviour
{
    public float speedMultiplier = 1f; // 1 = mesma velocidade, <1 = parallax

    private float spriteWidth;

    private void Start()
    {
        // Calcula largura do sprite
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        spriteWidth = sr.bounds.size.x;
    }

    private void Update()
    {
        if (GameManager.Instance == null || GameManager.Instance.isGameOver) return;

        float speed = GameManager.Instance.GetSpeed() * speedMultiplier;

        // Move para a esquerda
        transform.position += Vector3.right * speed * Time.deltaTime;

    }
}
