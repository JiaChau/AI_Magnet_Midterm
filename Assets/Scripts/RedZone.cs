using UnityEngine;

public class RedZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerRespawn playerRespawn = other.GetComponent<PlayerRespawn>();
            if (playerRespawn != null)
            {
                playerRespawn.RespawnPlayer(); // Reset player to last checkpoint
                Debug.Log("Player Hit Red Zone! Respawning...");
            }
        }
    }
}
