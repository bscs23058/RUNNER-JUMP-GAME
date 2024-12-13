using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public PlayerCollection playerCollection;

    public void RestartGame()
    {
        if (playerCollection != null)
        {
            playerCollection.isGamePaused = false; // Resume the game
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
