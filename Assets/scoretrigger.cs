using UnityEngine;
using TMPro; // Make sure to include this for TextMeshPro elements

public class ScoreTrigger : MonoBehaviour
{
    public int score = 0; // Current score
    public Transform respawnPoint; // Reference to the respawn location
    public TextMeshProUGUI scoreText; // Reference to the TextMeshPro element

    private void Start()
    {
        // Validate the respawn point
        if (respawnPoint == null)
        {
            Debug.LogError("Respawn Point is not assigned!");
        }

        // Initialize the score display
        UpdateScoreDisplay();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger has the tag "GamePiece"
        if (other.CompareTag("GamePiece"))
        {
            // Increase score
            score++;
            Debug.Log("Score: " + score);

            // Update the score display
            UpdateScoreDisplay();

            // Teleport the object
            TeleportObject(other.gameObject);
        }
    }

    private void TeleportObject(GameObject obj)
    {
        Debug.Log("Teleporting object.");

        // Check if respawn point is set
        if (respawnPoint != null)
        {
            // Move the object to the respawn point
            obj.transform.position = respawnPoint.position;
            obj.transform.rotation = respawnPoint.rotation;

            // Ensure the object's Rigidbody settings
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero; // Stop any residual movement
                rb.angularVelocity = Vector3.zero; // Stop any residual rotation
            }
        }
        else
        {
            Debug.LogError("Respawn Point is not set.");
        }
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        else
        {
            Debug.LogError("Score TextMeshPro UI element is not assigned.");
        }
    }
}
