using UnityEngine;

/// <summary>
/// Controls a rotating hinge-based obstacle.
/// </summary>
public class HingeRotate : MonoBehaviour
{
    private HingeJoint2D hinge;

    [Header("Rotation Settings")]
    public float motorSpeed = 100f; // Speed of rotation
    public float maxMotorForce = 1000f; // Strength of the motor

    private void Start()
    {
        hinge = GetComponent<HingeJoint2D>();

        // Setup motor
        JointMotor2D motor = hinge.motor;
        motor.motorSpeed = motorSpeed;
        motor.maxMotorTorque = maxMotorForce;
        hinge.motor = motor;
        hinge.useMotor = true;
    }
}
