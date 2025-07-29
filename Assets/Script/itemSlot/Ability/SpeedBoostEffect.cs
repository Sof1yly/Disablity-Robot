using UnityEngine;
using System.Collections;
using DavidJalbert;

public class SpeedBoostEffect : MonoBehaviour
{
    [HideInInspector] public float multiplier;
    [HideInInspector] public float duration;

    private TinyCarController carController;
    private float originalMaxSpeed;

    void Awake()
    {
        carController = GetComponent<TinyCarController>();
        if (carController == null)
        {
            Destroy(this);
            return;
        }
        // Store the original speed
        originalMaxSpeed = carController.maxSpeedForward;
        // Apply boost
        carController.maxSpeedForward = originalMaxSpeed * multiplier;
        // Schedule removal
        StartCoroutine(RemoveAfterDelay());
    }

    IEnumerator RemoveAfterDelay()
    {
        yield return new WaitForSeconds(duration);
        carController.maxSpeedForward = originalMaxSpeed;
        Destroy(this);
    }
}
