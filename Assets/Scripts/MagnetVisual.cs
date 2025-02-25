using UnityEngine;

/// <summary>
/// Dynamically displays the attraction range of a magnet.
/// </summary>
public class MagnetVisual : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private int segments = 50; // Circle resolution
    private MagnetController magnet;

    private void Start()
    {
        magnet = GetComponentInParent<MagnetController>(); // Get the MagnetController component from the parent
        if (magnet == null)
        {
            Debug.LogError("MagnetVisual: No MagnetController found on parent object!");
            return;
        }

        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.positionCount = segments + 1;
        lineRenderer.loop = true;
        lineRenderer.widthMultiplier = 0.05f;

        UpdateVisual();
    }

    /// <summary>
    /// Updates the visual radius dynamically.
    /// </summary>
    private void UpdateVisual()
    {
        float attractionRadius = magnet.GetAttractionRadius(); // Get updated attraction radius
        float angleStep = 360f / segments;

        for (int i = 0; i <= segments; i++)
        {
            float angle = Mathf.Deg2Rad * i * angleStep;
            float x = Mathf.Cos(angle) * attractionRadius;
            float y = Mathf.Sin(angle) * attractionRadius;
            lineRenderer.SetPosition(i, new Vector3(x, y, 0) + transform.position);
        }
    }

    private void Update()
    {
        UpdateVisual(); // Continuously update in case attraction radius changes dynamically
    }
}
