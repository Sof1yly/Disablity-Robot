using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [Header("Speeds")]
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float rotationSpeed = 360f;

    [Header("Accel / Decel")]
    [SerializeField] private float accelerationFactor = 5f;
    [SerializeField] private float decelerationFactor = 10f;

    [Header("Gravity")]
    [SerializeField] private float gravity = -9.81f;

    private InputSystem_Actions _inputActions;
    private CharacterController _controller;
    private Vector3 _input;
    private Vector3 _velocity;
    private float _currentSpeed;
    private bool _isGrounded;

    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
        _controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        _inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        _inputActions.Player.Disable();
    }

    private void Update()
    {
        // handle gravity
        _isGrounded = _controller.isGrounded;
        if (_isGrounded && _velocity.y < 0)
            _velocity.y = -3f;     // small downward to keep grounded
        else
            _velocity.y += gravity * Time.deltaTime;

        GatherInput();
        HandleRotation();
        HandleSpeed();
        HandleMovement();
    }

    private void GatherInput()
    {
        Vector2 raw = _inputActions.Player.Move.ReadValue<Vector2>();
        _input = new Vector3(raw.x, 0f, raw.y);
    }

    private void HandleRotation()
    {
        if (_input == Vector3.zero) return;

        // isometric rotation fix
        var isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45f, 0));
        Vector3 isoDir = isoMatrix.MultiplyPoint3x4(_input);

        Quaternion targetRot = Quaternion.LookRotation(isoDir, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRot,
            rotationSpeed * Time.deltaTime
        );
    }

    private void HandleSpeed()
    {
        if (_input == Vector3.zero && _currentSpeed > 0f)
        {
            _currentSpeed -= decelerationFactor * Time.deltaTime;
        }
        else if (_input != Vector3.zero && _currentSpeed < maxSpeed)
        {
            _currentSpeed += accelerationFactor * Time.deltaTime;
        }

        _currentSpeed = Mathf.Clamp(_currentSpeed, 0f, maxSpeed);
    }

    private void HandleMovement()
    {
        // forward motion + gravity
        Vector3 move = transform.forward * _currentSpeed * Time.deltaTime;
        move += _velocity * Time.deltaTime;
        _controller.Move(move);
    }
}
