using UnityEngine;

public class InsectController : MonoBehaviour
{
    public float moveDistance = 2f; // Distance to move left and right
    public float moveSpeed = 2f;    // Speed of movement
    private Vector2 startPosition; // Initial position of the insect

    private bool movingRight = true; // Tracks the direction of movement

    void Start()
    {
        // Save the initial position of the insect
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the movement range
        float targetX = startPosition.x + (movingRight ? moveDistance : -moveDistance);

        // Move the insect
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(targetX, transform.position.y), moveSpeed * Time.deltaTime);

        // Switch direction when reaching the limit
        if (Mathf.Abs(transform.position.x - targetX) < 0.01f)
        {
            movingRight = !movingRight;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player collides with the insect
        if (collision.CompareTag("player"))
        {
            PlayerCollection player = collision.GetComponent<PlayerCollection>();
            if (player != null)
            {
                player.ReduceHeart(); // Call the method to reduce hearts
            }
        }
    }
}
