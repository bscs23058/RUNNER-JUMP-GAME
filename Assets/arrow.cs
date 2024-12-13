using UnityEngine;

public class arrow : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Arrow collided with: " + collision.gameObject.name);

        if (collision.CompareTag("monster"))
        {
            monster monsterScript = collision.GetComponent<monster>();
            if (monsterScript != null)
            {
                monsterScript.TakeHit(); // Notify monster of a hit
            }

            Destroy(gameObject); // Destroy the arrow
        }
        else if (collision.CompareTag("ground"))
        {
            Destroy(gameObject); // Destroy the arrow on the ground
        }
    }
}
