using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] MovementSettings baseSetting;
    private MovementSettings currentSetting;
    [SerializeField] private LayerMask solidMask = ~0;

    [Header("Slope & Acceleration")]
    [Range(0f, 89f)] public float maxWalkSlope = 45f;
    public float accelTime = 0.12f;

    private Rigidbody rb;
    private CapsuleCollider col;
    private InputSystem_Actions input;

    private Vector3 rawInputDir;
    private Vector3 horizVel;
    private Vector3 smoothRef;

    private bool jumpQueued;


    void Awake()
    {
        currentSetting = baseSetting;

        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        col = GetComponent<CapsuleCollider>();
        input = new InputSystem_Actions();
    }

    void OnEnable() { input.Player.Enable(); }
    void OnDisable() { input.Player.Disable(); }

    void Update()
    {
        Vector2 raw = input.Player.Move.ReadValue<Vector2>();
        rawInputDir = new Vector3(raw.x, 0f, raw.y).normalized;

        if (input.Player.Jump.triggered && IsGrounded())
        {
            jumpQueued = true;
        }

        if (rawInputDir.sqrMagnitude > 0.001f)
        {
            Vector3 iso = Quaternion.Euler(0f, 45f, 0f) * rawInputDir;
            Quaternion tgt = Quaternion.LookRotation(iso, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, tgt, currentSetting.rotationSpeed * Time.deltaTime);
        }

        float targetSpeed = rawInputDir.sqrMagnitude > 0.001f ? currentSetting.maxSpeed : 0f;
        Vector3 wishDir = Quaternion.Euler(0f, 45f, 0f) * rawInputDir;
        Vector3 wishVel = wishDir * targetSpeed;

        if (currentSetting.speedMode == SpeedMode.Instant)
        {
            smoothRef = Vector3.zero;
            horizVel = wishDir * currentSetting.maxSpeed;
        }
        else if (currentSetting.speedMode == SpeedMode.Accelerated)
        {
            horizVel = Vector3.SmoothDamp(horizVel, wishVel, ref smoothRef, accelTime, currentSetting.maxSpeed + 1f);
        }



    }

    void FixedUpdate()
    {
        if (jumpQueued && IsGrounded())
        {
            rb.AddForce(Vector3.up * currentSetting.jumpForce, ForceMode.Impulse);
            jumpQueued = false;
        }

        Vector3 desiredMove = horizVel * Time.fixedDeltaTime;

        if (desiredMove.sqrMagnitude > 1e-5f)
        {
            Vector3 p1 = transform.position + Vector3.up * (col.radius - 0.01f);
            Vector3 p2 = p1 + Vector3.up * (col.height - 2f * col.radius);

            if (Physics.CapsuleCast(p1, p2, col.radius * 0.96f, desiredMove.normalized, out RaycastHit hit, desiredMove.magnitude, solidMask, QueryTriggerInteraction.Ignore))
            {
                float slopeDeg = Vector3.Angle(hit.normal, Vector3.up);
                if (slopeDeg <= maxWalkSlope)
                {
                    desiredMove = Vector3.ProjectOnPlane(desiredMove, hit.normal);
                }
                else
                {
                    desiredMove = Vector3.zero;
                }
            }
        }

        rb.MovePosition(rb.position + desiredMove);
        rb.linearVelocity = new Vector3(desiredMove.x / Time.fixedDeltaTime, rb.linearVelocity.y, desiredMove.z / Time.fixedDeltaTime);
    }

    bool IsGrounded()
    {
        float skin = currentSetting.groundSkin;
        float half = (col.height * 0.85f) - col.radius;
        Vector3 ori = transform.position + Vector3.up * (col.radius - 0.01f);

        if (Physics.SphereCast(ori, col.radius * 0.96f, Vector3.down, out RaycastHit hit, half + skin, solidMask, QueryTriggerInteraction.Ignore))
        {
            return hit.normal.y >= Mathf.Cos(maxWalkSlope * Mathf.Deg2Rad);
        }
        return false;
    }


    public void ApplyMovementSettings(MovementSettings settings)
    {
        this.currentSetting = settings;
    }
}
