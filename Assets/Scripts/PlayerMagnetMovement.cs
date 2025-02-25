using UnityEngine;

/// <summary>
/// Handles player movement, orbiting magnets with controlled gravity to prevent indefinite floating.
/// </summary>
public class PlayerMagnetMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpringJoint2D spring;
    private Transform magnetTransform;
    private MagnetController currentMagnet;
    private bool isAttached = false;

    [Header("Magnet Settings")]
    public float repelForce = 10f;  // Strength of repulsion
    public float springFrequency = 2.5f; // Attraction strength
    public float dampingRatio = 0.4f;  // Controls stability
    public float minSpringDistance = 1.5f; // Minimum compression
    private float maxSpringDistance = 4.0f; // Determined per magnet

    [Header("Orbit Settings")]
    public float startOrbitSpeed = 2.0f;  // Initial orbit speed
    public float maxOrbitSpeed = 5.0f;    // Speed cap
    public float speedIncreaseRate = 0.5f; // Rate at which speed increases per second
    public float stretchSpeedMultiplier = 1.5f; // Speed of spring expansion/contraction

    [Header("Gravity Control")]
    public float detachedGravityScale = 0.5f; // Lighter gravity for controlled descent
    public float attachedGravityScale = 0f; // No gravity when attached to a magnet

    private float currentOrbitSpeed;
    private float attachTime = 0f; // Tracks how long the player has been attached
    private bool stretching = true; // Determines if the spring is expanding or contracting

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spring = gameObject.AddComponent<SpringJoint2D>();
        spring.enabled = false; // Initially disabled
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isAttached) DetachFromMagnet();
            else AttachToNearestMagnet();
        }
    }

    private void FixedUpdate()
    {
        if (isAttached)
        {
            ApplyOscillatingOrbit();
        }
    }

    /// <summary>
    /// Attaches the player to the nearest magnet within that magnet's unique range.
    /// </summary>
    private void AttachToNearestMagnet()
    {
        Collider2D[] magnets = Physics2D.OverlapCircleAll(transform.position, 10f);
        Transform closestMagnet = null;
        float closestDistance = Mathf.Infinity;
        float magnetRange = 0f;
        MagnetController detectedMagnet = null;

        foreach (var magnet in magnets)
        {
            if (magnet.CompareTag("Magnet"))
            {
                MagnetController magnetController = magnet.GetComponent<MagnetController>();

                if (magnetController != null)
                {
                    float distance = Vector2.Distance(transform.position, magnet.transform.position);

                    if (distance < magnetController.attractionRadius && distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestMagnet = magnet.transform;
                        detectedMagnet = magnetController;
                        magnetRange = magnetController.attractionRadius;
                    }
                }
            }
        }

        if (closestMagnet != null && detectedMagnet != null)
        {
            magnetTransform = closestMagnet;
            currentMagnet = detectedMagnet;
            maxSpringDistance = magnetRange;

            spring.connectedAnchor = magnetTransform.position;
            spring.frequency = springFrequency;
            spring.dampingRatio = dampingRatio;
            spring.autoConfigureDistance = false;
            spring.distance = minSpringDistance;
            spring.enabled = true;
            isAttached = true;
            stretching = true;
            attachTime = 0f;
            currentOrbitSpeed = startOrbitSpeed;

            rb.gravityScale = attachedGravityScale; // Disable gravity when attached

            Vector2 initialDir = (transform.position - magnetTransform.position).normalized;
            rb.velocity = new Vector2(-initialDir.y, initialDir.x) * currentOrbitSpeed;
        }
    }

    /// <summary>
    /// Detaches the player from the magnet and applies a repulsion force.
    /// </summary>
    public void DetachFromMagnet()
    {
        if (!isAttached) return;

        isAttached = false;
        spring.enabled = false;
        rb.gravityScale = detachedGravityScale; // Enable gravity for a natural fall

        Vector2 repelDirection = (transform.position - magnetTransform.position).normalized;
        rb.AddForce(repelDirection * repelForce, ForceMode2D.Impulse);
    }

    /// <summary>
    /// Applies circular orbiting behavior with speed and spring length modulation.
    /// </summary>
    private void ApplyOscillatingOrbit()
    {
        if (magnetTransform == null) return;

        attachTime += Time.fixedDeltaTime;
        currentOrbitSpeed = Mathf.Min(startOrbitSpeed + (speedIncreaseRate * attachTime), maxOrbitSpeed);

        Vector2 toMagnet = (magnetTransform.position - transform.position).normalized;
        Vector2 perpendicularDirection = new Vector2(-toMagnet.y, toMagnet.x);

        if (stretching)
        {
            spring.distance = Mathf.Min(spring.distance + (stretchSpeedMultiplier * Time.fixedDeltaTime), maxSpringDistance);
            if (spring.distance >= maxSpringDistance) stretching = false;
        }
        else
        {
            spring.distance = Mathf.Max(spring.distance - (stretchSpeedMultiplier * Time.fixedDeltaTime), minSpringDistance);
            if (spring.distance <= minSpringDistance) stretching = true;
        }

        rb.velocity = perpendicularDirection * currentOrbitSpeed;

        float currentDistance = Vector2.Distance(transform.position, magnetTransform.position);
        if (currentDistance > maxSpringDistance)
        {
            rb.AddForce(-toMagnet * (currentDistance - maxSpringDistance) * 3f, ForceMode2D.Force);
        }
    }

    public bool IsAttached()
    {
        return isAttached;
    }

}
