using UnityEngine;

/// <summary>
/// Handles player respawning at the last checkpoint.
/// Automatically updates the checkpoint when the player reaches a new one.
/// </summary>
public class PlayerRespawn : MonoBehaviour
{
    public static Vector2 lastCheckpointPosition; // Stores last checkpoint

    private Rigidbody2D rb;
    private PlayerMagnetMovement magnetMovement; // Reference to the magnet script

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        magnetMovement = GetComponent<PlayerMagnetMovement>();

        lastCheckpointPosition = transform.position; // Set starting position as checkpoint
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // Press R to respawn
        {
            RespawnPlayer();
        }
    }

    /// <summary>
    /// Moves the player to the last checkpoint and resets movement.
    /// Also detaches from any active magnet to avoid glitches.
    /// </summary>
    public void RespawnPlayer()
    {
        if (magnetMovement != null && magnetMovement.IsAttached())
        {
            magnetMovement.DetachFromMagnet(); // Properly detach before respawning
        }

        transform.position = lastCheckpointPosition; // Move to last checkpoint
        rb.velocity = Vector2.zero; // Reset movement to prevent drifting

        Debug.Log("Player Respawned at: " + lastCheckpointPosition);
    }

    /// <summary>
    /// Updates the checkpoint location when the player reaches a new one.
    /// </summary>
    public static void UpdateCheckpoint(Vector2 newCheckpoint)
    {
        lastCheckpointPosition = newCheckpoint;
        Debug.Log("Checkpoint updated to: " + newCheckpoint);
    }
}
