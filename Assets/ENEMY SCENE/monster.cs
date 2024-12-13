using UnityEngine;
using UnityEngine.UI;

public class monster : MonoBehaviour
{
    public Sprite defaultSprite;      // Default monster sprite
    public Sprite fireSprite;         // Sprite when shooting fire
    public GameObject firePrefab;     // Fire prefab to instantiate
    public Transform fireSpawnPoint;  // Fire spawn point
    public float fireSpeed = 5f;      // Speed of the fire projectile

    public Image[] monsterHearts;     // References to heart images on the player's UI canvas
    public int maxLives = 3;          // Total number of lives for the monster
    public GameObject catdoorPrefab;

    private int currentLives;         // Current number of lives
    private int arrowHits;            // Count of consecutive arrow hits

    private SpriteRenderer spriteRenderer;
    public AudioClip monsterHitSound; // Sound for monster getting hit
    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentLives = maxLives;
        arrowHits = 0;
        InvokeRepeating("ShootFire", 2f, 4f); // Fire every 4 seconds
    }

    void ShootFire()
    {
        spriteRenderer.sprite = fireSprite;

        GameObject fire = Instantiate(firePrefab, fireSpawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = fire.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.left * fireSpeed; // Move left
        }

        Invoke(nameof(ResetSprite), 0.5f);
    }

    void ResetSprite()
    {
        spriteRenderer.sprite = defaultSprite;
    }

    public void TakeHit()
    {
        if (monsterHitSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(monsterHitSound);
        }
        arrowHits++;
        if (arrowHits >= 3)
        {
            ReduceLife();
            arrowHits = 0; // Reset arrow hit count
        }
    }

    private void ReduceLife()
    {
        currentLives--;

        // Update UI
        if (currentLives >= 0 && currentLives < monsterHearts.Length)
        {
            monsterHearts[currentLives].enabled = false; // Disable a heart image
        }

        if (currentLives <= 0)
        {
            SpawnCatdoor();
            Destroy(gameObject); // Destroy monster when lives are exhausted
        }
    }
    private void SpawnCatdoor()
    {
        if (catdoorPrefab != null)
        {
            Instantiate(catdoorPrefab, transform.position, Quaternion.identity);
        }
    }
}
