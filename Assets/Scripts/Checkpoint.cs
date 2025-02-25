using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Transform respawnPoint; // The exact point where the player respawns

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ensure it's the player
        {
            PlayerRespawn.lastCheckpointPosition = respawnPoint.position; // Update respawn point
            Debug.Log("Checkpoint Updated: " + respawnPoint.position);
        }
    }
}
