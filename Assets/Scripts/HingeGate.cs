using UnityEngine;
using TMPro;

/// <summary>
/// Controls a hinged gate that opens progressively as the player collects specific collectibles.
/// Displays a counter showing the number of collected items.
/// </summary>
public class HingeGate : MonoBehaviour
{
    [Header("Hinge Components")]
    public HingeJoint2D leftHinge, rightHinge;
    public int gateID; // Unique ID for this gate

    [Header("Gate Settings")]
    public int requiredItemsToPass = 6; // Number of collectibles required to pass
    public int maxCollectibles = 7; // Total possible collectibles (can go beyond requirement)
    public float itemOpenAmount = 10f; // Degrees per collectible
    public float maxOpenAngle = 80f;   // Maximum rotation before fully open

    [Header("UI Elements")]
    public TextMeshProUGUI progressText; // **Manually placed TMP text in the scene**

    private int collectedItems = 0;
    private float currentTargetAngle = 0f;

    private void Start()
    {
        UpdateGateText(); // Initialize text display
        SetDoorAngle(0f); // Ensure doors start fully closed
    }

    /// <summary>
    /// Increases hinge rotation based on collected items.
    /// </summary>
    public void IncreaseHingeAngle()
    {
        if (collectedItems < maxCollectibles)
        {
            collectedItems++;
            float newTargetAngle = Mathf.Clamp(collectedItems * itemOpenAmount, 0f, maxOpenAngle);

            if (newTargetAngle > currentTargetAngle)
            {
                currentTargetAngle = newTargetAngle;
                SetDoorAngle(currentTargetAngle);
            }

            UpdateGateText();
        }
    }

    /// <summary>
    /// Updates the TMP text above the gate to reflect progress.
    /// </summary>
    private void UpdateGateText()
    {
        if (progressText != null)
        {
            progressText.text = $"{collectedItems}/{requiredItemsToPass}";

            if (collectedItems >= requiredItemsToPass)
            {
                progressText.color = Color.green; // Indicate the gate is fully openable
            }
            else
            {
                progressText.color = Color.red; // Still needs more collectibles
            }
        }
    }

    /// <summary>
    /// Updates hinge limits for both doors.
    /// </summary>
    private void SetDoorAngle(float angle)
    {
        if (leftHinge != null)
        {
            JointAngleLimits2D leftLimits = leftHinge.limits;
            leftLimits.min = -angle;
            leftLimits.max = 0f;
            leftHinge.limits = leftLimits;
            leftHinge.useLimits = true;
        }

        if (rightHinge != null)
        {
            JointAngleLimits2D rightLimits = rightHinge.limits;
            rightLimits.min = 0f;
            rightLimits.max = angle;
            rightHinge.limits = rightLimits;
            rightHinge.useLimits = true;
        }
    }
}
