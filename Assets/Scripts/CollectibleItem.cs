using UnityEngine;

/// <summary>
/// Calls IncreaseHingeAngle on the correct HingeGate when the player collects an item.
/// </summary>
public class CollectibleItem : MonoBehaviour
{
    public int gateID; // Unique ID for the gate this collectible affects

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Find the correct gate by ID and apply the collectible effect
            HingeGate[] allGates = FindObjectsOfType<HingeGate>();
            foreach (HingeGate gate in allGates)
            {
                if (gate.gateID == gateID) // Match the correct gate
                {
                    gate.IncreaseHingeAngle(); // Pass the required ID
                    break;
                }
            }

            Destroy(gameObject); // Remove the collectible after it's collected
            Debug.Log($"Item collected for Gate {gateID}!");
        }
    }
}
