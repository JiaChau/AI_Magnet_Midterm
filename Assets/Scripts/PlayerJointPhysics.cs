using UnityEngine;

/// <summary>
/// Handles the player's physics-based movement when using joints.
/// </summary>
public class PlayerJointPhysics : MonoBehaviour
{
    public SpringJoint2D spring;
    public Rigidbody2D rb;
    public float oscillationFrequency = 2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spring = GetComponent<SpringJoint2D>();
        spring.enabled = false;
    }

    private void FixedUpdate()
    {
        if (spring.enabled)
        {
            float oscillationForce = Mathf.Sin(Time.time * oscillationFrequency) * 0.5f;
            Vector2 direction = (spring.connectedAnchor - (Vector2)transform.position).normalized;
            rb.AddForce(direction * oscillationForce, ForceMode2D.Force);
        }
    }
}
