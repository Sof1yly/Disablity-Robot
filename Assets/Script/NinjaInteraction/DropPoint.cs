using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This script defines a drop-off point where a player can deposit a held item.
/// When an item is dropped at this point by pressing the interaction key,
/// it will be taken from the player and snapped to a specific position and rotation.
///
/// This updated version allows for optional restriction of items based on their tag,
/// and can trigger a UnityEvent specifically when an item with a designated tag is dropped.
///
/// Setup:
/// 1. Attach this script to an empty GameObject.
/// 2. Add a Collider component (e.g., Box Collider, Sphere Collider) to this GameObject
///    and ensure 'Is Trigger' is checked. This defines the interaction zone.
/// 3. Create an empty child GameObject under this DropPoint GameObject (or any Transform in your scene)
///    and name it something descriptive like "SnapPoint". Position and rotate this "SnapPoint"
///    where you want items to rest.
/// 4. Drag and drop your "SnapPoint" Transform into the 'Snap Point' field in the Inspector of this DropPoint script.
/// 5. Ensure your Player GameObject has the tag specified in 'Player Tag' (default is "Player").
/// 6. (Optional) To restrict what can be dropped, enter an 'Item Tag' in the 'Required Item Tag' field.
///    Only items with this tag will be accepted. Leave empty to accept any item.
/// 7. (Optional) To trigger an event for a specific item, enter its tag in the 'Mission Item Tag' field
///    and configure the 'Mission Event' in the Inspector.
/// </summary>
public class DropPoint : MonoBehaviour
{
    [Header("Drop Point Settings")]
    [Tooltip("The Transform where the item will snap to when successfully dropped.")]
    [SerializeField] private Transform snapPoint;

    [Tooltip("The tag assigned to the player GameObject. Used to detect player presence.")]
    private string playerTag = "Player"; // Made serialized for inspector access

    [Tooltip("Optional: If set, only items with this tag can be dropped at this point. Leave empty to allow any item.")]
    [SerializeField] private string requiredItemTag = "";

    [Header("Mission Settings")]
    [Tooltip("Optional: The tag of an item that, when successfully dropped here, will trigger the Mission Event. Leave empty if no specific item triggers the event.")]
    [SerializeField] private string missionItemTag = "";

    [Tooltip("Event triggered when an item with the 'Mission Item Tag' is successfully dropped at this point.")]
    [SerializeField] UnityEvent Mission_event = new UnityEvent();

    // The dropKey is no longer directly used in DropPoint's Update,
    // but kept as a reminder that PickUpScript's key should match.
    // public KeyCode dropKey = KeyCode.E;

    private bool isPlayerInTrigger = false; // Tracks if the player is currently within this drop point's trigger.
    private PickUpScript playerPickUpScript = null; // Reference to the player's PickUpScript

    /// <summary>
    /// Called when this script is enabled. Used for event subscription.
    /// </summary>
    void OnEnable()
    {
        // Subscribe to the OnDropAttempt event from PickUpScript.
        PickUpScript.OnDropAttempt += HandleDropAttempt;
    }

    /// <summary>
    /// Called when this script is disabled. Used for event unsubscription to prevent memory leaks.
    /// </summary>
    void OnDisable()
    {
        // Unsubscribe from the OnDropAttempt event.
        PickUpScript.OnDropAttempt -= HandleDropAttempt;
    }

    /// <summary>
    /// Called when the script instance is being loaded.
    /// </summary>
    void Start()
    {
        // Basic validation for snapPoint.
        if (snapPoint == null)
        {
            Debug.LogError("Snap Point not assigned for DropPoint script on " + gameObject.name + ". " +
                            "Please assign a Transform in the Inspector where items will snap to.");
            enabled = false; // Disable script if essential setup is missing.
            return;
        }

        // Ensure this GameObject has a collider set as a trigger.
        Collider col = GetComponent<Collider>();
        if (col == null || !col.isTrigger)
        {
            Debug.LogWarning("DropPoint on " + gameObject.name + " requires a Collider with 'Is Trigger' enabled. " +
                             "Please add one (e.g., Box Collider, Sphere Collider) to define the interaction zone.");
        }

        // Try to find the PickUpScript on the player. This is done once at Start.
        // Assumes there's only one PickUpScript in the scene on the player.
        playerPickUpScript = FindFirstObjectByType<PickUpScript>();
        if (playerPickUpScript == null)
        {
            Debug.LogError("DropPoint could not find an active PickUpScript in the scene. " +
                            "Ensure your player GameObject has the PickUpScript attached and is active.");
        }
    }

