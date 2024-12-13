using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpriteController : MonoBehaviour
{
    public Sprite idleSprite;  // Idle sprite
    public Sprite runSprite;   // Running sprite
    public Sprite jumpSprite;  // Jumping sprite
    public GameObject arrowPrefab; // Prefab for the arrow
    public Transform arrowSpawnPoint; // Point where the arrow will spawn
    public float arrowSpeed = 10f; // Speed of the arrow

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    public float moveSpeed = 5f;  // Speed for horizontal movement
    public float jumpForce = 10f; // Force applied for jumping

    private bool isGrounded = true; // Tracks if the player is on the ground
    private PlayerCollection playerCollection;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        // Reference PlayerCollection to check the pause state
        playerCollection = FindObjectOfType<PlayerCollection>();
    }

    void Update()
    {
        if (playerCollection != null && playerCollection.isGamePaused)
        {
            rb.velocity = Vector2.zero; // Stop all movement
            return; // Exit Update to prevent further input
        }
        // Detect horizontal movement (left/right)
        float horizontal = Input.GetAxis("Horizontal");

        if (Mathf.Abs(horizontal) > 0.1f)
        {
            spriteRenderer.sprite = runSprite;
            spriteRenderer.flipX = horizontal < 0; // Flip sprite if moving left
            rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        }
        else
        {
            spriteRenderer.sprite = idleSprite;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        // Detect jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            spriteRenderer.sprite = jumpSprite;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isGrounded = false; // Prevent jumping mid-air
        }

        // Reset to Idle when grounded and not moving
        if (isGrounded && Mathf.Abs(horizontal) < 0.1f)
        {
            spriteRenderer.sprite = idleSprite;
        }

        // Shoot arrow when F key is pressed
        if (Input.GetKeyDown(KeyCode.F))
        {
            ShootArrow();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.contacts.Length > 0)
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            SceneManager.LoadScene("enemy scene");
        }
        if (collision.CompareTag("Catdoor"))
        {
            SceneManager.LoadScene("end scene"); // Replace "EndScene" with the name of your end scene
        }
    }

    private void ShootArrow()
    {
        // Instantiate the arrow at the spawn point
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, Quaternion.identity);

        // Set the direction of the arrow
        Rigidbody2D arrowRb = arrow.GetComponent<Rigidbody2D>();
        float direction = spriteRenderer.flipX ? -1 : 1; // Flip direction if the sprite is flipped
        arrowRb.velocity = new Vector2(arrowSpeed * direction, 0);

        // Flip the arrow sprite if moving left
        SpriteRenderer arrowSpriteRenderer = arrow.GetComponent<SpriteRenderer>();
        if (arrowSpriteRenderer != null)
        {
            arrowSpriteRenderer.flipX = direction < 0;
        }
    }
}
