using UnityEngine;

public enum SpeedMode
{
    Instant,
    Accelerated
}

[CreateAssetMenu(fileName = "MovementSettings", menuName = "Leg/MovementSettings")]
public class MovementSettings : ScriptableObject
{
    [Header("Speeds")]
    public float maxSpeed = 5f;
    public float jumpForce = 5f;
    public SpeedMode speedMode = SpeedMode.Accelerated;

    //no need to adjust those below value

    [Header("Rotation")]
    public float rotationSpeed = 360f;

    [Header("Accel / Decel")]
    public float accelerationFactor = 5f;
    public float decelerationFactor = 10f;

    public float groundSkin = 0.08f;

    [Header("Gravity")]
    public float gravity = -9.81f;
}