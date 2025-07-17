using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DavidJalbert
{
    public class TinyCarCamera : MonoBehaviour
    {
        public enum CAMERA_MODE
        {
            TopDown, ThirdPerson
        }

        [Tooltip("Which Transform the camera should track.")]
        public Transform whatToFollow;
        [Tooltip("Top Down: Only change the camera's position, keep rotation fixed.\nThird Person: Change both the position and rotation relative to the vehicle.")]
        public CAMERA_MODE viewMode = CAMERA_MODE.TopDown;

        [Header("Top Down parameters")]
        [Tooltip("Distance of the camera from the target.")]
        public float topDownDistance = 50;
        [Tooltip("Rotation of the camera.")]
        public Vector3 topDownAngle = new Vector3(60, 0, 0);
        [Tooltip("Smoothing of the camera's rotation. The lower the value, the smoother the rotation. Set to 0 to disable smoothing.")]
        public float topDownInterpolation = 10;

        [Header("Third Person parameters")]
        [Tooltip("Position of the camera relative to the target.")]
        public Vector3 thirdPersonOffset = new Vector3(0, 2, -10);
        [Tooltip("Rotation of the camera relative to the target.")]
        public Vector3 thirdPersonAngle = new Vector3(15, 0, 0);
        [Tooltip("The minimum distance to keep when an obstacle is in the way of the camera.")]
        public float thirdPersonSkinWidth = 0.1f;
        [Tooltip("Smoothing of the camera's rotation. The lower the value, the smoother the rotation. Set to 0 to disable smoothing.")]
        public float thirdPersonInterpolation = 10;
        [Tooltip("Smoothing of the camera's position. The lower the value, the smoother the transition.")]
        public float thirdPersonPositionInterpolation = 10;

        private void Start()
        {
        }

        void FixedUpdate()
        {
            Vector3 followPosition = whatToFollow.position;
            Quaternion followRotation = whatToFollow.rotation;

            Vector3 targetPosition = transform.position;
            Quaternion targetRotation = transform.rotation;

            float deltaTime = Time.fixedDeltaTime;

            switch (viewMode)
            {
                case CAMERA_MODE.ThirdPerson:
                    // Calculate rotation based on the vehicle's rotation
                    Vector3 rotationEuler = thirdPersonAngle + Vector3.up * followRotation.eulerAngles.y;

                    // Smoothly interpolate the camera's rotation
                    targetRotation = Quaternion.Lerp(targetRotation, Quaternion.Euler(rotationEuler), Mathf.Clamp01(thirdPersonInterpolation <= 0 ? 1 : thirdPersonInterpolation * deltaTime));

                    // Calculate the direction for the camera's position
                    Vector3 forwardDirection = targetRotation * Vector3.forward;
                    Vector3 rightDirection = targetRotation * Vector3.right;
                    Vector3 directionVector = forwardDirection * thirdPersonOffset.z + Vector3.up * thirdPersonOffset.y + rightDirection * thirdPersonOffset.x;
                    Vector3 directionVectorNormal = directionVector.normalized;
                    float directionMagnitude = directionVector.magnitude;

                    // Check if there is an obstacle between the camera and the target
                    Vector3 cameraWorldDirection = directionVectorNormal;
                    Vector3 startCast = followPosition;
                    RaycastHit hit;
                    if (Physics.Raycast(startCast, cameraWorldDirection, out hit, directionMagnitude))
                    {
                        targetPosition = followPosition + directionVectorNormal * Mathf.Max(thirdPersonSkinWidth, hit.distance - thirdPersonSkinWidth);
                    }
                    else
                    {
                        targetPosition = directionVector + followPosition;
                    }

                    // Smoothly interpolate the camera's position
                    targetPosition = Vector3.Lerp(transform.position, targetPosition, Mathf.Clamp01(thirdPersonPositionInterpolation <= 0 ? 1 : thirdPersonPositionInterpolation * deltaTime));

                    break;

                case CAMERA_MODE.TopDown:
                    targetRotation = Quaternion.Euler(topDownAngle);
                    // Interpolate the camera's position in the TopDown view
                    targetPosition = Vector3.Lerp(targetPosition, followPosition + targetRotation * Vector3.back * topDownDistance, Mathf.Clamp01(topDownInterpolation <= 0 ? 1 : topDownInterpolation * deltaTime));
                    break;
            }

            // Update the camera's position and rotation
            transform.position = targetPosition;
            transform.rotation = targetRotation;
        }
    }
}
