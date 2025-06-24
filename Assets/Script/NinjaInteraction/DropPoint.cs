using UnityEngine;
using UnityEngine.Events;

public class DropPoint : MonoBehaviour
{
    [Header("Drop Point Settings")]
    [SerializeField] private Transform snapPoint;
    private string playerTag = "Player";
    [SerializeField] private string requiredItemTag = ""; // Optional restriction for item tag
    [SerializeField] private string missionItemTag = "";  // Optional mission item tag
    [SerializeField] UnityEvent Mission_event = new UnityEvent(); // Event triggered for mission items

    private bool isPlayerInTrigger = false; // Tracks if the player is within the trigger
    private PickUpScript playerPickUpScript = null; // Reference to the player's PickUpScript

    // Subscribes to the OnDropAttempt event when the script is enabled
    void OnEnable()
    {
        PickUpScript.OnDropAttempt += HandleDropAttempt;
    }

    // Unsubscribes from the OnDropAttempt event when the script is disabled
    void OnDisable()
    {
        PickUpScript.OnDropAttempt -= HandleDropAttempt;
    }

    // Initializes the DropPoint settings and ensures necessary components are in place
    void Start()
    {
        if (snapPoint == null)
        {
            Debug.LogError("Snap Point not assigned for DropPoint script.");
            enabled = false; // Disable script if essential setup is missing
            return;
        }

        Collider col = GetComponent<Collider>();
        if (col == null || !col.isTrigger)
        {
            Debug.LogWarning("DropPoint requires a Collider with 'Is Trigger' enabled.");
        }

        playerPickUpScript = FindFirstObjectByType<PickUpScript>();
        if (playerPickUpScript == null)
        {
            Debug.LogError("Could not find an active PickUpScript in the scene.");
        }
    }

    // Handles drop attempt when an item is dropped at the DropPoint
    private void HandleDropAttempt(GameObject itemAttemptingToDrop)
    {
        if (isPlayerInTrigger && PickUpScript.currentHeldItem == itemAttemptingToDrop)
        {
            // If a required item tag is specified, check if the item matches the tag
            if (!string.IsNullOrEmpty(requiredItemTag) && !itemAttemptingToDrop.CompareTag(requiredItemTag))
            {
                Debug.Log($"Cannot drop '{itemAttemptingToDrop.name}' here. Requires '{requiredItemTag}' tag.");
                return; // Exit if item doesn't match the required tag
            }

            AttemptSnapItem(); // Proceed with snapping the item
        }
    }

    // Detects when the player enters the DropPoint's trigger zone
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            isPlayerInTrigger = true;
            Debug.Log("Player entered DropPoint trigger.");
        }
    }

    // Detects when the player exits the DropPoint's trigger zone
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            isPlayerInTrigger = false;
            Debug.Log("Player exited DropPoint trigger.");
        }
    }

    // Attempts to snap the held item to the drop point, releasing it from the player
    private void AttemptSnapItem()
    {
        GameObject itemToSnap = playerPickUpScript.ReleaseHeldItemWithoutPhysicsReset();

        if (itemToSnap != null)
        {
            itemToSnap.transform.SetParent(this.transform);
            itemToSnap.transform.position = snapPoint.position;
            itemToSnap.transform.rotation = snapPoint.rotation;

            Rigidbody itemRb = itemToSnap.GetComponent<Rigidbody>();
            if (itemRb != null)
            {
                itemRb.isKinematic = true;
                itemRb.useGravity = false;
                itemRb.linearVelocity = Vector3.zero;
                itemRb.angularVelocity = Vector3.zero;
                itemRb.WakeUp();
            }

            Collider itemCollider = itemToSnap.GetComponent<Collider>();
            if (itemCollider != null)
            {
                itemCollider.enabled = true;
            }

            // Trigger Mission Event if item matches the mission item tag
            if (!string.IsNullOrEmpty(missionItemTag) && itemToSnap.CompareTag(missionItemTag))
            {
                Mission_event?.Invoke();
                Debug.Log($"Mission Event triggered by dropping '{itemToSnap.name}'.");
            }

            Debug.Log($"Successfully snapped '{itemToSnap.name}' to DropPoint.");
        }
        else
        {
            Debug.Log("No item held by player to snap to DropPoint.");
        }
    }
}
