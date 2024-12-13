using UnityEngine;

public class cameraflow : MonoBehaviour
{
    public Transform player;           // Reference to the player's Transform
    public float smoothSpeed = 0.125f; // Speed of the camera's smooth movement
    public Vector3 offset;            // Offset from the player's position

    // Camera boundaries (only horizontal limits)
    public float minX = -8.75f, maxX = 40.01f;

    void LateUpdate()
    {
        if (player != null)
        {
            // Target position for the camera based on the player's position and offset
            Vector3 targetPosition = player.position + offset;

            // Clamp the camera's X position to stay within the defined horizontal boundaries
            float clampedX = Mathf.Clamp(targetPosition.x, minX, maxX);

            // Keep the camera's Y position fixed (it doesn't move vertically) and set the Z position to match camera
            targetPosition = new Vector3(clampedX, transform.position.y, transform.position.z);

            // Smoothly move the camera to the target position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

            // Update the camera's position
            transform.position = smoothedPosition;
        }
    }
}
