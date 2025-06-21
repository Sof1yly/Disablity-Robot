using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private MovementSettings settings;

    private CharacterController _cc;
    private InputSystem_Actions _inputActions;

    private Vector3 _input;
    private float _currentSpeed;

    private bool _isJumping;
    private float _verticalVelocity;
    private bool _jumpInput;

    private void Awake()
    {
        _cc = GetComponent<CharacterController>();
        _inputActions = new InputSystem_Actions();
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
        GatherInput();
        HandleLook();
        HandleSpeed();
        HandleMove();
    }

    private void GatherInput()
    {
        Vector2 mv = _inputActions.Player.Move.ReadValue<Vector2>();
        _input = new Vector3(mv.x, 0f, mv.y);

        _jumpInput = _inputActions.Player.Jump.triggered;
    }

    private void HandleLook()
    {
        if (_input.sqrMagnitude < 0.001f) return;

        Vector3 iso = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0))
                        .MultiplyPoint3x4(_input)
                        .normalized;
        Quaternion target = Quaternion.LookRotation(iso, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            target,
            settings.rotationSpeed * Time.deltaTime
        );
    }

    private void HandleSpeed()
    {
        if (_input == Vector3.zero && _currentSpeed > 0f)
            _currentSpeed -= settings.decelerationFactor * Time.deltaTime;
        else if (_input != Vector3.zero && _currentSpeed < settings.maxSpeed)
            _currentSpeed += settings.accelerationFactor * Time.deltaTime;

        _currentSpeed = Mathf.Clamp(_currentSpeed, 0f, settings.maxSpeed);
    }

    private void HandleMove()
    {
        Vector3 horiz = transform.forward * _currentSpeed * Time.deltaTime;

        Vector3 vert = Vector3.zero;
        if (_jumpInput && !_isJumping)
        {
            _isJumping = true;
            _verticalVelocity = settings.jumpForce;
        }

        if (_isJumping)
        {
            _verticalVelocity += settings.gravity * Time.deltaTime;
            vert = Vector3.up * _verticalVelocity * Time.deltaTime;

            if (_cc.isGrounded && _verticalVelocity <= 0f)
            {
                _isJumping = false;
                _verticalVelocity = 0f;
            }
        }

        _cc.Move(horiz + vert);
    }
}
