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

    [Header("Jump")]
    [SerializeField] private float jumpHeight = 2f;

    [Header("Gravity")]
    [SerializeField] private float gravity = -9.81f;

    private InputSystem_Actions _inputActions;
    private CharacterController _controller;
    private Vector3 _input;
    private Vector3 _moveDirection;
    private float _currentSpeed;
    private bool _isGrounded;
    private float _verticalVelocity;
    private float _jumpVelocity;

    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
        _controller = GetComponent<CharacterController>();
        CalculateJumpVelocity();
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
        ReadGroundState();
        ProcessJump();
        ProcessGravity();
        ReadInput();
        ProcessRotation();
        ProcessSpeed();
        ProcessMovement();
    }

    private void CalculateJumpVelocity()
    {

        _jumpVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    private void ReadGroundState()
    {
        _isGrounded = _controller.isGrounded;
        if (_isGrounded && _verticalVelocity < 0f)
        {
            _verticalVelocity = -2f;
        }
    }

    private void ProcessJump()
    {
        if (_inputActions.Player.Jump.triggered && _isGrounded)
        {
            _verticalVelocity = _jumpVelocity;
        }
    }

    private void ProcessGravity()
    {
        if (!_isGrounded)
        {
            _verticalVelocity += gravity * Time.deltaTime;
        }
    }

    private void ReadInput()
    {
        Vector2 raw = _inputActions.Player.Move.ReadValue<Vector2>();
        _input = new Vector3(raw.x, 0f, raw.y);
    }

    private void ProcessRotation()
    {
        if (_input.sqrMagnitude < 0.001f) return;

        var isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45f, 0));
        Vector3 isoDir = isoMatrix.MultiplyPoint3x4(_input);
        Quaternion target = Quaternion.LookRotation(isoDir, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, rotationSpeed * Time.deltaTime);
    }

    private void ProcessSpeed()
    {
        if (_input.sqrMagnitude > 0.001f)
        {
            _currentSpeed = Mathf.Min(_currentSpeed + accelerationFactor * Time.deltaTime, maxSpeed);
        }
        else
        {
            _currentSpeed = Mathf.Max(_currentSpeed - decelerationFactor * Time.deltaTime, 0f);
        }
    }

    private void ProcessMovement()
    {

        _moveDirection = Vector3.zero;
        if (_input.sqrMagnitude > 0.001f)
        {
            _moveDirection = transform.forward * _currentSpeed;
        }


        _moveDirection.y = _verticalVelocity;

        _controller.Move(_moveDirection * Time.deltaTime);
    }
}
