using UnityEngine;

[CreateAssetMenu(fileName = "FPScontrollSettings", menuName = "FPSsettings)")]
public class FPScontrollSettings : ScriptableObject
{
    [Header("movement Settings")]
    public float walkSpeed = 5.0f;
    public float sprintMultiplier = 2.0f;

    [Header("Jump Settings")]
    public float jumpForce = 5.0f;
    public float gravityMultiplier = 1.0f;

    [Header("Mouse Sensitivity")]
    public float mouseSensitivity = 1.0f;
    public float verticalLookRange = 80.0f;


}
