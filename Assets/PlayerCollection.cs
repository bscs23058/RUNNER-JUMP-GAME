using UnityEngine;
using UnityEngine.UI;

public class PlayerCollection : MonoBehaviour
{
    public Image coinImage; // Reference to the UI Image for displaying the coin icon
    public Image heartImage; // Reference to the UI Image for displaying the heart icon

    public Text coinCountText; // Text for showing the number of coins
    public Text heartCountText; // Text for showing the number of hearts

    public GameObject restartButton; // Reference to the restart button UI

    public AudioClip coinSound; // Audio clip for coin collection
    public AudioClip heartSound; // Audio clip for heart collection
    public AudioClip monsterHitSound; // Audio clip for monster life reduction
    public Image[] monsterHearts; // Monster heart UI elements

    private AudioSource audioSource; // Reference to AudioSource
    private int coins = 0; // Coin count
    private int hearts = 3; // Heart count, default 3
    
    public bool isGamePaused = false;
    void Start()
    {
        // Initialize AudioSource
        audioSource = GetComponent<AudioSource>();

        // Hide the restart button at the start
        if (restartButton != null)
        {
            restartButton.SetActive(false);
        }

        // Initialize monster hearts UI
        foreach (var heart in monsterHearts)
        {
            heart.enabled = true;
        }

        // Update the UI with initial values
        UpdateUI();
    }

    public void ReduceHeart()
    {
        // Decrease hearts and update UI
        hearts--;
        UpdateUI();

        // Play heart reduction sound
        PlaySound(heartSound);

        // If hearts reach 0, show the restart button
        if (hearts <= 0 && restartButton != null)
        {
            restartButton.SetActive(true);
            isGamePaused = true;
        }
    }

    void UpdateUI()
    {
        // Update the coin count UI
        if (coinCountText != null)
        {
            coinCountText.text = "x " + coins.ToString();
        }

        // Update the heart count UI
        if (heartCountText != null)
        {
            heartCountText.text = "x " + hearts.ToString();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("coin"))
        {
            coins++;
            Destroy(collision.gameObject);
            PlaySound(coinSound); // Play coin collection sound
            UpdateUI();
        }
        Debug.Log("Triggered with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("heart"))
        {
            Debug.Log("Heart collected!");
            if (hearts < 3)
            {
                hearts++;
                Destroy(collision.gameObject);
                PlaySound(heartSound);
                UpdateUI();
            }
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
