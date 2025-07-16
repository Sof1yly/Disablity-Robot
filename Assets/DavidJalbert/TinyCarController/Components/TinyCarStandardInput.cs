using UnityEngine;
using UnityEngine.InputSystem;

namespace DavidJalbert
{
    public class TinyCarStandardInput : MonoBehaviour
    {
        public TinyCarController carController;

        private PlayerInput playerInput;    // Reference to PlayerInput
        private InputAction moveAction;     // Action for movement (Steering)
        private InputAction boostAction;    // Action for boosting (East Button)
        private InputAction accelerateAction; // Action for acceleration (Right Trigger)
        private InputAction brakeAction;    // Action for braking (Left Trigger)
        private InputAction Restart;

        public float boostTankCapacity = 2; // The total boost tank capacity (how much boost is available)
        public float boostMultiplier = 2;   // Multiplier for boost
        private float currentBoost = 2;     // Current amount of boost in the tank (starts full)
        private bool isBoosting = false;    // Flag to track if the car is boosting
        private float boostDrainRate = 1f;  // How fast the boost drains when the boost button is held

        [SerializeField] string MoveScheme = "Move";

        [SerializeField] TrackUpdate Restarter;

        void Awake()
        {
            // Get PlayerInput component and bind actions
            playerInput = GetComponent<PlayerInput>();
        }

        void OnEnable()
        {
            // Bind actions
            moveAction = playerInput.actions[MoveScheme];  // "Move" action (WASD or Gamepad Left Stick)
            boostAction = playerInput.actions["Boost"];    // "Boost" action (East Button)
            accelerateAction = playerInput.actions["Accelerate"]; // "Accelerate" (Right Trigger)
            brakeAction = playerInput.actions["Brake"];  // "Brake" action (Left Trigger)
            Restart = playerInput.actions["Restart"];
        }

        void Update()
        {
            // Get the movement input as Vector2 (X = left/right, Y = forward/backward)
            Vector2 moveInput = moveAction.ReadValue<Vector2>();

            // Steering (Left/Right) is based on the X-axis (Gamepad horizontal axis)
            float steeringDelta = moveInput.x;  // Steering (A/D or Gamepad horizontal axis)

            // Get the right trigger value (used for acceleration)
            float motorDelta = accelerateAction.ReadValue<float>(); // Right trigger for acceleration
            float brakeDelta = brakeAction.ReadValue<float>(); // Left trigger for braking (reverse)
            float RestartData = Restart.ReadValue<float>();

            if (RestartData > 0)
            {
                Restarter.RestartCar();
            }

            // Check if the Left Trigger is pressed for reverse movement
            if (brakeDelta >= 1f)  // Left trigger is pressed
            {
                motorDelta = -1; // Move backward by setting motor value to negative
            }

            // Handle Boost Action (East Button press)
            if (boostAction.ReadValue<float>() > 0 && currentBoost > 0) // If the boost button is held and there's enough fuel
            {
                isBoosting = true;
                currentBoost -= boostDrainRate * Time.deltaTime; // Drain the boost tank while holding the button
            }
            else
            {
                isBoosting = false; // Stop boosting when the button is released or when the tank is empty
            }

            // Apply the boost multiplier if boosting
            if (isBoosting)
            {
                carController.setBoostMultiplier(boostMultiplier); // Apply boost multiplier
            }
            else
            {
                carController.setBoostMultiplier(1); // Reset to normal multiplier
            }

            Debug.Log($"{this.transform.gameObject.name} : Input State {moveInput.x} {motorDelta} Boost: {isBoosting} Boost Level: {currentBoost}");

            carController.setSteering(steeringDelta);  // Steering (left/right)
            carController.setMotor(motorDelta);       // Motor movement (accelerate/decelerate)
        }
    }
}
