using UnityEngine;
using UnityEngine.InputSystem;

namespace DavidJalbert
{
    public class TinyCarStandardInput : MonoBehaviour
    {
        public TinyCarController carController;

        private PlayerInput playerInput;    // Reference to PlayerInput
        private InputAction moveAction;     // Action for movement (Steering)
        private InputAction boostAction;    // Action for boosting
        private InputAction accelerateAction; // Action for acceleration (Right Trigger)
        private InputAction Restart;
        public float boostDuration = 1;
        public float boostCoolOff = 2;  // Time to wait before the next boost is available
        public float boostMultiplier = 2;
        public float triggerHoldDuration = 1; // Time in seconds to hold the trigger at max for boost
        public float triggerFullyPressed = 1f; // Trigger value when fully pressed (max = 1)
        private float boostTimer = 0;
        private bool isBoosting = false;
        private float triggerHoldTime = 0; // Time the trigger has been held at max
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
            float RestartData = Restart.ReadValue<float>();


            if (RestartData > 0)
            {
                Restarter.RestartCar();
            }


            // Track trigger value and time held at max
            if (motorDelta >= triggerFullyPressed)  // If right trigger is fully pressed
            {
                triggerHoldTime += Time.deltaTime;
            }
            else
            {
                triggerHoldTime = 0; // Reset the timer if the trigger is not at max
            }

            // Check if the trigger has been held for the required duration to activate the boost
            if (triggerHoldTime >= triggerHoldDuration && !isBoosting && boostTimer <= 0)
            {
                isBoosting = true;  // Activate boost
                boostTimer = boostDuration; // Start boost timer
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

            //Debug.Log($"{this.transform.gameObject.name} : Input State {moveInput.x} {motorDelta} Boost: {isBoosting} Trigger Hold Time: {triggerHoldTime}");

            carController.setSteering(steeringDelta);  // Steering (left/right)
            carController.setMotor(motorDelta);       // Motor movement (accelerate/decelerate)
        }
    }
}
