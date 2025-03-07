using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public int gateID; // Unique ID for the gate this collectible affects
    public AudioClip collectSFX; // Assign this in the Inspector
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Get AudioSource component
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Find the correct gate and update it
            HingeGate[] allGates = FindObjectsOfType<HingeGate>();
            foreach (HingeGate gate in allGates)
            {
                if (gate.gateID == gateID)
                {
                    gate.IncreaseHingeAngle();
                    break;
                }
            }

            if (audioSource && collectSFX)
                audioSource.PlayOneShot(collectSFX);

            Destroy(gameObject);
            Debug.Log($"Item collected for Gate {gateID}!");
        }
    }
}
