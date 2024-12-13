using System.Collections;
using TMPro;
using UnityEngine;

public class letterbyletter : MonoBehaviour
{
    public TextMeshProUGUI textField;          // Reference to the Text UI component
    public string fullText;                    // The full text to display
    public float typingSpeed = 0.1f;           // Speed of revealing each letter

    void Start()
    {
        // Start the coroutine to display text letter by letter
        StartCoroutine(DisplayText());
    }

    IEnumerator DisplayText()
    {
        textField.text = ""; // Clear the text initially
        foreach (char letter in fullText)
        {
            textField.text += letter; // Add one letter at a time
            yield return new WaitForSeconds(typingSpeed); // Wait for the typing speed
        }
    }
}
