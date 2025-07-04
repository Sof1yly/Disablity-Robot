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
        private bool isBoosting = false;

        [SerializeField] string MoveScheme = "Move";

        void Awake()
        {
            // Get PlayerInput component and bind actions
            playerInput = GetComponent<PlayerInput>();
        }

        void OnEnable()
        {
            // Bind actions
            moveAction = playerInput.actions[MoveScheme];   // "Move" action (WASD or Gamepad Left Stick)
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
            float boostInput = boostAction.ReadValue<float>();

            if (boostInput > 0.5f)  // Boosting when button is held down (threshold for trigger)
            {
                if (boostTimer == 0)
                {
                    // Start boosting
                    isBoosting = true;
                    boostTimer = boostDuration;
                }
            }
            else
            {
                // Stop boosting when button is released
                isBoosting = false;
            }

            // Handle timer and apply boost multiplier if boosting
            if (isBoosting)
            {
                boostTimer = Mathf.Max(boostTimer - Time.deltaTime, 0);  // Decrease boost timer
                carController.setBoostMultiplier(boostMultiplier); // Apply boost multiplier
            }
            else
            {
                if (boostTimer > boostCoolOff)
                {
                    boostTimer = Mathf.Max(boostTimer - Time.deltaTime, 0); // Cooldown timer
                }
                else
                {
                    carController.setBoostMultiplier(1); // Reset to normal multiplier
                }
            }

            Debug.Log($"{this.transform.gameObject.name} : Input State {moveInput.x} {moveInput.y} Boost: {boostInput}");

            carController.setSteering(steeringDelta);  // Steering (left/right)
            carController.setMotor(motorDelta);       // Motor movement (forward/backward)
        }
    }
}