    /// <summary>
    /// This method is called when PickUpScript's OnDropAttempt event is invoked.
    /// It acts as the observer's reaction to a drop attempt.
    /// </summary>
    /// <param name="itemAttemptingToDrop">The GameObject the player is currently holding.</param>
    private void HandleDropAttempt(GameObject itemAttemptingToDrop)
    {
        // Only attempt to snap the item if the player is within this DropPoint's trigger
        // AND the player is actually holding the item that was just attempted to drop (safety check).
        if (isPlayerInTrigger && PickUpScript.currentHeldItem == itemAttemptingToDrop)
        {
            // If a required item tag is specified, check if the item attempting to drop has that tag.
            // If it doesn't, log a message and do not proceed with snapping.
            if (!string.IsNullOrEmpty(requiredItemTag) && !itemAttemptingToDrop.CompareTag(requiredItemTag))
            {
                Debug.Log($"Cannot drop '{itemAttemptingToDrop.name}' here. This DropPoint only accepts items with the tag '{requiredItemTag}'.", itemAttemptingToDrop);
                return; // Exit the method, preventing the item from being snapped.
            }

            // If no required tag, or if the item matches the required tag, proceed to snap.
            AttemptSnapItem();
        }
    }

    /// <summary>
    /// Called when another collider enters this object's trigger collider.
    /// </summary>
    /// <param name="other">The Collider that entered the trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider belongs to the player.
        if (other.CompareTag(playerTag))
        {
            isPlayerInTrigger = true;
            Debug.Log("Player entered DropPoint trigger: " + gameObject.name);
        }
    }

    /// <summary>
    /// Called when another collider exits this object's trigger collider.
    /// </summary>
    /// <param name="other">The Collider that exited the trigger.</param>
    private void OnTriggerExit(Collider other)
    {
        // Check if the exiting collider belongs to the player.
        if (other.CompareTag(playerTag))
        {
            isPlayerInTrigger = false;
            Debug.Log("Player exited DropPoint trigger: " + gameObject.name);
        }
    }

    /// <summary>
    /// Attempts to snap the currently held item to this drop point.
    /// This method takes control of the item directly from the PickUpScript.
    /// </summary>
    private void AttemptSnapItem()
    {
        // Call the new method on the player's PickUpScript to release the item.
        // This ensures PickUpScript clears its internal references and doesn't interfere
        // with our snapping logic.
        GameObject itemToSnap = playerPickUpScript.ReleaseHeldItemWithoutPhysicsReset();

        // If an item was successfully released by the PickUpScript:
        if (itemToSnap != null)
        {
            // Set the item's parent to this DropPoint's GameObject.
            // This makes the item move with the DropPoint if the DropPoint itself moves.
            itemToSnap.transform.SetParent(this.transform);

            // Snap the item's position and rotation to the defined snapPoint.
            itemToSnap.transform.position = snapPoint.position;
            itemToSnap.transform.rotation = snapPoint.rotation;

            // Get the Rigidbody component of the snapped item.
            Rigidbody itemRb = itemToSnap.GetComponent<Rigidbody>();
            if (itemRb != null)
            {
                // Make the Rigidbody kinematic to lock the item in place.
                // It will no longer be affected by physics forces or gravity.
                itemRb.isKinematic = true;
                itemRb.useGravity = false;
                itemRb.linearVelocity = Vector3.zero;   // Stop any lingering linear velocity
                itemRb.angularVelocity = Vector3.zero; // Stop any lingering angular velocity

                // Explicitly wake up the Rigidbody to ensure it's re-evaluated by the physics system.
                // This can help ensure trigger detections are consistent.
                itemRb.WakeUp();
            }

            // Ensure the item's collider is enabled after being snapped.
            // This is a common point of failure for trigger detection if implicitly disabled.
            Collider itemCollider = itemToSnap.GetComponent<Collider>();
            if (itemCollider != null)
            {
                itemCollider.enabled = true;
            }

            // Check if this specific item triggers the mission event.
            // If a mission item tag is specified and the snapped item matches it, invoke the event.
            if (!string.IsNullOrEmpty(missionItemTag) && itemToSnap.CompareTag(missionItemTag))
            {
                Mission_event?.Invoke();
                Debug.Log($"Mission Event triggered by dropping '{itemToSnap.name}' at '{gameObject.name}'.", itemToSnap);
            }

            Debug.Log($"Successfully snapped '{itemToSnap.name}' to DropPoint: '{gameObject.name}'.", itemToSnap);
        }
        else
        {
            Debug.Log("No item was currently held by the player to snap at this DropPoint. " +
                      "This message might appear if another DropPoint handled the item first, " +
                      "or the player wasn't holding an item when the event fired.");
        }
    }
}
