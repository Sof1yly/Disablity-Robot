using UnityEngine;
[CreateAssetMenu(fileName = "MovementSettings", menuName = "Leg/MovementSettings")]
public class MovementSettings : ScriptableObject
{
    [Header("Speeds")]
    public float maxSpeed = 5f;
    public float jumpForce = 5f;

    //no need to adjust those below value

    [Header("Rotation")]
    public float rotationSpeed = 360f;

    [Header("Accel / Decel")]
    public float accelerationFactor = 5f;
    public float decelerationFactor = 10f;

    [Header("Gravity")]
    public float gravity = -9.81f;
}