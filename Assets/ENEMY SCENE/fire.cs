using UnityEngine;

public class fire : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("player"))
        {
            PlayerCollection player = collision.GetComponent<PlayerCollection>();
            if (player != null)
            {
                player.ReduceHeart();
            }
            Destroy(gameObject); // Destroy the fire after hitting the player
        }
        else if (collision.CompareTag("ground") )
        {
            Destroy(gameObject); // Destroy the fire on ground/obstacle collision
        }
    }

}
