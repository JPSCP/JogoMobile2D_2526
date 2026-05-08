using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip collectedClip;

    public int value = 50;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Pega o script do player
            PlayerMoviment player =
                other.GetComponent<PlayerMoviment>();

            // Toca o som
            if (player != null)
            {
                player.PlaySound(collectedClip);
            }

            GameManager.Instance.AddScore(value);

            Destroy(gameObject);
        }
    }
}
