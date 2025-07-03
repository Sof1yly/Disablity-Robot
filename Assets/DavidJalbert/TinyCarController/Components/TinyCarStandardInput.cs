using UnityEngine;
using UnityEngine.InputSystem;

namespace DavidJalbert
{
    public class TinyCarStandardInput : MonoBehaviour
    {
        public TinyCarController carController;

        private PlayerInput playerInput;    // Reference to PlayerInput
        private InputAction moveAction;     // Action for movement (Vector2)
        private InputAction boostAction;    // Action for boosting

        public float boostDuration = 1;
        public float boostCoolOff = 0;
        public float boostMultiplier = 2;

        private float boostTimer = 0;

        void Awake()
        {
            // Get PlayerInput component and bind actions
            playerInput = GetComponent<PlayerInput>();
        }

        void OnEnable()
        {
            // Bind actions
            moveAction = playerInput.actions["Move"];   // "Move" action (WASD or Gamepad Left Stick)
            boostAction = playerInput.actions["Boost"]; // "Boost" action
        }

        void Update()
        {
            // Get the movement input as Vector2 (X = left/right, Y = forward/backward)
            Vector2 moveInput = moveAction.ReadValue<Vector2>();

            // Forward/Backward movement is based on the Y-axis (W/S or Gamepad vertical axis)
            float motorDelta = moveInput.y;  // Forward/Backward movement (W/S or Gamepad)

            // Steering (Left/Right) is based on the X-axis (A/D or Gamepad horizontal axis)
            float steeringDelta = moveInput.x;  // Steering (A/D or Gamepad horizontal axis)

            // Handle boosting
            if (boostAction.triggered && boostTimer == 0)
            {
                boostTimer = boostCoolOff + boostDuration;
            }
            else if (boostTimer > 0)
            {
                boostTimer = Mathf.Max(boostTimer - Time.deltaTime, 0);
                carController.setBoostMultiplier(boostTimer > boostCoolOff ? boostMultiplier : 1);
            }

            // Apply motor and steering input to the car controller
            carController.setSteering(steeringDelta);  // Steering (left/right)
            carController.setMotor(motorDelta);       // Motor movement (forward/backward)
        }
    }
}
