using UnityEngine;

public class chain : MonoBehaviour
{
    public float swingAngle = 45f; // Maximum angle of the swing
    public float swingSpeed = 2f;  // Speed of the swinging motion
    private float startAngle;      // Starting rotation angle

    void Start()
    {
        // Store the initial rotation angle
        startAngle = transform.rotation.eulerAngles.z;
    }

    void Update()
    {
        // Calculate the swing angle using a sine wave for smooth motion
        float angle = startAngle + Mathf.Sin(Time.time * swingSpeed) * swingAngle;

        // Apply the rotation to the chain/blade
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the player collides with the swinging blade
        if (collision.CompareTag("player"))
        {
            PlayerCollection player = collision.GetComponent<PlayerCollection>();
            if (player != null)
            {
                player.ReduceHeart(); // Reduce hearts when hit by the blade
            }
        }
    }
}
