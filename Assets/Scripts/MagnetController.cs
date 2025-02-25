using UnityEngine;

/// <summary>
/// Controls the behavior of a magnet, applying attraction and repulsion forces.
/// </summary>
public class MagnetController : MonoBehaviour
{
    public float attractionForce = 5f;
    public float repulsionForce = 8f;
    public float attractionRadius = 3f; // The actual attraction radius

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            Vector2 direction = (transform.position - other.transform.position).normalized;
            rb.AddForce(direction * attractionForce, ForceMode2D.Force);
        }
    }

    /// <summary>
    /// Provides the current attraction radius for the MagnetVisual.
    /// </summary>
    public float GetAttractionRadius()
    {
        return attractionRadius;
    }
}
