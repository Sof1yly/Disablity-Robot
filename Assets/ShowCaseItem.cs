using UnityEngine;

public class ShowCaseItem : MonoBehaviour
{
    // --- Floating Properties ---
    [Header("Floating Settings")]
    [Tooltip("How high the object will float from its starting position.")]
    public float floatHeight = 0.5f;
    [Tooltip("How fast the object will float up and down.")]
    public float floatSpeed = 1f;

    private Vector3 startPos;

    // --- Rotation Properties ---
    [Header("Rotation Settings")]
    [Tooltip("Speed of rotation around the Y-axis (degrees per second).")]
    public float rotationSpeed = 30f;

    void Start()
    {
        // Store the initial position of the object
        startPos = transform.position;
    }

    void Update()
    {
        // --- Floating ---
        // Calculate the new Y position using a sine wave for smooth up and down motion
        float newY = startPos.y + (Mathf.Sin(Time.time * floatSpeed) * floatHeight);
        transform.position = new Vector3(this.transform.position.x, newY, this.transform.position.z);

        // --- Rotation ---
        // Rotate the object around its Y-axis
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
